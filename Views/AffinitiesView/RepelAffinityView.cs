using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class RepelAffinityView : AffinityViewBase
{
    public RepelAffinityView(View view, AffinityElement element) : base(view, element) { }

    public override void ShowAffinityReaction(UnitBase casterUnit, UnitBase targetUnit, int damage)
    {
        ShowAttackAnnouncement(casterUnit, targetUnit);
        ShowDamageRepelled(casterUnit, targetUnit, damage);
    }

    public override void ShowHp(UnitBase attackerUnit, UnitBase targetUnit)
    {
        ShowHpStatusLine(attackerUnit);
    }

    public override void ShowLightDarkReaction(UnitBase casterUnit, UnitBase targetUnit, SkillData skillData)
    {
        ShowAttackAnnouncement(casterUnit, targetUnit);
        ShowUnitEliminated(casterUnit);
    }
}