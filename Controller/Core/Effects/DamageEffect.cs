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

        var repelGroupEndIndexes = AffinityGroupHelper.GetLastIndexesOfRepelGroups(targetEnemyUnits, _elementType);

        for (int index = 0; index < targetEnemyUnits.Count; index++)
        {
            var currentTargetUnit = targetEnemyUnits[index];

            // ANALIZAR CADENA DOBLE IF --- Solución:
            if (previousTarget != null && HasTargetChanged(previousTarget, currentTargetUnit))
            {
                var previousAffinityBehavior = GetAffinityBehavior(previousTarget, _elementType);
                
                if (HasTargetChanged(previousTarget, currentTargetUnit) &&
                    previousAffinityBehavior.ShouldShowHpAfterAttack())
                {
                    ShowHp(casterUnit, previousTarget!);
                }
            }

            ApplyDamageToTarget(casterUnit, currentTargetUnit);
            HandleDeaths(casterUnit, currentTargetUnit);

            bool isLastHit = index == targetEnemyUnits.Count - 1;
            bool isEndOfRepelGroup = repelGroupEndIndexes.Contains(index);

            if (isEndOfRepelGroup)
                ShowHp(casterUnit, casterUnit);

            if (isLastHit)
            {
                var currentBehavior = GetAffinityBehavior(currentTargetUnit, _elementType);
                if (currentBehavior.ShouldShowHpAfterAttack())
                    ShowHp(casterUnit, currentTargetUnit);
            }

            previousTarget = currentTargetUnit;
        }
        
        var topAffinityBehavior = AffinityBehaviorFactory.Create(topPriorityAffinityReaction);
        ApplyTurnChange(topAffinityBehavior);
        casterUnit.Stats.UseMP(_skillData.Cost);
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

    private void HandleDeaths(UnitBase casterUnit, UnitBase targetUnit)
    {
        HandleDeath(_enemyPlayerId, targetUnit);
        HandleDeath(_currentPlayerId, casterUnit);
    }

    private void HandleDeath(int playerId, UnitBase unit)
    {
        if (unit.Stats.HP == 0)
            _boardManager.HandleUnitDeath(playerId, unit);
    }
}
