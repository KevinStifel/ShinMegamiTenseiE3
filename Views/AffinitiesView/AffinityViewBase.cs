using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public abstract class AffinityViewBase
{
    private readonly View _view;
    private readonly string _attackElementalVerb;

    protected AffinityViewBase(View view, AffinityElement element)
    {
        _view = view;
        _attackElementalVerb = ElementMessageHelper.GetElementalMessage(element);
    }

    public abstract void ShowAffinityReaction(UnitBase casterUnit, UnitBase targetUnit, int damage);

    public virtual void ShowHp(UnitBase casterUnit, UnitBase targetUnit)
    {
        ShowHpStatusLine(targetUnit);
    } 
    
    protected void ShowHpStatusLine(UnitBase unit)
    {
        _view.WriteLine($"{unit.Name} termina con HP:{unit.Stats.HP}/{unit.Stats.MaxHP}");
    }

    public virtual void ShowLightDarkReaction(UnitBase casterUnit, UnitBase targetUnit, SkillData skillData) { }

    protected void ShowAttackAnnouncement(UnitBase casterUnit, UnitBase targetUnit)
    {
        _view.WriteLine($"{casterUnit.Name} {_attackElementalVerb} a {targetUnit.Name}");
    }

    protected void ShowDamageTaken(UnitBase targetUnit, int damage)
    {
        _view.WriteLine($"{targetUnit.Name} recibe {damage} de daño");
    }

    protected void ShowAttackBlocked(UnitBase casterUnit, UnitBase targetUnit)
    {
        _view.WriteLine($"{targetUnit.Name} bloquea el ataque de {casterUnit.Name}");
    }

    protected void ShowUnitEliminated(UnitBase targetUnit)
    {
        _view.WriteLine($"{targetUnit.Name} ha sido eliminado");
    }

    protected void ShowAttackMissed(UnitBase casterUnit)
    {
        _view.WriteLine($"{casterUnit.Name} ha fallado el ataque");
    }

    protected void ShowResistanceNotice(UnitBase casterUnit, UnitBase targetUnit)
    {
        _view.WriteLine($"{targetUnit.Name} es resistente el ataque de {casterUnit.Name}");
    }

    protected void ShowWeaknessNotice(UnitBase casterUnit, UnitBase targetUnit)
    {
        _view.WriteLine($"{targetUnit.Name} es débil contra el ataque de {casterUnit.Name}");
    }

    protected void ShowDamageAbsorbed(UnitBase targetUnit, int damage)
    {
        _view.WriteLine($"{targetUnit.Name} absorbe {damage} daño");
    }

    protected void ShowDamageRepelled(UnitBase casterUnit, UnitBase targetUnit, int damage)
    {
        _view.WriteLine($"{targetUnit.Name} devuelve {damage} daño a {casterUnit.Name}");
    }
    
    protected bool IsTargetAlive(UnitBase targetUnit)
    {
        return targetUnit.Stats.HP > 0;
    }

}
