using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class EnergyDrainEffect : EffectBase
{
    private readonly AffinityElement _elementType = AffinityElement.Almighty;

    public EnergyDrainEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase caster, 
        List<UnitBase> targets, 
        SkillExecutionContext context)
    {
        InitializeEffect(context);

        var affinityBehavior = GetAffinityBehavior(caster, _elementType);
        caster.Stats.UseMP(SkillData.Cost);

        foreach (var target in targets)
            ApplyEnergyDrain(caster, target);
        
        var turnChange = TurnManager.ApplyAffinityTurnEffect(affinityBehavior);
        ActionView.ShowTurnConsumption(turnChange);
    }

    private void ApplyEnergyDrain(UnitBase caster, UnitBase target)
    {
        (int hpDrain, int mpDrain) = DrainCalculator.CalculateEnergyDrain(
            caster, target, SkillData);
        
        ApplyDrainToStats(caster, target, hpDrain, mpDrain);
        
        EffectView.ShowHpMpDrainEffect(caster, target, hpDrain, mpDrain);
        HandleDeath(target);
    }

    private static void ApplyDrainToStats(
        UnitBase caster, 
        UnitBase target, 
        int hpDrain, 
        int mpDrain)
    {
        target.Stats.TakeDamage(hpDrain);
        caster.Stats.Heal(hpDrain);

        target.Stats.UseMP(mpDrain);
        caster.Stats.RestoreMP(mpDrain);
    }

    private void HandleDeath(UnitBase target)
    {
        if (target.Stats.HP == 0)
            BoardManager.HandleUnitDeath(EnemyPlayerId, target);
    }
}