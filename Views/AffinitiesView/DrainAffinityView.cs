using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class DrainAffinityView : AffinityViewBase
{
    public DrainAffinityView(View view, AffinityElement element) : base(view, element) { }

    public override void ShowAffinityReaction(UnitBase casterUnit, UnitBase targetUnit, int damage)
    {
        ShowAttackAnnouncement(casterUnit, targetUnit);
        ShowDamageAbsorbed(targetUnit, damage);
    }

    public override void ShowLightDarkReaction(UnitBase casterUnit, UnitBase targetUnit, SkillData skillData)
    {
        ShowAttackAnnouncement(casterUnit, targetUnit);
    }
}