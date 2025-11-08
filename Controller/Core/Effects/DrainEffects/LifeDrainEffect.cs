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

    public override void ApplyEffect(UnitBase caster, List<UnitBase> targets, SkillExecutionContext context)
    {
        _turnManager = context.TurnManager;
        _boardManager = context.BoardManager;
        _skillData = context.SkillData;
        _currentPlayerId = context.CurrentPlayerId;
        _enemyPlayerId = BattleHelper.GetEnemyPlayerId(_currentPlayerId);

        var affinityBehavior = GetAffinityBehavior(caster, _elementType);
        caster.Stats.UseMP(_skillData.Cost);

        foreach (var target in targets)
            ApplyLifeDrain(caster, target);

        var turnChange = _turnManager.ApplyAffinityTurnEffect(affinityBehavior);
        ActionView.ShowTurnConsumption(turnChange);
    }

    private void ApplyLifeDrain(UnitBase caster, UnitBase target)
    {
        int drainAmount = (int)Math.Sqrt(caster.Stats.Mag * _skillData.Power);
        int actualDrain = Math.Min(target.Stats.HP, drainAmount);

        target.Stats.TakeDamage(actualDrain);
        caster.Stats.Heal(actualDrain);

        EffectView.ShowHpDrainEffect(caster, target, actualDrain);

        if (target.Stats.HP == 0)
            _boardManager.HandleUnitDeath(_enemyPlayerId, target);
    }
}