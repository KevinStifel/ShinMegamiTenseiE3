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
            int actualHpDrain = Math.Min(target.Stats.HP, baseDrain);
            int actualMpDrain = Math.Min(target.Stats.MP, baseDrain);
            
            return (actualHpDrain, actualMpDrain);
        }

        public static int CalculateLifeDrain(
            UnitBase caster, 
            UnitBase target, 
            SkillData skillData)
        {
            int baseDrain = CalculateBaseDrain(caster, skillData);
            return Math.Min(target.Stats.HP, baseDrain);
        }

        public static int CalculateSpiritDrain(
            UnitBase caster, 
            UnitBase target, 
            SkillData skillData)
        {
            int baseDrain = CalculateBaseDrain(caster, skillData);
            return Math.Min(target.Stats.MP, baseDrain);
        }
        
        private static int CalculateBaseDrain(UnitBase caster, SkillData skillData)
        {
            return (int)Math.Sqrt(caster.Stats.Mag * skillData.Power);
        }
    }
}