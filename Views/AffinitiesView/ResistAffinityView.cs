using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class ResistAffinityView : AffinityViewBase
{
    public ResistAffinityView(View view, AffinityElement element) : base(view, element) { }

    public override void ShowAffinityReaction(UnitBase casterUnit, UnitBase targetUnit, int damage)
    {
        View.WriteLine($"{casterUnit.Name} {AttackElementalVerb} a {targetUnit.Name}");
        View.WriteLine($"{targetUnit.Name} es resistente el ataque de {casterUnit.Name}");
        View.WriteLine($"{targetUnit.Name} recibe {damage} de daño");
    }
    
    public override void ShowLightDarkReaction(UnitBase casterUnit, UnitBase targetUnit, SkillData skillData)
    {
        View.WriteLine($"{casterUnit.Name} {AttackElementalVerb} a {targetUnit.Name}");

        if (targetUnit.Stats.HP == 0)
        {
            View.WriteLine($"{targetUnit.Name} es resistente el ataque de {casterUnit.Name}");
            View.WriteLine($"{targetUnit.Name} ha sido eliminado");
        }
        else
        {
            View.WriteLine($"{casterUnit.Name} ha fallado el ataque");
        }
    }
}