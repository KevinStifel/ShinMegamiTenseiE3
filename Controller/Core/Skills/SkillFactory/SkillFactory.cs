using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public static class SkillFactory
{
    public static Skill Create(SkillData skillData, BoardManager boardManager, View view)
    {
        string skillName = skillData.Name;
        
        if (skillName == "Lunge")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Oni-Kagura")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Mortal Jihad")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Gram Slice")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Fatal Sword")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Berserker God")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Bouncing Claw")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Damascus Claw")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Nihil Claw")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Axel Claw")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Iron Judgement")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Stigma Attack")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));
        
        if (skillName == "Scratch Dance")
            return new Skill(skillData, new DamageEffect(view), new MultiEnemySelector(view, boardManager));

        if (skillName == "Madness Nails")
            return new Skill(skillData, new DamageEffect(view), new MultiEnemySelector(view, boardManager));

        if (skillName == "Bar Toss")
            return new Skill(skillData, new DamageEffect(view), new MultiEnemySelector(view, boardManager));
        
        if (skillName == "Critical Wave")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Megaton Press")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Titanomachia")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Heat Wave")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Javelin Rain")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Hades Blast")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));
        
        if (skillName == "Needle Shot")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Tathlum Shot")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Grand Tack")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Riot Gun")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));
        
        if (skillName == "Myriad Arrows")
            return new Skill(skillData, new DamageEffect(view), new MultiEnemySelector(view, boardManager));
        
        if (skillName == "Rapid Needle")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Blast Arrow")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Heaven's Bow")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));
        
        if (skillName == "Agi")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Agilao")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Agidyne")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Trisagion")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));
        
        if (skillName == "Fire Breath")
            return new Skill(skillData, new DamageEffect(view), new MultiEnemySelector(view, boardManager));

        if (skillName == "Ragnarok")
           return new Skill(skillData, new DamageEffect(view), new MultiEnemySelector(view, boardManager));
        
        if (skillName == "Maragi")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Maragion")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Maragidyne")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));
  
        if (skillName == "Bufu")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Bufula")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Bufudyne")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));
        
        if (skillName == "Ice Breath")
            return new Skill(skillData, new DamageEffect(view), new MultiEnemySelector(view, boardManager));

        if (skillName == "Glacial Blast")
            return new Skill(skillData, new DamageEffect(view), new MultiEnemySelector(view, boardManager));

        if (skillName == "Breath")
            return new Skill(skillData, new DamageEffect(view), new MultiEnemySelector(view, boardManager));

        if (skillName == "Refrigerate")
            return new Skill(skillData, new DamageEffect(view), new MultiEnemySelector(view, boardManager));
   
        if (skillName == "Mabufu")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Mabufula")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Mabufudyne")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));
        
        if (skillName == "Zio")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Zionga")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Ziodyne")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));
        
        if (skillName == "Shock")
            return new Skill(skillData, new DamageEffect(view), new MultiEnemySelector(view, boardManager));

        if (skillName == "Plasma Discharge")
            return new Skill(skillData, new DamageEffect(view), new MultiEnemySelector(view, boardManager));
        
        if (skillName == "Mazio")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Mazionga")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Maziodyne")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Thunder Reign")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));
        
        if (skillName == "Zan")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Zanma")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Zandyne")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Deadly Wind")
            return new Skill(skillData, new DamageEffect(view), new EnemySelector(view, boardManager));
        
        if (skillName == "Wind Breath")
            return new Skill(skillData, new DamageEffect(view), new MultiEnemySelector(view, boardManager));

        if (skillName == "Floral Gust")
            return new Skill(skillData, new DamageEffect(view), new MultiEnemySelector(view, boardManager));
        
        if (skillName == "Mazan")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Mazanma")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));
        
        if (skillName == "Mazandyne")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));
        
        if (skillName == "Hama")
            return new Skill(skillData, new LightDarkEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Hamaon")
            return new Skill(skillData, new LightDarkEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Mahama")
            return new Skill(skillData, new LightDarkEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Mahamaon")
            return new Skill(skillData, new LightDarkEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Judgement Light")
            return new Skill(skillData, new LightDarkEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Mudo")
            return new Skill(skillData, new LightDarkEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Mudoon")
            return new Skill(skillData, new LightDarkEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Mamudo")
            return new Skill(skillData, new LightDarkEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Mamudoon")
            return new Skill(skillData, new LightDarkEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Die for Me!")
            return new Skill(skillData, new LightDarkEffect(view), new AllEnemySelector(view, boardManager));
        
        if (skillName == "Life Drain")
            return new Skill(skillData, new LifeDrainEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Spirit Drain")
            return new Skill(skillData, new SpiritDrainEffect(view), new EnemySelector(view, boardManager));

        if (skillName == "Energy Drain")
            return new Skill(skillData, new EnergyDrainEffect(view), new EnemySelector(view, boardManager));
        
        if (skillName == "Desperate Hit")
            return new Skill(skillData, new DamageEffect(view), new MultiEnemySelector(view, boardManager));

        if (skillName == "Megido")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Megidola")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Megidolaon")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Great Logos")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Holy Wrath")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Judgement")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Sea of Chaos")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Megidoplasma")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Shining Seal")
            return new Skill(skillData, new DamageEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Serpent of Sheol")
            return new Skill(skillData, new LifeDrainEffect(view), new AllEnemySelector(view, boardManager));

        if (skillName == "Dia")
            return new Skill(skillData, new HealEffect(view), new AllySelector(view, boardManager));

        if (skillName == "Diarama")
            return new Skill(skillData, new HealEffect(view), new AllySelector(view, boardManager));

        if (skillName == "Diarahan")
            return new Skill(skillData, new HealEffect(view), new AllySelector(view, boardManager));

        if (skillName == "Media")
            return new Skill(skillData, new HealEffect(view), new PartySelector(view, boardManager));

        if (skillName == "Mediarama")
            return new Skill(skillData, new HealEffect(view), new PartySelector(view, boardManager));

        if (skillName == "Mediarahan")
            return new Skill(skillData, new HealEffect(view), new PartySelector(view, boardManager));

        if (skillName == "Recarmdra")
            return new Skill(skillData, new MassReviveHealEffect(view), new MassReviveAllySelector(view, boardManager));
  
        if (skillName == "Recarm")
            return new Skill(skillData, new ReviveEffect(view), new DeadAllySelector(view, boardManager));

        if (skillName == "Samarecarm")
            return new Skill(skillData, new ReviveEffect(view), new DeadAllySelector(view, boardManager));
        
        if (skillName == "Invitation")
            return new Skill(skillData, new InvitationEffect(view), new ReserveSelectorAll(view, boardManager));

        if (skillName == "Sabbatma")
            return new Skill(skillData, new SpecialEffect(view), new SpecialSelector(view, boardManager));

        throw new SkillNotFoundException($"Skill '{skillData.Name}' no está implementada en SkillFactory.");    }
}
