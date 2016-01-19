using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace SquadronWars2.Game.SquadronWarsUnity.Repo
{
    public class DbConnection
    {
        public static bool ResponseError = false;

        public T PopulateObjectFromDb<T>(string path, string key, string id)
        {
            var url = $"{GlobalConstants.ServerUrl}{path}";

            var data = ExecuteApiCall(url, key, id);
            return DeserializeData<T>(data.Result);
        }

        private async Task<string> ExecuteApiCall(string url, string key, string id)
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string>
                {
                    {key, id}
                };

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
