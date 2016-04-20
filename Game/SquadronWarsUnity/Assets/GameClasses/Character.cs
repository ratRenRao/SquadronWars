using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Assets.GameClasses
{
    public class Character : IJsonable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Updated = false;

        public int CharacterId { get; set; }
        public int LevelId { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public int SpriteId { get; set; }
        public Stats BaseStats { get; set; }
        public Stats CurrentStats { get; set; }
        public Equipment Equipment { get; set; }
        public List<Ability> Abilities { get; set; }

        public void GetBonusStats()
        {
            foreach (var item in Equipment.GetItemList())
            {
                CurrentStats = CurrentStats.ConcatStats(BaseStats, item.Stats);
            }
        }

        public void CheckEffects()
        {
            /*
            foreach (Effect effect in effects)
            {
            }
            */
        }

        public int CurrentExperience()
        {
            return Experience;
        }

        public int ExperienceNeeded()
        {
            int[] expLevel = new int[] { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
            return expLevel[LevelId - 1];
        }

        public int PercentToNextLevel()
        {
            var startExp = CurrentExperience();
            var finishExp = ExperienceNeeded();
            double percentComplete = ((double)startExp / (double)finishExp);
            return Convert.ToInt32(percentComplete * 100);
        }

        public string GetJsonObjectName()
        {
            throw new NotImplementedException();
        }

        public List<PropertyInfo> GetJsonObjectParameters()
        {
            return GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).ToList();
        }

        public void SetJsonObjectParameters(Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public string GetJSONString()
        {
            string returnString = "{ \"CharacterId\" : \"" + CharacterId + "\", \"LevelId\" : \"" + LevelId + "\", \"Name\" : \"" + Name + "\", \"SpriteId\" : \""
                + SpriteId + "\", \"X\" : \"" + X + "\", \"Y\" : \"" + Y + "\", \"BaseStats\" : " + BaseStats.GetJSONString() + ", \"CurrentStats\" : " + CurrentStats.GetJSONString()
                + ", \"Equipment\" : " + Equipment.GetJSONString() + ", \"Abilities\" : [ ";
            int index = 0;
            foreach(Ability ability in Abilities)
            {
                if(index != 0)
                {
                    returnString += ", ";
                }
                returnString += ability.GetJSONString();
                index++;
            }
            returnString += " ] }";

            return returnString;
        }
    }
}