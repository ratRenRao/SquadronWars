using System.Collections.Generic;
using System.Reflection;

namespace Assets.GameClasses
{
    public class BattleAction : IJsonable
    {
        public List<Action> ActionOrder = new List<Action>();
        public List<int> CharacterQueue = new List<int>();

        //BuildQueue for characters turns
        public string GetJsonObjectName()
        {
            return "BattleActions";
        }

        public List<PropertyInfo> GetJsonObjectParameters()
        {
            throw new System.NotImplementedException();
        }

        public void SetJsonObjectParameters(Dictionary<string, object> parameters)
        {
            throw new System.NotImplementedException();
        }

        public void AddAction(Action action)
        {
            if(!ActionOrder.Contains(action))
            {
                ActionOrder.Add(action);
            }
        }

        public string GetJSONString()
        {
            string returnString = "GameJSON\", \"ActionOrder\" : { ";
            int index = 0;
            foreach(Action action in ActionOrder)
            {
                if(index != 0)
                {
                    returnString += ", ";
                }
                returnString += "\"" + index + "\" : " + action.GetJSONString();
                index++;
            }
            returnString += " }, \"CharacterQueue\" : { ";
            index = 0;
            foreach(int position in CharacterQueue)
            {
                if(index != 0)
                {
                    returnString += ", ";
                }
                returnString += "\"" + index + "\" : " + position;
                index++;
            }
            returnString += "} , \"end\" : \"end";

            return returnString;
        }
    }
}
