using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Assets.GameClasses;
using Assets.Scripts;
using Action = Assets.GameClasses.Action;

namespace Assets.Data
{
    class GameInfo : IJsonable
    { 
        public int gameID { get; set; }
        public int player1Id { get; set; }
        public int player2Id { get; set; }
        public GameJSON GameJson { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public string Finished { get; set; }
        public List<Character> character1Info { get; set; }
        public List<Character> character2Info { get; set; }
        public string GetJsonObjectName()
        {
            return "GameInfo";
        }

        public List<PropertyInfo> GetJsonObjectParameters()
        {
            throw new NotImplementedException();
        }

        public void SetJsonObjectParameters(Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }
    }

    class GameJSON : IJsonable
    {
        public Action ActionOrder { get; set; }
        public List<int> CharacterQueue { get; set; }
        public List<Tile> AffectedTiles { get; set; }
        public string GetJsonObjectName()
        {
            return "GameJSON";
        }

        public List<PropertyInfo> GetJsonObjectParameters()
        {
            throw new NotImplementedException();
        }

        public void SetJsonObjectParameters(Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }
    }

    class ActionOrder : IJsonable
    {
        Action.ActionType actionType { get; set; }

        public string GetJsonObjectName()
        {
            return "ActionOrder";
        }

        public List<PropertyInfo> GetJsonObjectParameters()
        {
            throw new NotImplementedException();
        }

        public void SetJsonObjectParameters(Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }
    }

   /* class CharacterQueue : IJsonable
    {
        //What are the numbers at the beginning of this section in the JSON?
        public List<int> CharacterQueue { get; set; } 
        public string GetJsonObjectName()
        {
            return "CharacterQueue";
        }

        public List<PropertyInfo> GetJsonObjectParameters()
        {
            throw new NotImplementedException();
        }

        public void SetJsonObjectParameters(Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }
    }
    */
}
