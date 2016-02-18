using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameClasses
{
    class CharacterGameObject : MonoBehaviour
    {
        public int X;
        public int Y;

        //DBConnection dbConnection = new DBConnection();
        public int CharacterId { get; set; }
        public int StatPoints { get; set; }
        public int SkillPoints { get; set; }
        public int LevelId { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public int Helm { get; set; }
        public int Chest { get; set; }
        public int Gloves { get; set; }
        public int Pants { get; set; }
        public int Shoulders { get; set; }
        public int Boots { get; set; }
        public int Accessory1 { get; set; }
        public int Accessory2 { get; set; }
        public int IsStandard { get; set; }
        public int Str { get; set; }
        public int Intl { get; set; }
        public int Agi { get; set; }
        public int Wis { get; set; }
        public int Vit { get; set; }
        public int Dex { get; set; }
        public int HitPoints { get; set; }
        public int Dmg { get; set; }
        public int AbilityPoints { get; set; }
        public int Speed { get; set; }
        public int Defense { get; set; }
        public int MagicDefense { get; set; }
        public int MagicAttack { get; set; }
        public int HitRate { get; set; }
        public int CritRate { get; set; }
        public int DodgeRate { get; set; }
        public int Luck { get; set; }
        public Sprite Sprite { get; set; }

        public int CurrentHp { get; set; }

        public readonly Character characterClassObject;

        void Update()
        {
            if (characterClassObject == null)
                return;

            if (characterClassObject.Updated)
                UpdateCharacterGameObject();
        }

        private void UpdateCharacterGameObject()
        {
            
        }

    }
}
