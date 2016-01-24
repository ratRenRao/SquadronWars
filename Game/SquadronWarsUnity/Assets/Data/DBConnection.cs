using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Experimental.Networking;
using SquadronWars2;

namespace Assets.Data
{
    class DBConnection
    {
        //Asynch web request in UnityEngines framework.
        private UnityWebRequest request;
        private DownloadHandler download;
        private UploadHandler upload;

        //May change constructor
        public DBConnection()
        {
            request = new UnityWebRequest();
            request.uploadHandler = upload;
            request.downloadHandler = download;
            request.method = UnityWebRequest.kHttpVerbPOST;
        }

        //public calls to send and receive with web API
        public void LoginPlayer(string username, string password)
        {
            request.url = GlobalConstants.playerDbUrl;
            
            //TODO: create log in calls to API will need to change the return type to what we want to return to the client
        }

        public void GetPlayer(string username, string password)
        {
            //TODO: Return player object. Needed or done in LoginPlayer?
        }

        public void GetCharacter(string username, string password, Character character)
        {
            //TODO: Code to get character from server.
        }

        public void GetSquad(string username, string password, Squad squad)
        {
            //TODO: Code to retrieve squad from server.
        }

        public void UpdatePlayer(string username, string password, Player player)
        {
            //TODO: What do we want to accomplish with Update Player?
        }

        public void UpdateCharacter(string username, string password, Character character)
        {
            //TODO: Will we need this routine? 
        }

        public void UpdateSquad(string username, string password, Squad squad)
        {
            //TODO: Code for updating a squad.
        }



    }
}
