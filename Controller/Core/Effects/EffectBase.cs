using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public abstract class EffectBase
{
    protected readonly EffectView EffectView;
    protected readonly CombatActionView ActionView;
    protected readonly View View;

    protected BoardManager _boardManager = null!;
    protected TurnManager _turnManager = null!;
    protected SkillData _skillData = null!;
    protected int _currentPlayerId;
    protected int _enemyPlayerId;

    protected EffectBase(View view)
    {
        View = view;
        EffectView = new EffectView(view);
        ActionView = new CombatActionView(view);
    }

    public abstract void ApplyEffect(
        UnitBase casterUnit, 
        List<UnitBase> targets, 
        SkillExecutionContext skillExecutionContext);

    protected virtual void InitializeEffect(SkillExecutionContext context)
    {
        _boardManager = context.BoardManager;
        _turnManager = context.TurnManager;
        _skillData = context.SkillData;
        _currentPlayerId = context.CurrentPlayerId;
        _enemyPlayerId = BattleHelper.GetEnemyPlayerId(_currentPlayerId);
    }

    protected void ApplyTurnCost()
    {
        ApplyNeutralTurnChange(_turnManager);
    }

    protected void ApplyMpCost(UnitBase casterUnit)
    {
        casterUnit.Stats.UseMP(_skillData.Cost);
    }

    protected void ApplyNeutralTurnChange(TurnManager turnManager)
    {
        var turnChange = turnManager.ConsumeNeutralTurn();
        ActionView.ShowTurnConsumption(turnChange);
    }

    protected AffinityBehavior GetAffinityBehavior(UnitBase unit, AffinityElement elementType)
    {
        var affinityReaction = unit.Affinity.GetAffinityReaction(elementType);
        return AffinityBehaviorFactory.Create(affinityReaction);
    }
}