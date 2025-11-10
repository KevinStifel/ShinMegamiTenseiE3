using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class InvitationEffect : EffectBase
{
    public InvitationEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase casterUnit,
        List<UnitBase> targets,
        SkillExecutionContext skillExecutionContext)
    {
        InitializeEffect(skillExecutionContext);
        
        var monsterToSummon = targets[0];
        var (replacedUnit, summonPosition) = PerformBoardUpdate(monsterToSummon);

        HandleSummonResult(casterUnit, monsterToSummon);

        UpdateTurnOrder(casterUnit, monsterToSummon, replacedUnit);
        ApplyTurnCost();
        ApplyMpCost(casterUnit);
    }

    private (UnitBase? ReplacedUnit, string Position) PerformBoardUpdate(UnitBase monsterToSummon)
    {
        var (summonPosition, replacedUnit) = BoardManager.GetPreparedSummonData(CurrentPlayerId);
        var playerBoard = BoardManager.SelectMutableBoard(CurrentPlayerId);
        var reserveUnits = BoardManager.GetReserveUnitsForPlayer(CurrentPlayerId);

        PlaceMonsterOnBoard(playerBoard, summonPosition, monsterToSummon);
        UpdateReserveList(reserveUnits, monsterToSummon, replacedUnit);
        
        return (replacedUnit, summonPosition);
    }
    
    private static void PlaceMonsterOnBoard(
        Dictionary<string, UnitBase?> playerBoard,
        string summonPosition,
        UnitBase monsterToSummon)
    {
        playerBoard[summonPosition] = monsterToSummon;
    }

    private static void UpdateReserveList(
        List<UnitBase> reserveUnits,
        UnitBase monsterToSummon,
        UnitBase? replacedUnit)
    {
        reserveUnits.Remove(monsterToSummon);

        bool hasReplacedUnit = replacedUnit != null;
        if (hasReplacedUnit)
            reserveUnits.Insert(0, replacedUnit!);
    }

    private void HandleSummonResult(UnitBase casterUnit, UnitBase monsterToSummon)
    {
        bool isMonsterDead = monsterToSummon.Stats.HP == 0;

        if (isMonsterDead)
            ReviveAndShowResult(casterUnit, monsterToSummon);
        else
            EffectView.ShowSummonResult(monsterToSummon);
    }

    private void ReviveAndShowResult(UnitBase casterUnit, UnitBase monsterToSummon)
    {
        int healAmount = monsterToSummon.Stats.MaxHP;
        monsterToSummon.Stats.Heal(healAmount);
        EffectView.ShowSummonAndReviveEffect(casterUnit, monsterToSummon, healAmount);
    }

    private void UpdateTurnOrder(
        UnitBase casterUnit, 
        UnitBase monsterToSummon, 
        UnitBase? replacedUnit)
    {
        TurnManager.UpdateOrder(casterUnit, monsterToSummon, replacedUnit);
    }
}
