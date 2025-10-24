namespace Shin_Megami_Tensei;

public static class AffinityGroupHelper
{
    public static HashSet<int> GetLastIndexesOfRepelGroups(List<UnitBase> targets, AffinityElement elementType)
    {
        var repelGroupEndIndexes = new HashSet<int>();

        for (int i = 0; i < targets.Count; i++)
        {
            var currentBehavior = GetAffinityBehavior(targets[i], elementType);
            bool currentIsRepel = currentBehavior.Type == AffinityType.Repel;

            bool nextIsRepel = false;
            if (i + 1 < targets.Count)
            {
                var nextBehavior = GetAffinityBehavior(targets[i + 1], elementType);
                nextIsRepel = nextBehavior.Type == AffinityType.Repel;
            }

            // si este es repel y el siguiente no, termina un grupo
            if (currentIsRepel && !nextIsRepel)
                repelGroupEndIndexes.Add(i);
        }

        return repelGroupEndIndexes;
    }

    private static AffinityBehavior GetAffinityBehavior(UnitBase unit, AffinityElement elementType)
    {
        var reaction = unit.Affinity.GetAffinityReaction(elementType);
        return AffinityBehaviorFactory.Create(reaction);
    }
}