using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class LifeDrainEffect : EffectBase
{
    private readonly AffinityElement _elementType = AffinityElement.Almighty;

    public LifeDrainEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase caster, 
        List<UnitBase> targets, 
        SkillExecutionContext context)
    {
        InitializeEffect(context);

        var affinityBehavior = GetAffinityBehavior(caster, _elementType);
        caster.Stats.UseMP(SkillData.Cost);

        foreach (var target in targets)
            ApplyLifeDrain(caster, target);

        var turnChange = TurnManager.ApplyAffinityTurnEffect(affinityBehavior);
        ActionView.ShowTurnConsumption(turnChange);
    }

    private void ApplyLifeDrain(UnitBase caster, UnitBase target)
    {
        int actualDrain = DrainCalculator.CalculateLifeDrain(caster, target, SkillData);

        ApplyDrainToStats(caster, target, actualDrain);
        EffectView.ShowHpDrainEffect(caster, target, actualDrain);
        HandleDeath(target);
    }

    private static void ApplyDrainToStats(UnitBase caster, UnitBase target, int actualDrain)
    {
        target.Stats.TakeDamage(actualDrain);
        caster.Stats.Heal(actualDrain);
    }

    private void HandleDeath(UnitBase target)
    {
        if (target.Stats.HP == 0)
            BoardManager.HandleUnitDeath(EnemyPlayerId, target);
    }
}