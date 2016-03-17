using System.Collections;
using System.Collections.Generic;
using Assets.GameClasses;
using UnityEditor;
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
            var response = SendPostData(url, paramObject);

            return Utilities.BuildObjectFromJsonData<T>(response.text);
        }

        public WWW SendPostData<T>(string url, T obj)
        {
            var jsonParamObject = WrapJsonInGameObject(ConvertToJson(obj));
            return ExecuteApiCall(url, jsonParamObject);
        }

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

        public JSONObject ConvertToJson<T>(T obj, string propertyScope = "public")
        {
            //var jsonDictionary = Utilities.CreatePublicPropertyDictionary(obj);
            return Utilities.CreateNestedJsonObject(obj, typeof(T), propertyScope);
            //return JSONObject.Create(jsonDictionary);
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
