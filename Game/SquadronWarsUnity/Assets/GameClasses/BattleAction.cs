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
    }
}
