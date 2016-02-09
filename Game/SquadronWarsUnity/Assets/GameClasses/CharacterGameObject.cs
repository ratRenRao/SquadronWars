using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameClasses
{
    class CharacterGameObject : MonoBehaviour
    {
        public bool IsStandard;
        public int x;
        public int y;
        //DBConnection dbConnection = new DBConnection();
        public new string name { get; set; }
        public int characterId { get; set; }
        public int statId { get; set; }
        public Stats baseStats { get; set; }
        public Stats alteredStats { get; set; }
        public int characterListId { get; set; }
        public string characterName { get; set; }
        public Equipment equipment { get; set; }
        public Dictionary<string, int> skillList { get; set; }
        public int level { get; set; }
        public int experience { get; set; }
        public int statPoints { get; set; }
        public int skillPoints { get; set; }
        public List<Effect> effects { get; set; }
        public Sprite sprite { get; set; }
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
