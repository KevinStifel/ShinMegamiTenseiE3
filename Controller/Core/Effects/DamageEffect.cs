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

        foreach (var (targetEnemyUnit, index) in targetEnemyUnits.Select((unit, index) => (unit, index)))
        {
            ApplyDamageToTarget(casterUnit, targetEnemyUnit);

            if (HasTargetChanged(previousTarget, targetEnemyUnit))
                ShowHp(casterUnit, previousTarget!);

            bool isLastHit = index == targetEnemyUnits.Count - 1;
            if (isLastHit)
                ShowHp(casterUnit, targetEnemyUnit);

            previousTarget = targetEnemyUnit;
        }

        var lastTarget = targetEnemyUnits.Last();
        ApplyTurnChange(lastTarget);
        HandleDeaths(casterUnit, lastTarget);
    }

    private void ApplyDamageToTarget(UnitBase casterUnit, UnitBase target)
    {
        var affinityBehavior = GetAffinityBehavior(target);
        var affinityView = AffinityViewFactory.Create(affinityBehavior.Type, View, _elementType);

        int inflictedDamage = DamageCalculator.CalculateFinalDamageForSkill(casterUnit, _skillData, affinityBehavior);
        affinityBehavior.ApplyEffect(casterUnit, target, inflictedDamage);
        affinityView.ShowAffinityReaction(casterUnit, target, inflictedDamage);
    }

    private static bool HasTargetChanged(UnitBase? previousTarget, UnitBase currentTarget)
    {
        return previousTarget != null && previousTarget != currentTarget;
    }

    private void ShowHp(UnitBase casterUnit, UnitBase target)
    {
        var affinityBehavior = GetAffinityBehavior(target);
        var affinityView = AffinityViewFactory.Create(affinityBehavior.Type, View, _elementType);
        affinityView.ShowHp(casterUnit, target);
    }

    private void ApplyTurnChange(UnitBase lastTarget)
    {
        var affinityBehavior = GetAffinityBehavior(lastTarget);
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

    private AffinityBehavior GetAffinityBehavior(UnitBase target)
    {
        var affinityReaction = target.Affinity.GetAffinityReaction(_elementType);
        return AffinityBehaviorFactory.Create(affinityReaction);
    }
}
