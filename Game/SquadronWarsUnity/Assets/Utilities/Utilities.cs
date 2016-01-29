using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking.NetworkSystem;
using Object = System.Object;

namespace SquadronWars.Game.SquadronWarsUnity.Assets.Utilities
{
    public class Utilities
    {
        /// <summary>
        /// Creates a dictionary of string objects containing only public parameters of the 
        /// object passed in, and their corresponding values.
        /// </summary>
        /// <param name="obj">Object to create param dictionary from</param>
        /// <returns>Dictionary&ltstring, string&gt</returns>
        public Dictionary<string, string> CreatePublicPropertyDictionary<T>(T obj)
        {
            return obj.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .Where(attribute => !string.IsNullOrEmpty(attribute.ToString()))
                .ToDictionary(attribute => attribute.Name, attribute => obj.GetType().GetProperty(attribute.Name).GetValue(obj, null).ToString().ToLower());
        }

        /// <summary>
        /// Creates a dictionary of string objects containing only private parameters of the 
        /// object passed in, and their corresponding values.
        /// </summary>
        /// <param name="obj">Object to create param dictionary from</param>
        /// <returns>Dictionary&ltstring, string&gt</returns>
        public Dictionary<string, string> CreatePrivatePropertyDictionary<T>(T obj)
        {
            return obj.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .Where(attribute => !string.IsNullOrEmpty(attribute.ToString()))
                .ToDictionary(attribute => attribute.Name, attribute => obj.GetType().GetProperty(attribute.Name).GetValue(obj, null).ToString().ToLower());
        }

        /// <summary>
        /// Creates a dictionary of string objects containing all parameters of the
        /// object passed in, and their corresponding values. 
        /// </summary>
        /// <param name="obj">Object to create the param dictionary from</param>
        /// <returns>Dictionary&ltstring, string$gt</returns>
        public Dictionary<string, string> CreateAllPropertyDictionary<T>(T obj)
        {
            return obj.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(attribute => !string.IsNullOrEmpty(attribute.ToString()))
                .ToDictionary(attribute => attribute.Name, attribute => obj.GetType().GetProperty(attribute.Name).GetValue(obj, null).ToString().ToLower());
        }

        public Dictionary<string, string> DeserializeData(string data)
        {
            var obj = new JSONObject(data);
            //var obj = JsonUtility.FromJson<object>(data);
            return obj.ToDictionary();
        }

        public T MapDataToObject<T>(string objData) where T : new()
        {
            var paramDictionary = DeserializeData(objData);
//            var paramDictionary = CreateAllPropertyDictionary(jsonObject);
            var obj = new T();

            foreach (var attribute in obj.GetType().GetProperties())
            {
                foreach (var param in paramDictionary.Where(param => attribute.Name.ToLower() == param.Key.ToLower()))
                    attribute.SetValue(obj, ConvertToType(attribute.GetType(), param.Value), null);
            }

            return obj;
        }

        public object ConvertToType(Type type, string obj)
        {
            if (type == typeof (int))
               return int.Parse(obj);
            if (type == typeof (bool))
                return bool.Parse(obj);

            return obj;
        }
    }
}
