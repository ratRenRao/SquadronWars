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
    public class GameInfo : IJsonable
    {        
        public int GameId { get; set; }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public BattleAction BattleAction { get; set; }
        public GameJSON GameJson { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public string Finished { get; set; }
        public int MapId { get; set; }
        public List<Character> Character1Info { get; set; }
        public List<Character> Character2Info { get; set; }


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

    public class GameJSON : IJsonable
    {
        public List<Action> ActionOrder { get; set; }
        public List<int> CharacterQueue { get; set; }
        public Dictionary<Tile, int> AffectedTiles { get; set; }
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

    internal class ActionOrder : IJsonable
    {
        ActionType ActionType { get; set; }

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
