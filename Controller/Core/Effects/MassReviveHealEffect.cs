using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class MassReviveHealEffect : EffectBase
{
    public MassReviveHealEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase casterUnit,
        List<UnitBase> targets,
        SkillExecutionContext skillExecutionContext)
    {
        var turnManager = skillExecutionContext.TurnManager;
        var skillData = skillExecutionContext.SkillData;

        foreach (var targetUnit in targets)
        {
            if (IsDead(targetUnit))
            {
                int healAmount = HealCalculator.CalculateHealAmount(targetUnit, skillData);
                Revive(targetUnit, healAmount);
                EffectView.ShowReviveEffect(casterUnit, targetUnit, healAmount);
            }
            else
            {
                int healAmount = HealCalculator.CalculateHealAmount(targetUnit, skillData);
                targetUnit.Stats.Heal(healAmount);
                EffectView.ShowHealEffect(casterUnit, targetUnit, healAmount);
            }
        }

        casterUnit.Stats.TakeDamage(casterUnit.Stats.MaxHP);
        EffectView.ShowHpStatus(casterUnit);

        ApplyNeutralTurnChange(turnManager);
    }

    private static bool IsDead(UnitBase unit) => unit.Stats.HP <= 0;

    private static void Revive(UnitBase unit, int healAmount)
    {
        unit.Stats.TakeDamage(unit.Stats.HP);
        unit.Stats.Heal(healAmount);
    }

}