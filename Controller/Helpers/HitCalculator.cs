namespace Shin_Megami_Tensei;

public static class HitCalculator
{
    public static int CalculateHits(string hitsPattern, int skillUseCount)
    {
        return IsFixedPattern(hitsPattern)
            ? GetFixedHits(hitsPattern)
            : GetRangeHits(hitsPattern, skillUseCount);
    }

    private static bool IsFixedPattern(string hitsPattern)
        => !hitsPattern.Contains('-');

    private static int GetFixedHits(string hitsPattern)
        => int.Parse(hitsPattern);

    private static int GetRangeHits(string hitsPattern, int skillUseCount)
    {
        var (minHits, maxHits) = ParseRange(hitsPattern);
        int range = GetRangeSpan(minHits, maxHits);
        int offset = CalculateOffset(skillUseCount, range);
        return minHits + offset;
    }

    private static (int Min, int Max) ParseRange(string hitsPattern)
    {
        var parts = hitsPattern.Split('-');
        return (int.Parse(parts[0]), int.Parse(parts[1]));
    }

    private static int GetRangeSpan(int minHits, int maxHits)
        => maxHits - minHits + 1;

    private static int CalculateOffset(int skillUseCount, int range)
        => skillUseCount % range;
}