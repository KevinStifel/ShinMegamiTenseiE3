using Shin_Megami_Tensei_GUI;
namespace Shin_Megami_Tensei;

public class Unit(string name, int hp, int mp, int maxHp, int maxMp)
    : IUnit
{
    public string Name { get; } = name;
    public int HP { get; set; } = hp;
    public int MP { get; set; } = mp;
    public int MaxHP { get; } = maxHp;
    public int MaxMP { get; } = maxMp;
}