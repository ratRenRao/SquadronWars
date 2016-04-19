using System.Collections.Generic;
using Assets.Scripts;

namespace Assets.GameClasses
{

    class Burn : Effect
    {
        public override void Initialize(ref List<Tile> tiles, ref CharacterGameObject executioner, ref Tile executionerTile)
        {
            base.Initialize(ref tiles, ref executioner, ref executionerTile);
            Duration = 6;
        }

        public override void LingeringEffect(Stats stats)
        {
//            Target.CurHp -= _burnDamage;
        }
    }
}