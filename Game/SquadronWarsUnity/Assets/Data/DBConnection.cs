using System.Collections;
using System.Collections.Generic;
using Assets.GameClasses;
using UnityEngine;
using Object = System.Object;

namespace Assets.Data
{
    public class DbConnection : MonoBehaviour
    {
        public static bool ResponseError = false;
        private static readonly Utilities.Utilities Utilities = new Utilities.Utilities();

        public IJsonable PopulateObjectFromDb<T>(string url, Player.Logins paramObject) where T : IJsonable, new() 
        {
            var parameters = Utilities.CreatePublicPropertyDictionary(paramObject);
            return PopulateObjectFromDb<T>(url, parameters);
        }

        public IJsonable PopulateObjectFromDb<T>(string url, Dictionary<string, string> parameters) where T : IJsonable, new()
        { 
            var response = ExecuteApiCall(url, PopulateParameters(parameters));
            return Utilities.BuildObjectFromJsonData<T>(response.text);
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
