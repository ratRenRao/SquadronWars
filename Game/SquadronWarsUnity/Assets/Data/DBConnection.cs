using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using UnityEngine;

namespace Assets.Data
{
    public class DBConnection : MonoBehaviour
    {
        public static bool ResponseError = false;
        private WWW www = null;
        //private string response = null; 

        public T PopulateObjectFromDb<T>(string url, T obj)
        {
            var parameters = CreateGetPropertyDictionary(obj);
            var response = ExecuteApiCall(url, PopulateParameters(parameters));
            return DeserializeData<T>(response);
        }

        /// <summary>
        /// Creates a dictionary of string objects containing only public parameters of the 
        /// object passed in, and their corresponding values.
        /// </summary>
        /// <param name="obj">Object to create param dictionary from</param>
        /// <returns>Dictionary&ltstring, string&gt</returns>
        private Dictionary<string, string> CreateGetPropertyDictionary<T>(T obj)
        {
            return obj.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .Where(attribute => !string.IsNullOrEmpty(attribute.ToString()))
                .ToDictionary(attribute => attribute.Name, attribute => obj.GetType().GetProperty(attribute.Name).GetValue(obj, null).ToString());
        }

        /// <summary>
        /// Creates a dictionary of string objects containing all parameters of the
        /// object passed in, and their corresponding values. 
        /// </summary>
        /// <param name="obj">Object to create the param dictionary from</param>
        /// <returns>Dictionary&ltstring, string$gt</returns>
        private Dictionary<string, string> CreatePostPropertyDictionary<T>(T obj)
        {
            return obj.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(attribute => !string.IsNullOrEmpty(attribute.ToString()))
                .ToDictionary(attribute => attribute.Name, attribute => obj.GetType().GetProperty(attribute.Name).ToString());
        }

        /*public string PushDataToDb(string url, Dictionary<string, string> parameters)
        {
            return ExecuteApiCall(url, parameters);
        }*/

        private string ExecuteApiCall(string url, WWWForm form)
        {
            www = new WWW(url, form);
            StartCoroutine(WaitForRequest()); 
            return www.text;
        }

        /*
        private IEnumerator CoroutineWaitFunction(WWW request)
        {
            yield return StartCoroutine(WaitForRequest(request));
        }
        */

        private WWWForm PopulateParameters(Dictionary<string, string> parameters)
        {
            var form = new WWWForm();

            foreach (var param in parameters)
                form.AddField(param.Key, param.Value);

            return form;
        }

        private IEnumerator WaitForRequest()
        {
            yield return www;

            if (www.error == null)
            {
                Debug.Log("API Success: " + www.text);
                yield return www.text;
            }
            else {
                Debug.Log("API Error: " + www.error);
                yield return "ERROR";
            }
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
