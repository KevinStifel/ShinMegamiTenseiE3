using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SpiritDrainEffect : EffectBase
{
    private readonly AffinityElement _elementType = AffinityElement.Almighty;

    public SpiritDrainEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase caster, 
        List<UnitBase> targets, 
        SkillExecutionContext context)
    {
        InitializeEffect(context);

        var affinityBehavior = GetAffinityBehavior(caster, _elementType);
        caster.Stats.UseMP(SkillData.Cost);

        foreach (var target in targets)
            ApplyMpDrain(caster, target);

        var turnChange = TurnManager.ApplyAffinityTurnEffect(affinityBehavior);
        ActionView.ShowTurnConsumption(turnChange);
    }

    private void ApplyMpDrain(UnitBase caster, UnitBase target)
    {
        int actualDrain = DrainCalculator.CalculateSpiritDrain(caster, target, SkillData);

        ApplyDrainToStats(caster, target, actualDrain);
        EffectView.ShowMpDrainEffect(caster, target, actualDrain);
    }

    private static void ApplyDrainToStats(UnitBase caster, UnitBase target, int actualDrain)
    {
        target.Stats.UseMP(actualDrain);
        caster.Stats.RestoreMP(actualDrain);
    }
}