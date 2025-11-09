using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public abstract class SummonSelectorBase : TargetSelectorBase
{
    protected SummonSelectorBase(View view, BoardManager boardManager)
        : base(view, boardManager, new SpecialSelectorView(view)) { }

    protected sealed override List<UnitBase> SelectTargets(UnitBase activeUnit, int currentPlayerId, SkillData skillData)
    {
        List<UnitBase> reserveUnits = GetCandidateUnits(currentPlayerId);
        var monsterToSummon = SelectMonsterToSummon(activeUnit, reserveUnits);

        var summonOptions = GetSummonOptions(currentPlayerId);
        var summonSlot = SelectSummonPosition(summonOptions);
        Board.PrepareSummonData(currentPlayerId, monsterToSummon, summonSlot);
        return [monsterToSummon];
    }

    private UnitBase SelectMonsterToSummon(UnitBase activeUnit, List<UnitBase> reserveUnits)
    {
        SelectorView.ShowAvailableTargets(activeUnit, reserveUnits);

        int monsterIndex = ReadTargetIndex(reserveUnits);
        if (IsSelectionCanceled(monsterIndex))
            throw new ActionCanceledException();

        var monsterToSummon = reserveUnits[monsterIndex];
        SelectorView.ShowSeparator();

        return monsterToSummon;
    }

    private List<(string Position, UnitBase? Occupant)> GetSummonOptions(int currentPlayerId)
    {
        var playerBoard = Board.SelectMutableBoard(currentPlayerId);
        var summonablePositions = GameConstants.BoardPositions.Skip(1);
        var summonOptions = summonablePositions.Select(position => 
            (Position: position, Occupant: playerBoard[position]));

        return summonOptions.ToList();
    }

    private (string Position, UnitBase? Occupant) SelectSummonPosition(
        List<(string Position, UnitBase? Occupant)> summonOptions)
    {
        ((SpecialSelectorView)SelectorView).ShowSummonPositions(summonOptions);

        int positionIndex = ((SpecialSelectorView)SelectorView).ReadPositionIndex(summonOptions.Count);
        if (IsSelectionCanceled(positionIndex))
            throw new ActionCanceledException();

        return summonOptions[positionIndex];
    }

    protected abstract List<UnitBase> GetCandidateUnits(int currentPlayerId);
}