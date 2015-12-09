using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SquadronWars2.Game.SquadronWarsUnity.Repo;
using UnityEditor.VersionControl;

namespace SquadronWars2
{
    class Squad
    {
        DbConnection dbConnection = new DbConnection();

        private int squadId { get; set; }
        private string squadDetails { get; set; }
        private List<Character> characterList { get; set; }
        public Squad(int squadId, int playerId)
        {
            this.squadId = squadId;
        }

        /*public async Task UpdateSquadFromDb()
        {
            await dbConnection.ExecuteApiCall(GlobalConstants.squadDbUrl); 
            Squad dbSquad = dbConnection.DeserializeData<Squad>(this);

            this.characterList = dbSquad.characterList;
            this.squadDetails = dbSquad.squadDetails;
        }*/
    }
}