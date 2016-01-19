using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Script.Serialization;

namespace SquadronWars2.Game.SquadronWarsUnity.Repo
{
    public class DbConnection
    {
        public static bool ResponseError = false;

        public T PopulateObjectFromDb<T>(string path, T obj)
        {
            var url = $"{GlobalConstants.ServerUrl}{path}";

            var parameters = CreatePropertyDictionary<T>(obj);
            var response = ExecuteApiCall(url, parameters);
            return DeserializeData<T>(response.Result);
        }

        private Dictionary<string, string> CreatePropertyDictionary<T>(T obj)
        {
            return obj.GetType().GetProperties().Where(attribute => !string.IsNullOrEmpty(attribute.ToString()))
                .ToDictionary(attribute => attribute.Name, attribute => attribute.GetConstantValue().ToString());
        }

        public string PushDataToDb(string path, Dictionary<string, string> parameters)
        {
            var url = $"{GlobalConstants.ServerUrl}{path}";

            return ExecuteApiCall(url, parameters).Result;
        }

        private async Task<string> ExecuteApiCall(string url, Dictionary<string, string> parameters)
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(parameters);
                var response = await client.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseString))
                    throw new Exception("No data returned");

                return responseString;
            }

            //return responseString;
        }

        private T DeserializeData<T>(string data)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(data);
        }
    }
}
