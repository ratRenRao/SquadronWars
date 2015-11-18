using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Squad
{
    private int squadId { get; set; }
    private int playerId { get; set; } // is this needed?
    private string squadDetails { get; set; }

    public Squad(int squadId, int playerId)
    {
        this.squadId = squadId;
        this.playerId = playerId;
    }
}
