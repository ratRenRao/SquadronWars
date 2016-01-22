using System;
using UnityEngine;
//  Uncomment to run in Unity
//using UnityEngine.UI;
using System.Collections.Generic;
//using System.Data.Common;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using SquadronWars2.Game.SquadronWarsUnity.Repo;
using DbConnection = SquadronWars2.Game.SquadronWarsUnity.Repo.DbConnection;

namespace SquadronWars2
{
    public class Player
#if !DEBUG
        : MonoBehaviour
#endif
    {
        private readonly DbConnection _dbConnection;
        public string Username { get; private set; } // remove and create LoginChange class for changes
        public string Password { get; private set; } // remove and create LoginChange class for changes
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime? LastLogin { get; private set; }
        public Squad Squad { get; private set; }
        public List<Item> ItemList { get; private set; }

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

        public void PopulatePlayer()
        {
            var parameters = new Dictionary<string, string>();
            
            var dbPlayer= _dbConnection.PopulateObjectFromDb<Player>(GlobalConstants.PlayerDbUrl, this);

            FirstName = dbPlayer.FirstName;
            LastName = dbPlayer.LastName;
            Email = dbPlayer.Email;
            LastLogin = dbPlayer.LastLogin;
            Squad = dbPlayer.Squad;
            ItemList = dbPlayer.ItemList;
        }

    }
}
