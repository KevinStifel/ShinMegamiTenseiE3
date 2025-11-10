using Shin_Megami_Tensei;

public static class TargetOrderingHelper
{
    public static List<UnitBase> OrderByBoardPreservingRepeats(
        List<UnitBase> selectedTargets,
        IReadOnlyDictionary<string, UnitBase?> boardSlots)
    {
        var orderedTargets = new List<UnitBase>();

        foreach (var boardUnit in EnumerateNonNullBoardUnits(boardSlots))
            AppendRepeatedTargetsForBoardUnit(orderedTargets, boardUnit, selectedTargets);

        return orderedTargets;
    }
    
    private static IEnumerable<UnitBase> EnumerateNonNullBoardUnits(
        IReadOnlyDictionary<string, UnitBase?> boardSlots)
    {
        foreach (var unit in boardSlots.Values)
            if (unit != null)
                yield return unit;
    }

    private static void AppendRepeatedTargetsForBoardUnit(
        List<UnitBase> orderedTargets,
        UnitBase boardUnit,
        List<UnitBase> selectedTargets)
    {
        int occurrences = CountOccurrencesOf(selectedTargets, boardUnit);
        AppendUnitMultipleTimes(orderedTargets, boardUnit, occurrences);
    }

    private static int CountOccurrencesOf(
        List<UnitBase> selectedTargets,
        UnitBase targetUnit)
    {
        return selectedTargets.Count(unit => unit == targetUnit);
    }

    private static void AppendUnitMultipleTimes(
        List<UnitBase> targets,
        UnitBase unitToAppend,
        int timesToAppend)
    {
        for (int repeatIndex = 0; repeatIndex < timesToAppend; repeatIndex++)
            targets.Add(unitToAppend);
    }
}