using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class MassReviveHealEffect : EffectBase
{
    private new BoardManager _boardManager = null!;
    private new TurnManager _turnManager = null!;
    private new SkillData _skillData = null!;
    private new int _currentPlayerId;

    public MassReviveHealEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase casterUnit,
        List<UnitBase> targets,
        SkillExecutionContext skillExecutionContext)
    {
        InitializeEffectContext(skillExecutionContext);

        ProcessTargets(casterUnit, targets);
        ApplySelfSacrifice(casterUnit);
        
        ApplyTurnCost();
        ApplyMpCost(casterUnit);
    }

    private void InitializeEffectContext(SkillExecutionContext context)
    {
        _boardManager = context.BoardManager;
        _turnManager = context.TurnManager;
        _skillData = context.SkillData;
        _currentPlayerId = context.CurrentPlayerId;
    }

    private void ProcessTargets(UnitBase casterUnit, List<UnitBase> targets)
    {
        foreach (var targetUnit in targets)
        {
            int healAmount = HealCalculator.CalculateHealAmount(targetUnit, _skillData);
            bool isTargetDead = IsDead(targetUnit);

            if (isTargetDead)
            {
                ReviveTarget(casterUnit, targetUnit, healAmount);
            }
            else
            {
                HealTarget(casterUnit, targetUnit, healAmount);
            }
        }
    }
    
    private void ReviveTarget(UnitBase casterUnit, UnitBase targetUnit, int healAmount)
    {
        ReviveUnit(targetUnit, healAmount);
        EffectView.ShowReviveEffect(casterUnit, targetUnit, healAmount);
    }

    private void HealTarget(UnitBase casterUnit, UnitBase targetUnit, int healAmount)
    {
        targetUnit.Stats.Heal(healAmount);
        EffectView.ShowHealEffect(casterUnit, targetUnit, healAmount);
    }

    private void ApplySelfSacrifice(UnitBase casterUnit)
    {
        ApplySelfDamage(casterUnit);
        UpdateBoardAndTurnOrder(casterUnit);
    }

    private void ApplySelfDamage(UnitBase casterUnit)
    {
        casterUnit.Stats.TakeDamage(casterUnit.Stats.MaxHP);
        EffectView.ShowHpStatus(casterUnit);
    }

    private void UpdateBoardAndTurnOrder(UnitBase casterUnit)
    {
        _boardManager.HandleUnitDeath(_currentPlayerId, casterUnit);
        _turnManager.SyncWithBoard(_boardManager, _currentPlayerId);
    }
    
    private static bool IsDead(UnitBase unit) => unit.Stats.HP <= 0;

    private static void ReviveUnit(UnitBase unit, int healAmount)
    {
        unit.Stats.TakeDamage(unit.Stats.HP);
        unit.Stats.Heal(healAmount);
    }
    
}