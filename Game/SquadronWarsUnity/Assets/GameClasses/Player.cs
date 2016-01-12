using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using SquadronWars2.Game.SquadronWarsUnity.Repo;

namespace SquadronWars2
{
    public class Player : MonoBehaviour
    {
        private DbConnection _dbConnection = new DbConnection();
        private string Username { get; set; } // remove and create LoginChange class for changes
        private string Password { get; set; } // remove and create LoginChange class for changes
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string Email { get; set; }
        private DateTime? LastLogin { get; set; }
        private Squad Squad { get; set; }
        private List<Item> ItemList { get; set; }

        public Player(string username, string password, string firstName, string lastName, string email,
            DateTime? lastLogin, List<Item> itemList, Squad squad, DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            LastLogin = lastLogin;
            Squad = squad;
            ItemList = itemList;
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
