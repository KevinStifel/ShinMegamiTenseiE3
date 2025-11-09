namespace Shin_Megami_Tensei;

public static class AffinityMapper
{
    private static readonly Dictionary<string, AffinityElement> Map = new(StringComparer.OrdinalIgnoreCase)
    {
        { "Phys", AffinityElement.Physical },
        { "Gun", AffinityElement.Gun },
        { "Fire", AffinityElement.Fire },
        { "Ice", AffinityElement.Ice },
        { "Elec", AffinityElement.Elec },
        { "Force", AffinityElement.Force },
        { "Light", AffinityElement.Light },
        { "Dark", AffinityElement.Dark },
        { "Heal", AffinityElement.Heal },
        { "Special", AffinityElement.Special },
        { "Almighty", AffinityElement.Almighty },
        
    };
    public static AffinityElement Parse(string type)
    {
        return Map.TryGetValue(type, out var element)
            ? element
            : throw new UnknownAffinityTypeException($"Tipo de afinidad desconocido: {type}");
    }
}