using System.Collections.Generic;
using System.Linq;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class UseSkillAction : CombatActionBase
{
    private const string PassiveSkillType = "Passive";

    public UseSkillAction(View view) : base(view) { }

    public override void ExecuteAction(
        int currentPlayerId, 
        BoardManager boardManager, 
        TurnManager turnManager)
    {
        var attackerUnit = turnManager.GetAttackerOnTurn();

        var selectedSkillData = PromptSkillSelection(attackerUnit);
        if (selectedSkillData == null)
            throw new ActionCanceledException();

        ValidateManaAvailability(attackerUnit, selectedSkillData);

        var skillInstance = CreateSkillInstance(selectedSkillData, boardManager);
        skillInstance.Apply(currentPlayerId, boardManager, turnManager);
    }

    private SkillData? PromptSkillSelection(UnitBase attackerUnit)
    {
        var availableSkills = GetUsableSkills(attackerUnit);
        ActionView.ShowAvailableSkills(attackerUnit, availableSkills);

        int selectedIndex = ActionView.ReadSkillIndexFromInput(availableSkills);
        return IsSelectionCanceled(selectedIndex) ? null : availableSkills[selectedIndex];
    }

    private static IReadOnlyList<SkillData> GetUsableSkills(UnitBase attackerUnit)
    {
        var allSkills = attackerUnit is Samurai samurai
            ? samurai.Skills
            : ((Monster)attackerUnit).Skills;

        var usableSkills = allSkills
            .Where(skill => IsUsableSkill(attackerUnit, skill));

        return usableSkills.ToList();
    }

    private static void ValidateManaAvailability(
        UnitBase attackerUnit, 
        SkillData selectedSkillData)
    {
        if (attackerUnit.Stats.MP < selectedSkillData.Cost)
            throw new ActionCanceledException();
    }

    private Skill CreateSkillInstance(
        SkillData selectedSkillData, 
        BoardManager boardManager)
    {
        return SkillFactory.Create(selectedSkillData, boardManager, View);
    }
    
    private static bool IsUsableSkill(UnitBase attackerUnit, SkillData skill)
    {
        bool hasEnoughMP = skill.Cost <= attackerUnit.Stats.MP;
        
        bool isActive = !string.Equals(
            skill.Type, 
            GameConstants.SkillTypePassive,
            StringComparison.OrdinalIgnoreCase);
        
        return hasEnoughMP && isActive;
    }
}