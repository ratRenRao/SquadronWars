using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Assets.Scripts;
using System.Linq;
namespace Assets.GameClasses
{
    public class Action : IJsonable, IEffectable
    {

        public enum ActionType
        {
            Idle,
            Move,
            Attack,
            AttackAbility,
            CastAbility,
            Endturn,
            Reset
        }

        public ActionType actionType { get; set; }
        public List<Tile> actionTiles { get; set; }
        public string performedAction { get; set; }
        public double ImmediateBaseDamage = 0;
        public double LingeringBaseDamage = 0;
        public bool complete = false;
        internal int Duration = 0;
        internal int Damage = 0;
        internal CharacterGameObject Executioner { get; private set; }
        internal Tile ExecutionerTile { get; private set; }
        internal Stopwatch Stopwatch = new Stopwatch();
        internal TimeListener TimeListener;
        internal List<Effect> ResultingEffects;
        internal List<Tile> Tiles = new List<Tile>();
        internal Dictionary<CharacterGameObject, Tile> TileDictionary;
        internal AnimationManager AnimationManager;

        public Action() : this(ActionType.Idle, new List<Tile>(), "default")
        {
        }

        public Action(ActionType actionType, List<Tile> actionTiles, string performedAction)
        {
            this.actionType = actionType;
            this.actionTiles = actionTiles;
            this.performedAction = performedAction;

            //if (performedAction != null && performedAction != "default")
            //    SetEffectFromString();

            //AddPayoutValueForAction();
        }

        /*
        internal void SetEffectFromString()
        {
            //var abilityMatchTypeName = GlobalConstants.AbilityMasterList.SingleOrDefault(effect => effect.Name.ToString().Equals(performedAction));
            //var itemMatch = GlobalConstants.ItemsMasterList.SingleOrDefault(item => item.Name.ToString().Equals(performedAction));

            if (performedAction != null)
            {
                var effectType =
                    GlobalConstants.EffectTypes.SingleOrDefault(type => type.Name.ToString().Equals(performedAction));

                if (effectType != null)
                    Effect = (Effect) Activator.CreateInstance(effectType);
            }
            //else if (itemMatch != null)
            //    Effect = itemMatch; 
        }*/

        public string GetJsonObjectName()
        {
            return "Actions";
        }

        public List<PropertyInfo> GetJsonObjectParameters()
        {
            return
                GetType()
                    .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                    .ToList();
        }

        public void SetJsonObjectParameters(Dictionary<string, object> parameters)
        {
            throw new System.NotImplementedException();
        }

        public string GetJSONString()
        {
            string returnString = "{ \"actionType\" : \"" + actionType + "\", \"performedAction\" : \"" +
                                  performedAction + "\", \"actionTiles\" : [ ";
            int index = 0;
            foreach (Tile tile in actionTiles)
            {
                if (index != 0)
                {
                    returnString += ", ";
                }
                returnString += tile.GetJSONString();
                index++;
            }
            returnString += "] }";

            return returnString;
        }

        public void AddPayoutValueForAction()
        {
            actionTiles.ForEach(tile =>
            {
                if (actionType == ActionType.CastAbility)
                {
                    var total = (int) (tile.amount*.05)/actionTiles.Count;
                    GlobalConstants.DamageAndHealingDone += total != 0
                        ? (int) (tile.amount*.05)/actionTiles.Count
                        : 0;
                }
                else if (actionType == ActionType.Attack || actionType == ActionType.AttackAbility)
                {
                    var total = tile.amount/actionTiles.Count;
                    GlobalConstants.DamageAndHealingDone += total != 0 ? tile.amount/actionTiles.Count : 0;
                }
            });
        }

        public virtual void Initialize(ref Dictionary<CharacterGameObject, Tile> tileDictionary, ref CharacterGameObject executioner,
            ref Tile executionerTile)
        {            
            TileDictionary = tileDictionary;
            Executioner = executioner;
            ExecutionerTile = executionerTile;
        }

        public virtual void Execute()
        {

            foreach (var character in TileDictionary)
            {
                AnimationManager = new AnimationManager(Executioner, character.Key, ExecutionerTile, character.Value, actionType, Damage);
                ImmediateEffect(character.Key.CharacterClassObject.CurrentStats);

                if (Duration > 0)
                {
                    TimeListener = new TimeListener(Duration, character.Key.CharacterClassObject.CurrentStats)
                    {
                        ExecutionMethod = LingeringEffect,
                        FinishingMethod = RemoveEffect
                    };

                    TimeListener.Start();
                    GlobalConstants.TimeListeners[character.Key.CharacterClassObject.CharacterId] = TimeListener; //.Add(character.Key.CharacterClassObject.CharacterId, TimeListener);
                }
                else if (Duration == 0)
                {
                    RemoveEffect();
                }
            }
        }

        public virtual void ImmediateEffect(Stats stats)
        {

        }

        public virtual void RemoveEffect()
        {
            complete = true;
        }

        public virtual void RemoveEffect(ref Stats stats)
        {
            complete = true;
        }

        public virtual void LingeringEffect(ref Stats stats)
        {
        }
    }
}
