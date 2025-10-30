using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public class EffectView : AbstractView
{
    public EffectView(View view) : base(view) { }

    public void ShowHealEffect(UnitBase casterUnit, UnitBase targetUnit, int healAmount)
    {
        View.WriteLine($"{casterUnit.Name} cura a {targetUnit.Name}");
        View.WriteLine($"{targetUnit.Name} recibe {healAmount} de HP");
        View.WriteLine($"{targetUnit.Name} termina con HP:{targetUnit.Stats.HP}/{targetUnit.Stats.MaxHP}");
    }

    public void ShowReviveEffect(UnitBase casterUnit, UnitBase targetUnit, int healAmount)
    {
        View.WriteLine($"{casterUnit.Name} revive a {targetUnit.Name}");
        View.WriteLine($"{targetUnit.Name} recibe {healAmount} de HP");
        View.WriteLine($"{targetUnit.Name} termina con HP:{targetUnit.Stats.HP}/{targetUnit.Stats.MaxHP}");
    }
    
    public void ShowHpDrainEffect(UnitBase casterUnit, UnitBase targetUnit, int drainAmount)
    {
        View.WriteLine($"{casterUnit.Name} lanza un ataque todo poderoso a {targetUnit.Name}");
        View.WriteLine($"El ataque drena {drainAmount} HP de {targetUnit.Name}");
        View.WriteLine($"{targetUnit.Name} termina con HP:{targetUnit.Stats.HP}/{targetUnit.Stats.MaxHP}");
        View.WriteLine($"{casterUnit.Name} termina con HP:{casterUnit.Stats.HP}/{casterUnit.Stats.MaxHP}");
    }

    public void ShowMpDrainEffect(UnitBase casterUnit, UnitBase targetUnit, int drainAmount)
    {
        View.WriteLine($"{casterUnit.Name} lanza un ataque todo poderoso a {targetUnit.Name}");
        View.WriteLine($"El ataque drena {drainAmount} MP de {targetUnit.Name}");
        View.WriteLine($"{targetUnit.Name} termina con MP:{targetUnit.Stats.MP}/{targetUnit.Stats.MaxMP}");
        View.WriteLine($"{casterUnit.Name} termina con MP:{casterUnit.Stats.MP}/{casterUnit.Stats.MaxMP}");
    }

    public void ShowHpMpDrainEffect(UnitBase casterUnit, UnitBase targetUnit, int hpDrain, int mpDrain)
    {
        View.WriteLine($"{casterUnit.Name} lanza un ataque todo poderoso a {targetUnit.Name}");
        View.WriteLine($"El ataque drena {hpDrain} HP de {targetUnit.Name}");
        View.WriteLine($"{targetUnit.Name} termina con HP:{targetUnit.Stats.HP}/{targetUnit.Stats.MaxHP}");
        View.WriteLine($"{casterUnit.Name} termina con HP:{casterUnit.Stats.HP}/{casterUnit.Stats.MaxHP}");
        View.WriteLine($"El ataque drena {mpDrain} MP de {targetUnit.Name}");
        View.WriteLine($"{targetUnit.Name} termina con MP:{targetUnit.Stats.MP}/{targetUnit.Stats.MaxMP}");
        View.WriteLine($"{casterUnit.Name} termina con MP:{casterUnit.Stats.MP}/{casterUnit.Stats.MaxMP}");
    }

    public void ShowSummonResult(UnitBase targetUnit)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"{targetUnit.Name} ha sido invocado");
    }

    public void ShowSummonAndReviveEffect(UnitBase casterUnit, UnitBase targetUnit, int healAmount)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"{targetUnit.Name} ha sido invocado");
        View.WriteLine($"{casterUnit.Name} revive a {targetUnit.Name}");
        View.WriteLine($"{targetUnit.Name} recibe {healAmount} de HP");
        View.WriteLine($"{targetUnit.Name} termina con HP:{targetUnit.Stats.HP}/{targetUnit.Stats.MaxHP}");
    }
    
    public void ShowHpStatus(UnitBase unit)
    {
        View.WriteLine($"{unit.Name} termina con HP:{unit.Stats.HP}/{unit.Stats.MaxHP}");
    }
}
