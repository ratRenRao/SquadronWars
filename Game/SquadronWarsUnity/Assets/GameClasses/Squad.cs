using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SquadronWars2
{
    class Squad
    {
        private int squadId { get; set; }
        private string squadDetails { get; set; }
        private List<Character> characterList { get; set; }
        public Squad(int squadId, int playerId)
        {
            this.squadId = squadId;
        }
    }
}