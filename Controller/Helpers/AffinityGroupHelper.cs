namespace Shin_Megami_Tensei;

public static class AffinityGroupHelper
{
    public static HashSet<int> GetLastIndexesOfRepelGroups(List<UnitBase> targets, AffinityElement elementType)
    {
        var repelGroupEndIndexes = new HashSet<int>();

        for (int index = 0; index < targets.Count; index++)
        {
            var currentBehavior = GetAffinityBehavior(targets[index], elementType);
            bool currentIsRepel = currentBehavior.Type == AffinityType.Repel;

            bool nextIsRepel = false;
            if (index + 1 < targets.Count)
            {
                var nextBehavior = GetAffinityBehavior(targets[index + 1], elementType);
                nextIsRepel = nextBehavior.Type == AffinityType.Repel;
            }

            if (currentIsRepel && !nextIsRepel)
                repelGroupEndIndexes.Add(index);
        }

        return repelGroupEndIndexes;
    }

    private static AffinityBehavior GetAffinityBehavior(UnitBase unit, AffinityElement elementType)
    {
        var reaction = unit.Affinity.GetAffinityReaction(elementType);
        return AffinityBehaviorFactory.Create(reaction);
    }
}