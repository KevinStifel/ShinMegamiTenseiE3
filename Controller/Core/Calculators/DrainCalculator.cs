namespace Shin_Megami_Tensei
{
    public static class DrainCalculator
    {
        public static (int HpDrain, int MpDrain) CalculateEnergyDrain(
            UnitBase caster, 
            UnitBase target, 
            SkillData skillData)
        {
            int baseDrain = CalculateBaseDrain(caster, skillData);
            int actualHpDrain = CombatMath.ClampAtMost(baseDrain, target.Stats.HP);
            int actualMpDrain = CombatMath.ClampAtMost(baseDrain, target.Stats.MP);
            
            return (actualHpDrain, actualMpDrain);
        }

        public static int CalculateLifeDrain(
            UnitBase caster, 
            UnitBase target, 
            SkillData skillData)
        {
            int baseDrain = CalculateBaseDrain(caster, skillData);
            return CombatMath.ClampAtMost(baseDrain, target.Stats.HP);
        }

        public static int CalculateSpiritDrain(
            UnitBase caster, 
            UnitBase target, 
            SkillData skillData)
        {
            int baseDrain = CalculateBaseDrain(caster, skillData);
            return CombatMath.ClampAtMost(baseDrain, target.Stats.MP);
        }
        
        private static int CalculateBaseDrain(UnitBase caster, SkillData skillData)
        {
            var baseDrain = CombatMath.SqrtOfStatTimesPower(caster.Stats.Mag, skillData.Power);
            return CombatMath.RoundDamageDown(baseDrain);        }
    }
}