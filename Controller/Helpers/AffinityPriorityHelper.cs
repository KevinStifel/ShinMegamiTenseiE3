namespace Shin_Megami_Tensei;

public static class AffinityPriorityHelper
{
    public static string GetTopPriorityReaction(List<UnitBase> targets, AffinityElement elementType)
    {
        if (targets.Count == 0)
            return "-";

        string topReaction = "-";
        int topPriority = int.MaxValue;

        foreach (var target in targets)
        {
            string reaction = target.Affinity.GetAffinityReaction(elementType);
            
            int priority = GetPriorityValue(reaction);

            if (priority < topPriority)
            {
                topReaction = reaction;
                topPriority = priority;
            }
        }

        return topReaction;
    }

    private static int GetPriorityValue(string reaction)
    {
        string normalized = reaction.Trim().ToLower();

        return normalized switch
        {
            "rp" => 1,
            "dr" => 1,
            "nu" => 2,
            "wk" => 3,
            "rs" => 4,
            "-" => 5,
            _ => 5
        };
    }
}