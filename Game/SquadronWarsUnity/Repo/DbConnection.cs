using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

namespace SquadronWars2.Game.SquadronWarsUnity.Repo
{
    public class DbConnection
    {
        private const string Url = "https://ec2-user@ec2-52-27-154-55.us-west-2.compute.amazonaws.com";
        public static bool ResponseError = false;

        public T PopulateObjectFromDb<T>(string primaryKey, string call)
        {
            var data = ExecuteApiCall(primaryKey, call);
            return DeserializeData<T>(data.Result);
        }

        private async Task<string> ExecuteApiCall(string primaryKey, string call)
        {
            var data = "";
            call = call + "id=" + primaryKey;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync(call);
                    if (response.IsSuccessStatusCode)
                    {
                        data = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        ResponseError = true;
                    }
                }
            }
            catch (Exception e)
            {
                
            }

            return data;
        }

        private T DeserializeData<T>(string data)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(data);
        }
    }
}
