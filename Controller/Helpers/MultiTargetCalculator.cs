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
        bool moveRight = startIndex % 2 == 0;

        List<UnitBase> orderedTargets = [];
        int currentIndex = startIndex;

        for (int hit = 0; hit < totalHits; hit++)
        {
            orderedTargets.Add(aliveEnemies[currentIndex]);
            currentIndex = GetNextIndex(currentIndex, totalAlive, moveRight);
        }

        return orderedTargets;
    }

    private static int GetNextIndex(int currentIndex, int total, bool moveRight)
    {
        return moveRight
            ? (currentIndex + 1) % total
            : (currentIndex - 1 + total) % total;
    }
}
