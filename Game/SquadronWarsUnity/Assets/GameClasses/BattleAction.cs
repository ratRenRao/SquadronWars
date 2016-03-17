﻿using System.Collections.Generic;
using System.Reflection;
using Assets.Scripts;

namespace Assets.GameClasses
{
    public class BattleAction : IJsonable
    {
        public List<Action> ActionOrder = new List<Action>();
        public List<int> CharacterQueue = new List<int>();
        public Dictionary<Tile, int> AffectedTiles = new Dictionary<Tile, int>();

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

        public void ResetBattleActions()
        {
            ActionOrder.Clear();
            CharacterQueue.Clear();
            AffectedTiles.Clear();
        }

        public void AddAffectedTile(Tile tile, int amount)
        {
            AffectedTiles.Add(tile, amount);
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
            string returnString = "Gameinfo\", \"GameJSON\" : { \"ActionOrder\" : { ";
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
            returnString += "}, \"AffectedTiles\" : { ";
            index = 0;
            foreach(KeyValuePair<Tile,int> key in AffectedTiles)
            {
                if(index != 0)
                {
                    returnString += ", ";
                }
                returnString += "\"" + index + "\" : { \"Tile\" : " + key.Key.GetJSONString() + ", \"Amount\" : " + key.Value + "}";
                index++;
            }
            returnString += "} }, \"end\" : \"end";

            return returnString;
        }
    }
}
