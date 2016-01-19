namespace SquadronWars2
{
    public class Stats
    {
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Vitality { get; set; }
        public int Dexterity { get; set; }
        public int Wisdom { get; set; }
        public int Luck { get; set; }
        public int CurrentHp { get; set; }
        public int MaxHp { get; set; }
        public int Damage { get; set; }
        public int Defense { get; set; }
        public int CurrentMp { get; set; }
        public int MaxMp { get; set; }
        public int Speed { get; set; }
        public int MagicDef { get; set; }
        public int MagicDmg { get; set; }
        public int HitRate { get; set; }
        public int CritRate { get; set; }
        public int DodgeRate { get; set; }
        public int StatPoints { get; set; }

        public Stats(int strength, int agility, int intelligence, int vitality, int wisdom, int dexterity, int luck)
        {
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            Vitality = vitality;
            Wisdom = wisdom;
            Dexterity = dexterity;
            Luck = luck;
        }

        public Stats(int strength, int agility, int intelligence, int vitality, int wisdom, int dexterity, int maxHP, 
            int maxMP, int damage, int magicDmg, int speed, int defense, int magicDef, int hitRate, int dodgeRate, int critRate)
        {
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            Vitality = vitality;
            Wisdom = wisdom;
            Dexterity = dexterity;
            MaxHp = maxHP;
            MaxMp = maxMP;
            Damage = damage;
            MagicDmg = magicDmg;
            Speed = speed;
            Defense = defense;
            MagicDef = magicDef;
            HitRate = hitRate;
            DodgeRate = dodgeRate;
            CritRate = critRate;
        }

        public int CalculateHp(int level) {
            return (level * 5) + (Vitality * 10) + (Strength * 2);
        }

        public int CalculateMp(int level)
        {
            return (level * 5) + (Intelligence * 5) + (Wisdom * 10); 
        }

        public int CalculateDamage(int level)
        {
            return (level * 2) + (Strength * 6) + (Dexterity * 2);
        }

        public int CalculateMagicDamage(int level)
        {
            return level + (Intelligence * 8) + Wisdom; 
        }

        public int CalculateSpeed(int level)
        {
            return 5 + (level / 15) + (Agility / 20);
        }

        public int CalculateDefense(int level)
        {
            return (level * 2) + (Vitality * 6) + Strength;
        }

        public int CalculateMagicDefense(int level)
        {
            return (level * 3) + (Wisdom * 5) + Intelligence;
        }

        public int CalculateHitRate(int level)
        {
            return 50 + (level * 2) + (Dexterity * 6) + (Wisdom * 2);
        }

        public int CalculateDodgeRate(int level)
        {
            return 50 + (level * 2) + (Agility * 6);
        }

        public int CalculateCritRate(int level)
        {
            return 35 + (level * 2) + (Luck * 3);
        }

        public Stats RemoveAlteredStats(Stats charStats, Stats itemStats)
        {
            charStats.Strength = charStats.Strength - itemStats.Strength;
            charStats.Agility = charStats.Agility - itemStats.Agility;
            charStats.Intelligence = charStats.Intelligence - itemStats.Intelligence;
            charStats.Vitality = charStats.Vitality - itemStats.Vitality;
            charStats.Dexterity = charStats.Dexterity - itemStats.Dexterity;
            charStats.Wisdom = charStats.Wisdom - itemStats.Wisdom;
            charStats.Luck = charStats.Luck - itemStats.Luck;
            return charStats;
        }
        public Stats ConcatStats(Stats charStats, Stats itemStats)
        {
            var alteredStats = new Stats(0, 0, 0, 0, 0, 0, 0)
            {
                Strength = charStats.Strength + itemStats.Strength,
                Agility = charStats.Agility + itemStats.Agility,
                Intelligence = charStats.Intelligence + itemStats.Intelligence,
                Vitality = charStats.Vitality + itemStats.Vitality,
                Dexterity = charStats.Dexterity + itemStats.Dexterity,
                Wisdom = charStats.Wisdom + itemStats.Wisdom,
                Luck = charStats.Luck + itemStats.Luck
            };
            return alteredStats;
        }
    }
}
