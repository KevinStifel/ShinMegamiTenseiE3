using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public abstract class EffectBase
{
    protected readonly EffectView EffectView;
    protected readonly CombatActionView ActionView;
    protected readonly View View;

    protected BoardManager BoardManager = null!;
    protected TurnManager TurnManager = null!;
    protected SkillData SkillData = null!;
    protected int CurrentPlayerId;
    protected int EnemyPlayerId;

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
        BoardManager    = context.BoardManager!;
        TurnManager     = context.TurnManager!;
        SkillData       = context.SkillData!;
        CurrentPlayerId = context.CurrentPlayerId;
        EnemyPlayerId   = BattleHelper.GetEnemyPlayerId(CurrentPlayerId);
    }

    protected void ApplyTurnCost()
    {
        ApplyNeutralTurnChange(TurnManager);
    }

    protected void ApplyMpCost(UnitBase casterUnit)
    {
        casterUnit.Stats.UseMP(SkillData.Cost);
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