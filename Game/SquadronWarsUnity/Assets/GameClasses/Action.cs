using System.Collections.Generic;
using System.Reflection;
using Assets.Scripts;
using Assets.Scripts;
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
            Occupy
        }

        ActionType actionType { get; set; }
        List<Tile> actionTiles = new List<Tile>();
        string performedAction { get; set; }

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
            throw new System.NotImplementedException();
        }

        public void SetJsonObjectParameters(Dictionary<string, object> parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}
