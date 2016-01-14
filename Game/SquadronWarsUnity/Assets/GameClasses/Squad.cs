using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquadronWars2.Game.SquadronWarsUnity.Repo;
using DbConnection = SquadronWars2.Game.SquadronWarsUnity.Repo.DbConnection;

namespace SquadronWars2
{
    public class Squad
    {
        public DbConnection Connection { get; set; }

        private int SquadId { get; set; }
        private string SquadDetails { get; set; }
        private List<Character> CharacterList { get; set; }
        public Squad(int squadId, int playerId, DbConnection dbConnection, string squadDetails, List<Character> characterList)
        {
            SquadId = squadId;
            Connection = dbConnection;
            SquadDetails = squadDetails;
            CharacterList = characterList;
        }

        public void UpdateSquadFromDb()
        {
            var dbSquad = Connection.PopulateObjectFromDb<Squad>(SquadId.ToString(), GlobalConstants.SquadDbUrl);

            CharacterList = dbSquad.CharacterList;
            SquadDetails = dbSquad.SquadDetails;
        }
    }
}