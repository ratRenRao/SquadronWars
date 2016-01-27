namespace Assets.GameClasses
{
    public class Stats
    {
        public int strength { get; set; }
        public int agility { get; set; }
        public int intelligence { get; set; }
        public int vitality { get; set; }
        public int dexterity { get; set; }
        public int wisdom { get; set; }
        public int luck { get; set; }
        public int currentHP { get; set; }
        public int maxHP { get; set; }
        public int damage { get; set; }
        public int defense { get; set; }
        public int currentMP { get; set; }
        public int maxMP { get; set; }
        public int speed { get; set; }
        public int magicDef { get; set; }
        public int magicDmg { get; set; }
        public int hitRate { get; set; }
        public int critRate { get; set; }
        public int dodgeRate { get; set; }
        public int statPoints { get; set; }

        public Stats(int strength, int agility, int intelligence, int vitality, int wisdom, int dexterity, int luck)
        {
            this.strength = strength;
            this.agility = agility;
            this.intelligence = intelligence;
            this.vitality = vitality;
            this.wisdom = wisdom;
            this.dexterity = dexterity;
            this.luck = luck;
        }

        public Stats(int strength, int agility, int intelligence, int vitality, int wisdom, int dexterity, int maxHP, 
            int maxMP, int damage, int magicDmg, int speed, int defense, int magicDef, int hitRate, int dodgeRate, int critRate)
        {
            this.strength = strength;
            this.agility = agility;
            this.intelligence = intelligence;
            this.vitality = vitality;
            this.wisdom = wisdom;
            this.dexterity = dexterity;
            this.maxHP = maxHP;
            this.maxMP = maxMP;
            this.damage = damage;
            this.magicDmg = magicDmg;
            this.speed = speed;
            this.defense = defense;
            this.magicDef = magicDef;
            this.hitRate = hitRate;
            this.dodgeRate = dodgeRate;
            this.critRate = critRate;
        }

        public int calculateHP(int level) {
            return (level * 5) + (vitality * 10) + (strength * 2);
        }

        public int calculateMP(int level)
        {
            return (level * 5) + (intelligence * 5) + (wisdom * 10); 
        }

        public int calculateDamage(int level)
        {
            return (level * 2) + (strength * 6) + (dexterity * 2);
        }

        public int calculateMagicDamage(int level)
        {
            return level + (intelligence * 8) + wisdom; 
        }

        public int calculateSpeed(int level)
        {
            return 5 + (level / 15) + (agility / 20);
        }

        public int calculateDefense(int level)
        {
            return (level * 2) + (vitality * 6) + strength;
        }

        public int calculateMagicDefense(int level)
        {
            return (level * 3) + (wisdom * 5) + intelligence;
        }

        public int calculateHitRate(int level)
        {
            return 50 + (level * 2) + (dexterity * 6) + (wisdom * 2);
        }

        public int calculateDodgeRate(int level)
        {
            return 50 + (level * 2) + (agility * 6);
        }

        public int calculateCritRate(int level)
        {
            return 35 + (level * 2) + (luck * 3);
        }

        public Stats removeAlteredStats(Stats charStats, Stats itemStats)
        {
            charStats.strength = charStats.strength - itemStats.strength;
            charStats.agility = charStats.agility - itemStats.agility;
            charStats.intelligence = charStats.intelligence - itemStats.intelligence;
            charStats.vitality = charStats.vitality - itemStats.vitality;
            charStats.dexterity = charStats.dexterity - itemStats.dexterity;
            charStats.wisdom = charStats.wisdom - itemStats.wisdom;
            charStats.luck = charStats.luck - itemStats.luck;
            return charStats;
        }
        public Stats concatStats(Stats charStats, Stats itemStats)
        {
            Stats alteredStats = new Stats(0,0,0,0,0,0,0);
            alteredStats.strength = charStats.strength + itemStats.strength;
            alteredStats.agility = charStats.agility + itemStats.agility;
            alteredStats.intelligence = charStats.intelligence + itemStats.intelligence;
            alteredStats.vitality = charStats.vitality + itemStats.vitality;
            alteredStats.dexterity = charStats.dexterity + itemStats.dexterity;
            alteredStats.wisdom = charStats.wisdom + itemStats.wisdom;
            alteredStats.luck = charStats.luck + itemStats.luck;
            return alteredStats;
        }
    }
}
