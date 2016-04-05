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

        public int StartExperience()
        {
            if (LevelId == 1)
            {
                return 0;
            }
            return 200 + ((int)Math.Pow(LevelId - 1, 2) * 50);
        }

        public int ExperienceNeeded()
        {
            if (LevelId == 1)
            {
                return 200;
            }
            return 200 + ((int)Math.Pow(LevelId, 2) * 50);
        }

        public int PercentToNextLevel()
        {
            var startExp = StartExperience();
            var finishExp = ExperienceNeeded();
            var percentComplete = (double)(BaseStats.Experience - startExp) / (finishExp - startExp) * 100;
            return Convert.ToInt32(percentComplete);
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