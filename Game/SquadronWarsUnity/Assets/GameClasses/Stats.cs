using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquadronWars
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
        public int level { get; set; }
        public int experience { get; set; }
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
            int experience, int level, int maxMP, int damage, int magicDmg, int speed, int defense, int magicDef, int hitRate, int dodgeRate, int critRate)
        {
            this.strength = strength;
            this.agility = agility;
            this.intelligence = intelligence;
            this.vitality = vitality;
            this.wisdom = wisdom;
            this.dexterity = dexterity;
            this.experience = experience;
            this.level = level;
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

        public int calculateHP() {
            return (level * 15) + (vitality * 10) + (strength * 2);
        }

        public int calculateMP()
        {
            return (level * 5) + (intelligence * 5) + (wisdom * 10); 
        }

        public int calculateDamage()
        {
            return (level * 2) + (strength * 6) + (dexterity * 2);
        }

        public int calculateMagicDamage()
        {
            return level + (intelligence * 8) + wisdom; 
        }

        public int calculateSpeed()
        {
            return 5 + (level / 15) + (agility / 20);
        }

        public int calculateDefense()
        {
            return (level * 2) + (vitality * 6) + strength;
        }

        public int calculateMagicDefense()
        {
            return (level * 3) + (wisdom * 5) + intelligence;
        }

        public int calculateHitRate()
        {
            return 50 + (level * 2) + (dexterity * 6) + (wisdom * 2);
        }

        public int calculateDodgeRate()
        {
            return 50 + (level * 2) + (agility * 6);
        }

        public int calculateCritRate()
        {
            return 35 + (level * 2) + (luck * 3);
        }

    }
}
