namespace Assets.GameClasses
{
    public class Stats
    {
        // Temp attribute definition
        public int StatPoints = 2;
        //public int StatPoints { get; set; }
        public int SkillPoints { get; set; }
        public int Experience { get; set; }
        public int Str { get; set; }
        public int Intl { get; set; }
        public int Agi { get; set; }
        public int Wis { get; set; }
        public int Vit { get; set; }
        public int Dex { get; set; }
        public int HitPoints { get; set; }
        public int CurHP { get; set; }
        public int Dmg { get; set; }
        public int AbilityPoints { get; set; }
        public int Speed { get; set; }
        public int Defense { get; set; }
        public int MagicDefense { get; set; }
        public int MagicAttack { get; set; }
        public int MagicPoints { get; set; }
        public int CurMP { get; set; }
        public int HitRate { get; set; }
        public int CritRate { get; set; }
        public int DodgeRate { get; set; }
        public int Luck { get; set; }

        public Stats(int strength = 0, int agility = 0, int intelligence = 0, int vitality = 0, int wisdom = 0, int dexterity = 0,
            int luck = 0, int hitPoints = 0, int damage = 0, int magicDmg = 0, int speed = 0, int defense = 0, int magicDef = 0, 
            int hitRate = 0, int dodgeRate = 0, int critRate = 0, int statPoints = 0, int skillPoints = 0)
        {
            Str = strength;
            Agi = agility;
            Intl = intelligence;
            Vit = vitality;
            Wis = wisdom;
            Dex = dexterity;
            Luck = luck;
            HitPoints = hitPoints;
            Dmg = damage;
            MagicAttack = magicDmg;
            Speed = speed;
            Defense = defense;
            MagicDefense = magicDef;
            HitRate = hitRate;
            DodgeRate = dodgeRate;
            CritRate = critRate;
            StatPoints = statPoints;
            SkillPoints = skillPoints;
        }

        public Stats Clone()
        {
            return new Stats(
                Str,
                Agi,
                Intl,
                Vit,
                Wis,
                Dex,
                Luck,
                HitPoints,
                Dmg,
                MagicAttack,
                Speed,
                Defense,
                MagicDefense,
                HitRate,
                DodgeRate,
                CritRate,
                StatPoints,
                SkillPoints);
        }

        public int CalculateHp(int level) {
            return (level * 5) + (Vit * 10) + (Str * 2);
            /*
            HitPoints = CalculateHp(level);
            M = calculateMP(level);
            damage = calculateDamage(level);
            magicDmg = calculateMagicDamage(level);
            hitRate = calculateSpeed(level);
            defense = calculateDefense(level);
            magicDef = calculateMagicDefense(level);
            hitRate = calculateHitRate(level);
            dodgeRate = calculateDodgeRate(level);
            critRate = calculateCritRate(level);
            */
        }

        public int CalculateMp(int level)
        {
            return (level * 5) + (Intl * 5) + (Wis * 10); 
        }

        public int CalculateDamage(int level)
        {
            return (level * 2) + (Str * 6) + (Dex * 2);
        }

        public int CalculateMagicDamage(int level)
        {
            return level + (Intl * 8) + Wis; 
        }

        public int CalculateSpeed(int level)
        {
            return 5 + (level / 15) + (Agi / 20);
        }

        public int CalculateDefense(int level)
        {
            return (level * 2) + (Vit * 6) + Str;
        }

        public int CalculateMagicDefense(int level)
        {
            return (level * 3) + (Wis * 5) + Intl;
        }

        public int CalculateHitRate(int level)
        {
            return 50 + (level * 2) + (Dex * 6) + (Wis * 2);
        }

        public int CalculateDodgeRate(int level)
        {
            return 50 + (level * 2) + (Agi * 6);
        }

        public int CalculateCritRate(int level)
        {
            return 35 + (level * 2) + (Luck * 3);
        }

        public Stats RemoveAlteredStats(Stats charStats, Stats itemStats)
        {
            charStats.Str = charStats.Str - itemStats.Str;
            charStats.Agi = charStats.Agi - itemStats.Agi;
            charStats.Intl = charStats.Intl - itemStats.Intl;
            charStats.Vit = charStats.Vit - itemStats.Vit;
            charStats.Dex = charStats.Dex - itemStats.Dex;
            charStats.Wis = charStats.Wis - itemStats.Wis;
            charStats.Luck = charStats.Luck - itemStats.Luck;
            return charStats;
        }
        public Stats ConcatStats(Stats charStats, Stats itemStats)
        {
            var alteredStats = new Stats();
            alteredStats.Str = charStats.Str + itemStats.Str;
            alteredStats.Agi = charStats.Agi + itemStats.Agi;
            alteredStats.Intl = charStats.Intl + itemStats.Intl;
            alteredStats.Vit = charStats.Vit + itemStats.Vit;
            alteredStats.Dex = charStats.Dex + itemStats.Dex;
            alteredStats.Wis = charStats.Wis + itemStats.Wis;
            alteredStats.Luck = charStats.Luck + itemStats.Luck;
            return alteredStats;
        }

        public void BuildCurrentStats(Character character)
        {
            HitPoints = CalculateHp(character.LevelId);
            MagicPoints = CalculateMp(character.LevelId);
            Dmg = CalculateDamage(character.LevelId);
            MagicAttack = CalculateMagicDamage(character.LevelId);
            Speed = CalculateSpeed(character.LevelId);
            Defense = CalculateDefense(character.LevelId);
            MagicDefense = CalculateMagicDefense(character.LevelId);
            HitRate = CalculateHitRate(character.LevelId);
            DodgeRate = CalculateDodgeRate(character.LevelId);
            CritRate = CalculateCritRate(character.LevelId);
            
        }
    }
}
