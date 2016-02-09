using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using SquadronWars2.Game.SquadronWarsUnity.Repo;

namespace Assets.GameClasses
{
    public class Squad : IJsonable, IEnumerable<Character>
    {
        //DBConnection dbConnection = new DBConnection();

        public int squadId { get; set; }
        public string squadDetails { get; set; }
        public List<Character> characterList { get; set; }

        public Squad() {}
        /*
        public Squad(int squadId = 0, string squadDetails = null, List<Character> characterList = null)
        {
            this.squadId = squadId;
        }

        public async Task UpdateSquadFromDb()
        {
            await dbConnection.ExecuteApiCall(GlobalConstants.squadDbUrl); 
            Squad dbSquad = dbConnection.DeserializeData<Squad>(this);

            this.characterList = dbSquad.characterList;
            this.squadDetails = dbSquad.squadDetails;
        }*/
        public string GetJsonObjectName()
        {
            return GlobalConstants.SquadJsonObjectName;
        }

        #region IEnumerable<T> Members

        public IEnumerator<Character> GetEnumerator()
        {
            foreach (Character c in characterList)
            {
               if (c == null) 
                   break;

                yield return c;
            }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            // Lets call the generic version here
            return this.GetEnumerator();
        }

        #endregion
    }
}