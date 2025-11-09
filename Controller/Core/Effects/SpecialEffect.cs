using System.Collections.Generic;
using System.Linq;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SpecialEffect : EffectBase
{
    public SpecialEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase casterUnit,
        List<UnitBase> targets,
        SkillExecutionContext skillExecutionContext)
    {
        var (boardManager, turnManager, currentPlayerId) = InitializeCore(skillExecutionContext);

        var (placement, formation, replacedUnit) = BuildSummonSetup(boardManager, currentPlayerId);

        var monsterToSummon = GetMonsterToSummon(targets);

        PerformSummon(monsterToSummon, formation, placement);

        UpdateTurnOrder(turnManager, casterUnit, (monsterToSummon, replacedUnit));

        ApplyNeutralTurnChange(turnManager);
        casterUnit.Stats.UseMP(skillExecutionContext.SkillData.Cost);
    }
    

    private (BoardManager boardManager, TurnManager turnManager, int currentPlayerId)
        InitializeCore(SkillExecutionContext skillContext)
        => (skillContext.BoardManager, skillContext.TurnManager, skillContext.CurrentPlayerId);

    private (SummonPlacement placement, PlayerBoardFormation formation, UnitBase? replacedUnit)
        BuildSummonSetup(BoardManager boardManager, int currentPlayerId)
    {
        var (boardPosition, replacedUnit) = boardManager.GetPreparedSummonData(currentPlayerId);

        var placement = new SummonPlacement(boardPosition, replacedUnit);

        var formation = new PlayerBoardFormation(
            boardManager.SelectMutableBoard(currentPlayerId),
            boardManager.GetReserveUnitsForPlayer(currentPlayerId)
        );

        return (placement, formation, replacedUnit);
    }

    private UnitBase GetMonsterToSummon(List<UnitBase> targets) => targets.First();

    private void PerformSummon(UnitBase monsterToSummon, PlayerBoardFormation formation, SummonPlacement placement)
    {
        var summonEffect = new SummonEffect(View);
        summonEffect.ApplySamuraiSummon(monsterToSummon, formation, placement);
    }

    private void UpdateTurnOrder(
        TurnManager turnManager,
        UnitBase casterUnit,
        (UnitBase monsterToSummon, UnitBase? replacedUnit) units)
    {
        turnManager.UpdateOrder(casterUnit, units.monsterToSummon, units.replacedUnit);
    }
}
