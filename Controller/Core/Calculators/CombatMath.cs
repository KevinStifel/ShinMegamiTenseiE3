namespace Shin_Megami_Tensei;

internal static class CombatMath
{
 
    public static int RoundDamageDown(double value) => (int)Math.Floor(value);

    public static double SqrtOfStatTimesPower(int stat, int power)
        => Math.Sqrt((double)stat * power);

    public static int ClampAtLeast(int value, int min)
        => value < min ? min : value;

    public static int ClampAtMost(int value, int max)
        => value > max ? max : value;

    public static int ClampWithinRange(int value, int min, int max)
        => value < min ? min : (value > max ? max : value);

    public static int EnsureNonNegative(int value)
        => value < 0 ? 0 : value;
}