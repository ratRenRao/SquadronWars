using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Assets.Data
{
    public class DBConnection : MonoBehaviour
    {
        public static bool ResponseError = false;

        public T PopulateObjectFromDb<T>(string url)
        {
            var parameters = CreatePropertyDictionary<T>(typeof(T));
            var response = ExecuteApiCall(url, PopulateParameters(parameters));
            return DeserializeData<T>(response);
        }

        private Dictionary<string, string> CreatePropertyDictionary<T>(Type type)
        {
            return type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .Where(attribute => !string.IsNullOrEmpty(attribute.ToString()))
                .ToDictionary(attribute => attribute.Name, attribute => attribute.ToString());
        }

        /*public string PushDataToDb(string url, Dictionary<string, string> parameters)
        {
            return ExecuteApiCall(url, parameters);
        }*/

        private IEnumerator WaitForRequest(WWW www)
        {
            yield return www;

            if (www.error == null)
            {
                Debug.Log("Success : " + www.text);
            }
            else {
                Debug.Log("Error: " + www.error);
            }
        }

        private string ExecuteApiCall(string url, WWWForm form)
        {
            var www = new WWW(url, form);
            var response = StartCoroutine(WaitForRequest(www));

            Debug.Log(response.ToString());
            return response.ToString();
        }

        private WWWForm PopulateParameters(Dictionary<string, string> parameters)
        {
            var form = new WWWForm();

            foreach (var param in parameters)
                form.AddField(param.Key, param.Value);

            return form;
        }

        private T DeserializeData<T>(string data)
        {
            var obj = JsonUtility.FromJson<T>(data);
            return obj;
        }

        /*
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
            request.url = GlobalConstants.PlayerDbUrl;
            
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
        }*/
    }
}
