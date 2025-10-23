namespace Shin_Megami_Tensei;
public static class MultiTargetCalculator
{ public static List<UnitBase> CalculateMultiTargetSequence(
        List<UnitBase> aliveEnemies,
        int playerSkillUseCount,
        int totalHits)
    {
        if (aliveEnemies.Count == 0)
            return [];

        int totalActiveEnemies = aliveEnemies.Count;
        int startingIndex = playerSkillUseCount % totalActiveEnemies;
        bool shouldMoveRight = startingIndex % 2 == 0;

        List<UnitBase> orderedTargets = [];
        int currentIndex = startingIndex;

        for (int hitIndex = 0; hitIndex < totalHits; hitIndex++)
        {
            orderedTargets.Add(aliveEnemies[currentIndex]);

            currentIndex = shouldMoveRight
                ? (currentIndex + 1) % totalActiveEnemies          // Avanza derecha (rotación circular)
                : (currentIndex - 1 + totalActiveEnemies) % totalActiveEnemies; // Avanza izquierda
        }

        return orderedTargets;
    }
}
