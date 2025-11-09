using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SpiritDrainEffect : EffectBase
{
    private TurnManager _turnManager = null!;
    private BoardManager _boardManager = null!;
    private SkillData _skillData = null!;
    private int _currentPlayerId;
    private int _enemyPlayerId;
    private readonly AffinityElement _elementType = AffinityElement.Almighty;

    public SpiritDrainEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase caster, 
        List<UnitBase> targets, 
        SkillExecutionContext context)
    {
        InitializeEffect(context);
        var affinityBehavior = GetAffinityBehavior(caster, _elementType);
        caster.Stats.UseMP(_skillData.Cost);

        foreach (var target in targets)
            ApplyMpDrain(caster, target);

        var turnChange = ApplyTurnEffect(affinityBehavior);
        ActionView.ShowTurnConsumption(turnChange);
    }

    protected override void InitializeEffect(SkillExecutionContext context)
    {
        _turnManager = context.TurnManager;
        _boardManager = context.BoardManager;
        _skillData = context.SkillData;
        _currentPlayerId = context.CurrentPlayerId;
        _enemyPlayerId = BattleHelper.GetEnemyPlayerId(_currentPlayerId);
    }

    private void ApplyMpDrain(UnitBase caster, UnitBase target)
    {
        int actualDrain = DrainCalculator.CalculateSpiritDrain(caster, target, _skillData);

        ApplyDrainToStats(caster, target, actualDrain);
        EffectView.ShowMpDrainEffect(caster, target, actualDrain);
    }

    private static void ApplyDrainToStats(UnitBase caster, UnitBase target, int actualDrain)
    {
        target.Stats.UseMP(actualDrain);
        caster.Stats.RestoreMP(actualDrain);
    }
            
    private TurnChange ApplyTurnEffect(AffinityBehavior affinityBehavior)
    {
        return _turnManager.ApplyAffinityTurnEffect(affinityBehavior);
    }
}