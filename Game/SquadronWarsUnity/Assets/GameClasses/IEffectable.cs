using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts;

namespace Assets.GameClasses
{
    public interface IEffectable
    {
        void Initialize(ref List<Tile> tiles, ref CharacterGameObject executioner, ref Tile executionerTile);
        void Execute();
        void ImmediateEffect(Stats stats);
        void RemoveEffect(Stats stats);
        void LingeringEffect(Stats stats);
        bool IsComplete();
    }
}
