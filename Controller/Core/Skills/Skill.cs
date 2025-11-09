using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class Skill
{
    private readonly SkillData _skillData;
    private readonly EffectBase _effect;
    private readonly TargetSelectorBase _targetSelector;

    public Skill(SkillData skillData, EffectBase effect, TargetSelectorBase targetSelector)
    {
        _skillData = skillData;
        _effect = effect;
        _targetSelector = targetSelector;
    }
    
    public void Apply(int currentPlayerId, BoardManager boardManager, TurnManager turnManager)
    {
        var casterUnit = turnManager.GetAttackerOnTurn();
        var skillExecutionContext = new SkillExecutionContext(_skillData, boardManager, turnManager, currentPlayerId);

        var targetsReadOnly = _targetSelector.SelectTargetsReadOnly(casterUnit, currentPlayerId, _skillData);

        if (targetsReadOnly.Count == 0)
            throw new ActionCanceledException();

        boardManager.RegisterPlayerSkillCounter(currentPlayerId);

        var targets = new List<UnitBase>(targetsReadOnly);
        _effect.ApplyEffect(casterUnit, targets, skillExecutionContext);

        boardManager.IncrementSkillUseCount(currentPlayerId);
    }

}