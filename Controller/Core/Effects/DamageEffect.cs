using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class DamageEffect : EffectBase
{
    private AffinityElement _elementType;
    private TurnManager _turnManager = null!;
    private BoardManager _boardManager = null!;
    private SkillData _skillData = null!;
    private int _currentPlayerId;
    private int _enemyPlayerId;

    public DamageEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase casterUnit,
        List<UnitBase> targetEnemyUnits,
        SkillExecutionContext skillExecutionContext)
    {
        _turnManager = skillExecutionContext.TurnManager;
        _boardManager = skillExecutionContext.BoardManager;
        _skillData = skillExecutionContext.SkillData;
        _currentPlayerId = skillExecutionContext.CurrentPlayerId;
        _enemyPlayerId = BattleHelper.GetEnemyPlayerId(_currentPlayerId);
        _elementType = AffinityMapper.Parse(_skillData.Type);

        UnitBase? previousTarget = null;
        string topPriorityAffinityReaction = AffinityPriorityHelper.GetTopPriorityReaction(targetEnemyUnits, _elementType);

        foreach (var (targetEnemyUnit, index) in targetEnemyUnits.Select((unit, index) => (unit, index)))
        {

            if (HasTargetChanged(previousTarget, targetEnemyUnit))
                ShowHp(casterUnit, previousTarget!);
            
            ApplyDamageToTarget(casterUnit, targetEnemyUnit);

            bool isLastHit = index == targetEnemyUnits.Count - 1;
            if (isLastHit)
                ShowHp(casterUnit, targetEnemyUnit);

            previousTarget = targetEnemyUnit;
            
        }

        var topAffinityBehavior = AffinityBehaviorFactory.Create(topPriorityAffinityReaction);
        ApplyTurnChange(topAffinityBehavior);
        
        var lastTarget = targetEnemyUnits.Last();
        HandleDeaths(casterUnit, lastTarget);
    }

    private void ApplyDamageToTarget(UnitBase casterUnit, UnitBase targetUnit)
    {
        var affinityBehavior = GetAffinityBehavior(targetUnit, _elementType);
        var affinityView = AffinityViewFactory.Create(affinityBehavior.Type, View, _elementType);
        int inflictedDamage = DamageCalculator.CalculateFinalDamageForSkill(casterUnit, _skillData, affinityBehavior);
        
        affinityBehavior.ApplyEffect(casterUnit, targetUnit, inflictedDamage);
        affinityView.ShowAffinityReaction(casterUnit, targetUnit, inflictedDamage);
    }

    private static bool HasTargetChanged(UnitBase? previousTarget, UnitBase currentTarget)
    {
        return previousTarget != null && previousTarget != currentTarget;
    }

    private void ShowHp(UnitBase casterUnit, UnitBase target)
    {
        var affinityBehavior = GetAffinityBehavior(target, _elementType);
        var affinityView = AffinityViewFactory.Create(affinityBehavior.Type, View, _elementType);
        affinityView.ShowHp(casterUnit, target);
    }

    private void ApplyTurnChange(AffinityBehavior affinityBehavior)
    {
        var turnChange = _turnManager.ApplyAffinityTurnEffect(affinityBehavior);
        ActionView.ShowTurnConsumption(turnChange);
    }
    
    private void HandleDeaths(UnitBase casterUnit, UnitBase lastTarget)
    {
        HandleDeath(_boardManager, _enemyPlayerId, lastTarget);
        HandleDeath(_boardManager, _currentPlayerId, casterUnit);
    }

    private static void HandleDeath(BoardManager boardManager, int playerId, UnitBase unit)
    {
        if (unit.Stats.HP == 0)
            boardManager.HandleUnitDeath(playerId, unit);
    }
}
