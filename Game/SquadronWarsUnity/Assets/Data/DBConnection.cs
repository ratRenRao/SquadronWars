using System.Collections;
using System.Collections.Generic;
using Assets.GameClasses;
using UnityEngine;

namespace Assets.Data
{
    public class DbConnection : MonoBehaviour
    {
        public static bool ResponseError = false;
        private static readonly Utilities.Utilities Utilities = new Utilities.Utilities();

        public T PopulateObjectFromDb<T>(string url, object paramObject) where T : IJsonable
        {
            var parameters = Utilities.CreatePublicPropertyDictionary(paramObject);
            return PopulateObjectFromDb<T>(url, parameters);
        }

        public T PopulateObjectFromDb<T>(string url, Dictionary<string, string> parameters) where T : IJsonable
        { 
            var response = ExecuteApiCall(url, PopulateParameters(parameters));
            return Utilities.BuildObjectFromJsonData<T>(response.text);
        }

        public WWW ExecuteApiCall(string url, WWWForm form)
        {
            var www = new WWW(url, form);
            StartCoroutine(WaitForRequest(www));
            while (!www.isDone) { }

            return www;
        }

        public WWWForm PopulateParameters(Dictionary<string, string> parameters)
        {
            var form = new WWWForm();

            foreach (var param in parameters)
                form.AddField(param.Key, param.Value);

            return form;
        }

        public JSONObject ConvertToJson<T>(T obj)
        {
            var jsonDictionary = Utilities.CreatePublicPropertyDictionary(obj);
            var jsonObject = JSONObject.Create(jsonDictionary);
            return WrapJsonInGameObject(jsonObject);
        }

        private JSONObject WrapJsonInGameObject(JSONObject obj)
        {
            var wrappedJson = new JSONObject(); 
            wrappedJson.AddField("GameObject", obj);
            return wrappedJson;
        } 

        private IEnumerator WaitForRequest(WWW www)
        {
            yield return www;
        }
    }
}
