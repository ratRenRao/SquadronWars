using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts;
using UnityEditor;

namespace Assets.GameClasses
{
    public interface IEffectable
    {
        void Initialize(ref Dictionary<Character, Tile> tileDictionary, ref Character executioner, ref Tile executionerTile);
        void Execute();
        void ImmediateEffect(Stats stats);
        void RemoveEffect(ref Stats stats);
        void LingeringEffect(ref Stats stats);
    }
}
