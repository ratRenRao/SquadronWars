using System.Collections.Generic;
using System.Reflection;
using Assets.Scripts;
using System.Linq;

namespace Assets.GameClasses
{
    public class Action : IJsonable
    {

        public enum ActionType
        {
            Idle,
            Move,
            Attack,
            AttackAbility,
            CastAbility,
            Endturn
        }

        public ActionType actionType { get; set; }
        public List<Tile> actionTiles { get; set; }
        public string performedAction { get; set; }

        public Action() : this(ActionType.Idle, new List<Tile>(), "default") { }
        public Action(ActionType actionType, List<Tile> actionTiles, string performedAction)
        {
            this.actionType = actionType;
            this.actionTiles = actionTiles;
            this.performedAction = performedAction;
        }

        public string GetJsonObjectName()
        {
            return "Actions";
        }

        public List<PropertyInfo> GetJsonObjectParameters()
        {
            return GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).ToList();
        }

        public void SetJsonObjectParameters(Dictionary<string, object> parameters)
        {
            throw new System.NotImplementedException();
        }

        public string GetJSONString()
        {
            string returnString = "{ \"actionType\" : \"" + actionType + "\", \"performedAction\" : \"" + performedAction + "\", \"actionTiles\" : [ ";
            int index = 0;
            foreach(Tile tile in actionTiles)
            {
                if(index != 0)
                {
                    returnString += ", ";
                }
                returnString += tile.GetJSONString();
                index++;
            }
            returnString += "] }";

            return returnString;
        }
    }
}
