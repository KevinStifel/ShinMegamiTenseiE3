namespace Shin_Megami_Tensei;

public static class MultiTargetCalculator
{
    public static List<UnitBase> CalculateTargets(
        List<UnitBase> aliveEnemies,
        int playerSkillUseCount,
        int totalHits)
    {
        if (aliveEnemies.Count == 0)
            return [];

        int totalAlive = aliveEnemies.Count;
        int startIndex = playerSkillUseCount % totalAlive;
        int selectionDirection = GetSelectionDirection(startIndex);

        List<UnitBase> orderedTargets = [];
        int currentIndex = startIndex;

        for (int hit = 0; hit < totalHits; hit++)
        {
            orderedTargets.Add(aliveEnemies[currentIndex]);
            currentIndex = GetNextIndex(currentIndex, totalAlive, selectionDirection);
        }

        return orderedTargets;
    }

    private static int GetSelectionDirection(int startIndex)
    {
        return startIndex % 2 == 0 ? 1 : -1;
    }

    private static int GetNextIndex(int currentIndex, int total, int selectionDirection)
    {
        return (currentIndex + selectionDirection + total) % total;
    }
}