using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

namespace SquadronWars.Game.SquadronWarsUnity.Repo
{
    public class DbConnection
    {
        private static string url = "https://ec2-user@ec2-52-27-154-55.us-west-2.compute.amazonaws.com";
        private static bool responseError = false;
        private static string data = "";        

        public async Task ExecuteApiCall(string apiCall)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiCall);
                if (response.IsSuccessStatusCode)
                {
                     data = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    responseError = true;
                }
            }
        }

        public T DeserializeData<T>(T obj)
        {
            var serializer = new JavaScriptSerializer();
            obj = serializer.Deserialize<T>(data);
            return obj;
        }
    }
}
