﻿using Shin_Megami_Tensei;

public static class TargetOrderingHelper
{
    public static List<UnitBase> OrderByBoardPreservingRepeats(
        List<UnitBase> selectedTargets,
        IReadOnlyDictionary<string, UnitBase?> boardSlots)
    {
        var ordered = new List<UnitBase>();

        foreach (var boardUnit in boardSlots.Values)
        {
            if (boardUnit == null)
                continue;

            int count = selectedTargets.Count(u => u == boardUnit);
            for (int i = 0; i < count; i++)
                ordered.Add(boardUnit);
        }

        return ordered;
    }
}