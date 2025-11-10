using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class DamageEffect : EffectBase
{
    private AffinityElement _elementType;

    public DamageEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase casterUnit,
        List<UnitBase> targetEnemyUnits,
        SkillExecutionContext skillExecutionContext)
    {
        InitializeEffect(skillExecutionContext);
        _elementType = AffinityMapper.Parse(SkillData.Type);

        string topPriorityReaction =
            AffinityPriorityHelper.GetTopPriorityReaction(targetEnemyUnits, _elementType);
        var repelGroupEndIndexes =
            AffinityGroupHelper.GetLastIndexesOfRepelGroups(targetEnemyUnits, _elementType);

        ProcessDamageHits(casterUnit, targetEnemyUnits, repelGroupEndIndexes);
        
        var topAffinityBehavior = AffinityBehaviorFactory.Create(topPriorityReaction);
        ApplyTurnChange(topAffinityBehavior);
        casterUnit.Stats.UseMP(SkillData.Cost);
    }

    private void ProcessDamageHits(
        UnitBase casterUnit,
        List<UnitBase> targetEnemyUnits,
        HashSet<int> repelGroupEndIndexes)
    {
        UnitBase? previousTarget = null;
        int totalHits = targetEnemyUnits.Count;

        for (int index = 0; index < totalHits; index++)
        {
            var currentTargetUnit = targetEnemyUnits[index];

            HandlePreviousTargetDisplay(casterUnit, previousTarget, currentTargetUnit);

            ApplyDamageToTarget(casterUnit, currentTargetUnit);
            HandleDeaths(casterUnit, currentTargetUnit);

            HandleRepelGroupDisplay(casterUnit, repelGroupEndIndexes, index);

            bool isLastHit = index == totalHits - 1;
            if (isLastHit)
            {
                HandleLastHitDisplay(casterUnit, currentTargetUnit);
            }

            previousTarget = currentTargetUnit;
        }
    }

    private void HandlePreviousTargetDisplay(
        UnitBase casterUnit,
        UnitBase? previousTarget,
        UnitBase currentTargetUnit)
    {
        if (HasTargetChanged(previousTarget, currentTargetUnit))
        {
            var previousAffinityBehavior = GetAffinityBehavior(previousTarget!, _elementType);
        
            if (previousAffinityBehavior.ShouldShowHpAfterAttack())
            {
                ShowHp(casterUnit, previousTarget!);
            }
        }
    }

    private void HandleRepelGroupDisplay(UnitBase casterUnit, HashSet<int> repelGroupEndIndexes, int index)
    {
        bool isEndOfRepelGroup = repelGroupEndIndexes.Contains(index);
        if (isEndOfRepelGroup)
            ShowHp(casterUnit, casterUnit);
    }

    private void HandleLastHitDisplay(UnitBase casterUnit, UnitBase currentTargetUnit)
    {
        var currentBehavior = GetAffinityBehavior(currentTargetUnit, _elementType);
        if (currentBehavior.ShouldShowHpAfterAttack())
            ShowHp(casterUnit, currentTargetUnit);
    }

    private void ApplyDamageToTarget(UnitBase casterUnit, UnitBase targetUnit)
    {
        var affinityBehavior = GetAffinityBehavior(targetUnit, _elementType);
        var affinityView = AffinityViewFactory.Create(affinityBehavior.Type, View, _elementType);
        
        int inflictedDamage = DamageCalculator.CalculateFinalDamageForSkill(
            casterUnit, SkillData, affinityBehavior);

        affinityBehavior.ApplyEffect(casterUnit, targetUnit, inflictedDamage);
        affinityView.ShowAffinityReaction(casterUnit, targetUnit, inflictedDamage);
    }

    private static bool HasTargetChanged(UnitBase? previousTarget, UnitBase currentTarget)
    {
        return previousTarget is not null && previousTarget != currentTarget;
    }

    private void ShowHp(UnitBase casterUnit, UnitBase target)
    {
        var affinityBehavior = GetAffinityBehavior(target, _elementType);
        var affinityView = AffinityViewFactory.Create(affinityBehavior.Type, View, _elementType);
        affinityView.ShowHp(casterUnit, target);
    }

    private void ApplyTurnChange(AffinityBehavior affinityBehavior)
    {
        var turnChange = TurnManager.ApplyAffinityTurnEffect(affinityBehavior);
        ActionView.ShowTurnConsumption(turnChange);
    }

    private void HandleDeaths(UnitBase casterUnit, UnitBase targetUnit)
    {
        HandleDeath(EnemyPlayerId, targetUnit);
        HandleDeath(CurrentPlayerId, casterUnit);
    }

    private void HandleDeath(int playerId, UnitBase unit)
    {
        if (unit.Stats.HP == 0)
            BoardManager.HandleUnitDeath(playerId, unit);
    }
}
