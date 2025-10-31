﻿using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class DrainAffinityView : AffinityViewBase
{
    public DrainAffinityView(View view, AffinityElement element) : base(view, element) { }

    public override void ShowAffinityReaction(UnitBase casterUnit, UnitBase targetUnit, int damage)
    {
        View.WriteLine($"{casterUnit.Name} {AttackElementalVerb} a {targetUnit.Name}");
        View.WriteLine($"{targetUnit.Name} absorbe {damage} daño");
    }
    
    public override void ShowLightDarkReaction(UnitBase casterUnit, UnitBase targetUnit, SkillData skillData)
    {
        View.WriteLine($"{casterUnit.Name} {AttackElementalVerb} a {targetUnit.Name}");
    }
}