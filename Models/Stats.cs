using System.Text.Json.Serialization;

namespace Shin_Megami_Tensei;

public class Stats
{
    public int HP  { get; private set; }
    public int MP  { get; private set; }

    public int MaxHP { get; }
    public int MaxMP { get; }

    public int Str { get; }
    public int Skl { get; }
    public int Mag { get; }
    public int Spd { get; }
    public int Lck { get; }
    
    [JsonConstructor]
    public Stats(int hp, int mp, int str, int skl, int mag, int spd, int lck)
    {
        MaxHP = hp;
        MaxMP = mp;
        HP = hp;
        MP = mp;
        Str = str;
        Skl = skl;
        Mag = mag;
        Spd = spd;
        Lck = lck;
    }
    
    public void UseMP(int cost)
        => MP = CombatMath.ClampAtLeast(MP - cost, 0);

    public void RestoreMP(int amount)
        => MP = CombatMath.ClampAtMost(MP + amount, MaxMP);

    public void TakeDamage(int amount)
        => HP = CombatMath.ClampAtLeast(HP - amount, 0);

    public void Heal(int amount)
        => HP = CombatMath.ClampAtMost(HP + amount, MaxHP);

    public override string ToString()
        => $"HP:{HP}/{MaxHP} MP:{MP}/{MaxMP}";
}