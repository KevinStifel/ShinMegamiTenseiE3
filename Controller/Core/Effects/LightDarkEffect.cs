using System;
using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class LightDarkEffect : EffectBase
{
    private TurnManager _turnManager = null!;
    private BoardManager _boardManager = null!;
    private SkillData _skillData = null!;
    private int _currentPlayerId;
    private int _enemyPlayerId;
    private AffinityElement _elementType;

    public LightDarkEffect(View view) : base(view) { }

    public override void ApplyEffect(UnitBase casterUnit, List<UnitBase> targets, SkillExecutionContext context)
    {
        _turnManager = context.TurnManager;
        _boardManager = context.BoardManager;
        _skillData = context.SkillData;
        _currentPlayerId = context.CurrentPlayerId;
        _enemyPlayerId = BattleHelper.GetEnemyPlayerId(_currentPlayerId);
        _elementType = AffinityMapper.Parse(_skillData.Type);
        
        foreach (var target in targets)
            ApplyLightDarkToTarget(casterUnit, target);

        var topAffinity = AffinityPriorityHelper.GetTopPriorityReaction(targets, _elementType);
        Console.WriteLine($"[DEBUG] Top Affinity: {topAffinity}");

        var topBehavior = AffinityBehaviorFactory.Create(topAffinity);
        ApplyTurnChange(topBehavior);
        casterUnit.Stats.UseMP(_skillData.Cost);
    }

    private void ApplyLightDarkToTarget(UnitBase caster, UnitBase target)
    {
        var affinityBehavior = GetAffinityBehavior(target, _elementType);
        var affinityView = AffinityViewFactory.Create(affinityBehavior.Type, View, _elementType);
        affinityBehavior.ApplyLightDarkEffect(caster, target, _skillData);
        affinityView.ShowLightDarkReaction(caster, target, _skillData);
        EffectView.ShowHpStatus(target);

        if (target.Stats.HP == 0)
        {
            _boardManager.HandleUnitDeath(_enemyPlayerId, target);
        }

    }

    private void ApplyTurnChange(AffinityBehavior behavior)
    {
        var turnChange = _turnManager.ApplyAffinityTurnEffect(behavior);
        ActionView.ShowTurnConsumption(turnChange);
    }
}
