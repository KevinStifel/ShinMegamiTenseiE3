namespace Shin_Megami_Tensei;

public abstract class AffinityBehavior
{
    public abstract AffinityType Type { get; }
    
    public virtual bool ShouldShowHpAfterAttack() => true;

    public abstract double ModifyDamage(double baseDamage);
    
    public abstract void ApplyEffect(UnitBase casterUnit, UnitBase targetUnit, int damage);

    public abstract TurnChange CalculateTurnEffect(int fullTurns, int blinkingTurns);
    
    public virtual void ApplyLightDarkEffect(UnitBase casterUnit, UnitBase targetUnit, SkillData skillData) { }
}