using System.Collections;
using System.Collections.Generic;
using Assets.GameClasses;
using UnityEngine;
using SquadronWars.Game.SquadronWarsUnity.Assets.Utilities;
using Object = System.Object;

namespace Assets.Data
{
    public class DbConnection : MonoBehaviour
    {
        public static bool ResponseError = false;
        private static readonly Utilities Utilities = new Utilities();

        public T PopulateObjectFromDb<T>(string url, Player.Logins paramObject) where T : new()
        {
            var parameters = Utilities.CreatePublicPropertyDictionary(paramObject);
            return PopulateObjectFromDb<T>(url, parameters);
        }

        public T PopulateObjectFromDb<T>(string url, Dictionary<string, string> parameters) where T : new()
        { 
            var response = ExecuteApiCall(url, PopulateParameters(parameters));
            return Utilities.MapDataToObject<T>(response.text);
        }

        private WWW ExecuteApiCall(string url, WWWForm form)
        {
            var www = new WWW(url, form);
            StartCoroutine(WaitForRequest(www));
            while (!www.isDone) { }

            return www;
        }

        private WWWForm PopulateParameters(Dictionary<string, string> parameters)
        {
            var form = new WWWForm();

            foreach (var param in parameters)
                form.AddField(param.Key, param.Value);

            return form;
        }

        private IEnumerator WaitForRequest(WWW www)
        {
            yield return www;
        }
    }
}
