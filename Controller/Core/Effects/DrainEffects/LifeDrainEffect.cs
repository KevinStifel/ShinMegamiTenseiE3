using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class LifeDrainEffect : EffectBase
{
    private TurnManager _turnManager = null!;
    private BoardManager _boardManager = null!;
    private SkillData _skillData = null!;
    private int _currentPlayerId;
    private int _enemyPlayerId;
    private readonly AffinityElement _elementType = AffinityElement.Almighty;

    public LifeDrainEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase caster, 
        List<UnitBase> targets, 
        SkillExecutionContext context)
    {
        InitializeEffect(context);
        var affinityBehavior = GetAffinityBehavior(caster, _elementType);
        caster.Stats.UseMP(_skillData.Cost);

        foreach (var target in targets)
            ApplyLifeDrain(caster, target);

        var turnChange = ApplyTurnEffect(affinityBehavior);
        ActionView.ShowTurnConsumption(turnChange);
    }

    private void InitializeEffect(SkillExecutionContext context)
    {
        _turnManager = context.TurnManager;
        _boardManager = context.BoardManager;
        _skillData = context.SkillData;
        _currentPlayerId = context.CurrentPlayerId;
        _enemyPlayerId = BattleHelper.GetEnemyPlayerId(_currentPlayerId);
    }

    private void ApplyLifeDrain(UnitBase caster, UnitBase target)
    {
        int actualDrain = DrainCalculator.CalculateLifeDrain(caster, target, _skillData);

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
            _boardManager.HandleUnitDeath(_enemyPlayerId, target);
    }
            
    private TurnChange ApplyTurnEffect(AffinityBehavior affinityBehavior)
    {
        return _turnManager.ApplyAffinityTurnEffect(affinityBehavior);
    }
}