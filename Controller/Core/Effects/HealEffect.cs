using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class HealEffect : EffectBase
{
    public HealEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase casterUnit,
        List<UnitBase> targets,
        SkillExecutionContext skillExecutionContext)
    { 
        InitializeEffect(skillExecutionContext);
        
        EffectView.ShowSeparator();
        ApplyHealToTargets(casterUnit, targets, SkillData);
        
        ApplyTurnCost();
        ApplyMpCost(casterUnit);
    }

    private void ApplyHealToTargets(
        UnitBase casterUnit, 
        List<UnitBase> targets, 
        SkillData skillData)
    {
        foreach (var targetUnit in targets)
        {
            ApplyHealToTarget(casterUnit, targetUnit, skillData);
        }
    }

    private void ApplyHealToTarget(UnitBase caster, UnitBase target, SkillData skillData)
    {
        int healAmount = HealCalculator.CalculateHealAmount(target, skillData);
        target.Stats.Heal(healAmount);
        EffectView.ShowHealEffect(caster, target, healAmount);
    }
}