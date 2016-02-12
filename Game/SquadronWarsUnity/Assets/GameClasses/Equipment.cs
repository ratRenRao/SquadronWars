using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameClasses
{
    public class Equipment
    {
        //public Stats stats { get; set; }
        public int helm { get; set; }
        public int chest { get; set; }
        public int gloves{ get; set; }
        public int pants{ get; set; }
        public int shoulders{ get; set; }
        public int boots{ get; set; }
        public int accessory1{ get; set; }
        public int accessory2{ get; set; }

        private Item helmObject { get; set; }
        private Item chestObject { get; set; }
        private Item glovesObject { get; set; }
        private Item pantsObject { get; set; }
        private Item shouldersObject { get; set; }
        private Item bootsObject { get; set; }
        private Item accessory1Object { get; set; }
        private Item accessory2Object { get; set; }

        public Equipment()
        {
            
        }

        public List<Item> GetEquipmentItems()
        {

            return new List<Item>()
            {
                helmObject,
                chestObject,
                glovesObject,
                pantsObject,
                shouldersObject,
                bootsObject,
                accessory1Object,
                accessory2Object
            };
        }
    }
}
