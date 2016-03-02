using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Action {

    public enum ActionType
    {
        IDLE,
        MOVE,
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

}
