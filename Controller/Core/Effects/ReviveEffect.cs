using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class ReviveEffect : EffectBase
{
    public ReviveEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase casterUnit,
        List<UnitBase> targets,
        SkillExecutionContext context)
    {
        EffectView.ShowSeparator();

        ReviveDeadUnits(casterUnit, targets, context.SkillData);

        ApplyNeutralTurnChange(context.TurnManager);
        casterUnit.Stats.UseMP(context.SkillData.Cost);
    }

    private void ReviveDeadUnits(
        UnitBase casterUnit,
        IEnumerable<UnitBase> targets,
        SkillData skillData)
    {
        foreach (var target in targets)
        {
            if (!ShouldRevive(target))
                continue;

            ReviveUnit(casterUnit, target, skillData);
        }
    }

    private static bool ShouldRevive(UnitBase target)
    {
        return target.Stats.HP == 0;
    }

    private void ReviveUnit(UnitBase casterUnit, UnitBase target, SkillData skillData)
    {
        int amount = HealCalculator.CalculateHealAmount(target, skillData);
        target.Stats.Heal(amount);
        EffectView.ShowReviveEffect(casterUnit, target, amount);
    }
}