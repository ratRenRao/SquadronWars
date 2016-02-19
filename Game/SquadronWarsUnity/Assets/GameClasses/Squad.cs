using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//using SquadronWars2.Game.SquadronWarsUnity.Repo;

namespace Assets.GameClasses
{
    public class Squad : IJsonable, IEnumerable<Character>
    {
        //DBConnection dbConnection = new DBConnection();

        public int SquadId { get; set; }
        public string SquadDetails { get; set; }
        public List<Character> CharacterList { get; set; }

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
            //return GlobalConstants.SquadJsonObjectName;
            return "Squad";
        }

        public List<PropertyInfo> GetJsonObjectParameters()
        {
            throw new System.NotImplementedException();
        }

        public void SetJsonObjectParameters(Dictionary<string, object> parameters)
        {
            throw new System.NotImplementedException();
        }

        #region IEnumerable<T> Members

        public IEnumerator<Character> GetEnumerator()
        {
            foreach (Character c in CharacterList)
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