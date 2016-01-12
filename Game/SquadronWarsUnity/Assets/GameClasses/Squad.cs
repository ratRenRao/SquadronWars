using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using SquadronWars2.Game.SquadronWarsUnity.Repo;

namespace SquadronWars2
{
    public class Squad
    {
        private DbConnection _dbConnection = new DbConnection();

        private int SquadId { get; set; }
        private string SquadDetails { get; set; }
        private List<Character> CharacterList { get; set; }
        public Squad(int squadId, int playerId, DbConnection dbConnection, string squadDetails, List<Character> characterList)
        {
            SquadId = squadId;
            _dbConnection = dbConnection;
            SquadDetails = squadDetails;
            CharacterList = characterList;
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