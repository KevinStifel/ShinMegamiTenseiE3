using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class LightDarkEffect : EffectBase
{
    private AffinityElement _elementType;

    public LightDarkEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase casterUnit, 
        List<UnitBase> targets, 
        SkillExecutionContext context)
    {
        InitializeEffect(context);
        _elementType = AffinityMapper.Parse(SkillData.Type);
        
        foreach (var target in targets)
            ApplyLightDarkToTarget(casterUnit, target);

        ApplyAffinityTurnCost(targets);
        ApplyMpCost(casterUnit);
    }

    private void ApplyLightDarkToTarget(UnitBase caster, UnitBase target)
    {
        var affinityBehavior = GetAffinityBehavior(target, _elementType);
        var affinityView = AffinityViewFactory.Create(affinityBehavior.Type, View, _elementType);
        
        affinityBehavior.ApplyLightDarkEffect(caster, target, SkillData);
        
        affinityView.ShowLightDarkReaction(caster, target, SkillData);
        EffectView.ShowHpStatus(target);

        HandleDeath(target);
    }

    private void HandleDeath(UnitBase target)
    {
        bool isTargetDead = target.Stats.HP == 0;
        if (isTargetDead)
        {
            BoardManager.HandleUnitDeath(EnemyPlayerId, target);
        }
    }

    private void ApplyAffinityTurnCost(List<UnitBase> targets)
    {
        var topAffinity = AffinityPriorityHelper.GetTopPriorityReaction(targets, _elementType);
        var topBehavior = AffinityBehaviorFactory.Create(topAffinity);
        
        var turnChange = TurnManager.ApplyAffinityTurnEffect(topBehavior);
        ActionView.ShowTurnConsumption(turnChange);
    }
}