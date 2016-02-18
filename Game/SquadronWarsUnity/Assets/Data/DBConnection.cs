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
        private static readonly Utilities Utilities = new Utilities();

        public T PopulateObjectFromDb<T>(string url, object paramObject) where T : IJsonable
        {
            var jsonParamObject = WrapJsonInGameObject(ConvertToJson(paramObject));
            var response = ExecuteApiCall(url, jsonParamObject);

            return Utilities.BuildObjectFromJsonData<T>(response.text);
            
            //var parameters = Utilities.CreatePublicPropertyDictionary(paramObject);
            //return PopulateObjectFromDb<T>(url, parameters);
        }

        /*
        public T PopulateObjectFromDb<T>(string url, Dictionary<string, string> parameters) where T : IJsonable
        { 
            var response = ExecuteApiCall(url, PopulateParameters(parameters));
            return Utilities.BuildObjectFromJsonData<T>(response.text);
        }
        */

        public WWW ExecuteApiCall(string url, WWWForm form)
        {
            var www = new WWW(url, form);
            StartCoroutine(WaitForRequest(www));
            while (!www.isDone) { }

            return www;
        }

        public WWW ExecuteApiCall(string url, JSONObject jsonObject)
        {
            //jsonObject = JSONObject.CreateStringObject("{\"GameObject\": {\"userName\": \"test\",\"password\": \"testing123\"}");
            var www = new WWW(url, jsonObject);
            StartCoroutine(WaitForRequest(www));
            while (!www.isDone) { }

            return www;
        }

        public WWWForm CreatePostForm(JSONObject jsonParamObject)
        {
            var form = new WWWForm();

            form.AddField("GameObject", jsonParamObject.ToString());

            return form;
        }

        public JSONObject ConvertToJson<T>(T obj)
        {
            var jsonDictionary = Utilities.CreatePublicPropertyDictionary(obj);
            return JSONObject.Create(jsonDictionary);
        }

        public JSONObject WrapJsonInGameObject(JSONObject jsonObject)
        {
            var tempJsonObject = new JSONObject();
            tempJsonObject.AddField("GameObject", jsonObject);

            return tempJsonObject;
        }

        private IEnumerator WaitForRequest(WWW www)
        {
            yield return www;
        }
    }
}
