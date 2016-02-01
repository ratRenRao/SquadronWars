using System.Collections.Generic;

//using SquadronWars2.Game.SquadronWarsUnity.Repo;

namespace Assets.GameClasses
{
    public class Squad : IJsonable
    {
        //DBConnection dbConnection = new DBConnection();

        public int squadId { get; set; }
        public string squadDetails { get; set; }
        public List<Character> characterList { get; set; }
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
        public string GetJsonObjectName()
        {
            throw new System.NotImplementedException();
        }
    }
}