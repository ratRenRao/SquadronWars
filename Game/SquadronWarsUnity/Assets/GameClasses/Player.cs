using System;
using System.Collections.Generic;
using UnityEngine;

//using SquadronWars2.Game.SquadronWarsUnity.Repo;

namespace Assets.GameClasses
{
    [Serializable]
    public class Player : MonoBehaviour
    {
        //DBConnection dbConnection = new DBConnection();
        public string username { get; set; } // remove and create LoginChange class for changes
        public string password { get; set; } // remove and create LoginChange class for changes
        private string firstName { get; set; }
        private string lastName { get; set; }
        private string email { get; set; }
        private DateTime? lastLogin { get; set; }
        private Squad squad { get; set; }
        private List<Item> itemList { get; set; }

        public Player(string username, string password, string firstName, string lastName, string email,
            DateTime? lastLogin, int itemListId, int squadListId)
        {
            this.username = username;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.lastLogin = lastLogin;
            this.itemList = itemList;
        }

        void Update()
        {

        }

        /*public async Task UpdatePlayerFromDb()
        {
            await dbConnection.ExecuteApiCall(GlobalConstants.squadDbUrl);
            Player dbPlayer = dbConnection.DeserializeData<Player>(this);

            this.firstName = dbPlayer.firstName;
            this.lastName = dbPlayer.lastName;
            this.email = dbPlayer.email;
            this.lastLogin = dbPlayer.lastLogin;
            this.squad = dbPlayer.squad;
            this.itemList = dbPlayer.itemList;
        }*/

    }
}
