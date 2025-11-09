namespace Shin_Megami_Tensei
{
    public static class LightDarkCalculator
    {
        public static bool IsNeutralSuccess(UnitBase caster, UnitBase target, SkillData skill)
        {
            int casterLck = caster.Stats.Lck;
            int targetLck = target.Stats.Lck;
            int power = skill.Power;
            
            return casterLck + power >= targetLck;
        }

        public static bool IsResistSuccess(UnitBase caster, UnitBase target, SkillData skill)
        {
            int casterLck = caster.Stats.Lck;
            int targetLck = target.Stats.Lck;
            int power = skill.Power;

            return casterLck + power >= 2 * targetLck;
        }
    }
}