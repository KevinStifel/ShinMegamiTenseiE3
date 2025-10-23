using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class EnergyDrainEffect : EffectBase
{
    private TurnManager _turnManager = null!;
    private BoardManager _boardManager = null!;
    private SkillData _skillData = null!;
    private int _currentPlayerId;
    private int _enemyPlayerId;
    private readonly AffinityElement _elementType = AffinityElement.Almighty;

    public EnergyDrainEffect(View view) : base(view) { }

    public override void ApplyEffect(UnitBase caster, List<UnitBase> targets, SkillExecutionContext context)
    {
        _turnManager = context.TurnManager;
        _boardManager = context.BoardManager;
        _skillData = context.SkillData;
        _currentPlayerId = context.CurrentPlayerId;
        _enemyPlayerId = BattleHelper.GetEnemyPlayerId(_currentPlayerId);

        var affinityBehavior = GetAffinityBehavior(caster, _elementType);

        foreach (var target in targets)
            ApplyEnergyDrain(caster, target);

        var turnChange = _turnManager.ApplyAffinityTurnEffect(affinityBehavior);
        ActionView.ShowTurnConsumption(turnChange);
    }

    private void ApplyEnergyDrain(UnitBase caster, UnitBase target)
    {
        int baseDrain = (int)Math.Sqrt(caster.Stats.Mag * _skillData.Power);
        int actualHpDrain = Math.Min(target.Stats.HP, baseDrain);
        int actualMpDrain = Math.Min(target.Stats.MP, baseDrain);

        target.Stats.TakeDamage(actualHpDrain);
        caster.Stats.Heal(actualHpDrain);

        target.Stats.UseMP(actualMpDrain);
        caster.Stats.RestoreMP(actualMpDrain);

        EffectView.ShowHpMpDrainEffect(caster, target, actualHpDrain, actualMpDrain);

        if (target.Stats.HP == 0)
            _boardManager.HandleUnitDeath(_enemyPlayerId, target);
    }
}