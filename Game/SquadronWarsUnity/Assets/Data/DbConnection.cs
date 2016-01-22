using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Policy;
//using System.Web.Script.Serialization;

namespace SquadronWars2.Game.SquadronWarsUnity.Repo
{
    public class DbConnection
    {
        public static bool ResponseError = false;

        public T PopulateObjectFromDb<T>(string path, T obj)
        {
            var url = "";//$"{GlobalConstants.ServerUrl}{path}";

            var parameters = CreatePropertyDictionary<T>(obj);
            var response = ExecuteApiCall(url, parameters);
            return DeserializeData<T>(response);
        }

        private Dictionary<string, string> CreatePropertyDictionary<T>(T obj)
        {
            return obj.GetType().GetProperties().Where(attribute => !string.IsNullOrEmpty(attribute.ToString()))
                .ToDictionary(attribute => attribute.Name, attribute => attribute.ToString());
        }

        public string PushDataToDb(string path, Dictionary<string, string> parameters)
        {
            var url = "";// $"{GlobalConstants.ServerUrl}{path}";

            return ExecuteApiCall(url, parameters);
        }

        private string ExecuteApiCall(string url, Dictionary<string, string> parameters)
        {
            /*
            using (var client = new HttpClient())
            {

                //HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://[address]/");

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.ToString();
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int) response.StatusCode, response.ReasonPhrase);
                }
            }
            */
            return null;
        }

        private T DeserializeData<T>(string data)
        {
            //var serializer = new JavaScriptSerializer();
            // return serializer.Deserialize<T>(data);
            var re = new object();
            return (T)re;
        }
    }
}
