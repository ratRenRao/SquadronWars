using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.GameClasses;
using UnityEngine;

namespace Assets.Utilities
{
    public class Utilities
    {
        private JSONObject JsonObject { get; set; }
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

        public JSONObject DeserializeData(string data)
        {
            var obj = new JSONObject(data);
            Debug.Log("New Object = " + obj.Count);
            //var obj = JsonUtility.FromJson<object>(data);
            return obj;
        }

        public IJsonable BuildObjectFromJsonData<T>(string data) where T : IJsonable, new()
        {
            JsonObject = FlattenJsonObject(DeserializeData(data));
            var obj = new T();
            obj = MapJsonToObject(ref obj);

            return obj;
        }

        public T MapJsonToObject<T>(ref T obj) 
        {
            //var obj = new T(); 
            var attributes =
                obj.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            foreach (
                var attribute in
                    attributes.Where(atr => JsonObject.keys.Select(key => key.ToLower()).Contains(atr.Name.ToLower())))
            {
                if (attribute.PropertyType == typeof (int) || attribute.PropertyType == typeof (string) ||
                    attribute.PropertyType == typeof (bool))
                {
                    var value = JsonObject[JsonObject.keys.Single(key => key.ToLower() == attribute.Name.ToLower())];

                    obj.GetType()
                        .GetProperty(attribute.Name)
                        .SetValue(obj, ConvertToType(attribute.PropertyType, value.str), null);
                }
                else
                {
                    var subObject = obj.GetType().GetProperty(attribute.Name);
                    var subValue = MapJsonToObject(ref subObject);

                    obj.GetType()
                        .GetProperty(attribute.Name)
                        .SetValue(obj, subValue, null);
                }
            }

            return obj;
        }

        public JSONObject FlattenJsonObject(JSONObject jsonObject)
        {
            var flattenedJson = new JSONObject();

            foreach (var key in jsonObject.keys)
            {
                if (jsonObject[key].keys.Count > 1)
                {
                    var flattenResult = FlattenJsonObject(jsonObject[key]);
                    foreach(var resultKey in flattenResult.keys)
                        flattenedJson.Add(flattenResult[resultKey]);
                }
                else
                    flattenedJson.Add(jsonObject[key]);
            }

            return flattenedJson;
        }

        public object ConvertToType(Type type, string value)
        {
            if (type == typeof(int))
                return int.Parse(value);
            if (type == typeof(bool))
                return bool.Parse(value);

            return value;

/*            switch (jsonObject.type)
            {
                case JSONObject.Type.OBJECT:
                    var jsonDictionary = new Dictionary<string, jsonObject.ct>();
                    for (var i = 0; i < jsonObject.list.Count; i++)
                    {
                        var key = jsonObject.keys[i];
                        var j = jsonObject.list[i];
                        jsonDictionary.Add(key, Decode(j));
                    }
                    return jsonDictionary;
                case JSONObject.Type.ARRAY:
                    var jsonArray = new jsonObject.ct[jsonObject.list.Count];
                    for (var i = 0; i < jsonObject.list.Count; i++)
                    {
                        jsonArray[i] = Decode(jsonObject.list[i]).Values;
                    }
                    return new Dictionary<string, jsonObject.ct>() { { "TEMP", jsonArray.ToArray() } };
                case JSONObject.Type.STRING:
                    return new Dictionary<string, jsonObject.ct>() { { "TEMP", jsonObject.ToString() } };
                case JSONObject.Type.NUMBER:
                    return new Dictionary<string, jsonObject.ct>() { { "TEMP", int.Parse(jsonObject.ToString()) } };
                case JSONObject.Type.BOOL:
                    return new Dictionary<string, jsonObject.ct>() { { "TEMP", bool.Parse(jsonObject.ToString()) } };
                case JSONObject.Type.NULL:
                    return new Dictionary<string, jsonObject.ct>() { { "TEMP", null } };
                case JSONObject.Type.BAKED:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }  */
        }

        public Dictionary<string, JSONObject> TraverseJsonForObjectMapping<T>(Dictionary<string, JSONObject> jsonDictionary, ref T obj)
        {
            foreach (var kvp in jsonDictionary)
            {
                if (obj.GetType().Equals(JSONObject.Type.OBJECT))
                    jsonDictionary.Concat(ToSingleDimensionDictionary(kvp.Value));
                else
                {
                   // jsonDictionary.Add(kvp.Key, obj[kvp.Key]);
                }
            }
            return jsonDictionary;
        }
        
        public JSONObject GetMatchingJsonAttributes<T>(JSONObject jsonObject)
        {
            Debug.Log("Entered getmatchingjsonatributes");
            var parsedJson = new JSONObject();
            //var jsonDictionary = ToSingleDimensionDictionary(jsonObject); 
            var objectAttributes =
                typeof(T).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).ToList();
            Debug.Log("Obj count" + jsonObject.Count);
            foreach (var key in jsonObject.keys.Where(key => objectAttributes.Select(attribute => attribute.Name.ToLower())
                .Contains(key.ToLower())))
            {
                parsedJson.AddField(key, jsonObject[key]);
                Debug.Log(jsonObject[key]);
            }

            return parsedJson;
        }

        /*
        public JSONObject SyncAttributeName<T>(JSONObject jsonObject)
        {
            var objectAttributes = typeof(T).GetProperties().ToList();
            foreach (var key in jsonObject.keys.Where(key => objectAttributes.Select(attribute => attribute.Name.ToLower())
                 .Contains(key.ToLower())))
            {
                jsonObject[key].;
            }
        } */

        public JSONObject GetJsonObjectSubset(JSONObject obj, string field)
        {
            foreach (var param in obj.keys)
            {
                if (param.ToLower() == field.ToLower())
                {
                    return obj[param];
                }
                else
                {
                    var foundObject = GetJsonObjectSubset(obj[param], field);
                    if (foundObject.keys.Count > 0)
                        return foundObject;
                }
            }
            return null;
        }

        public Dictionary<string, JSONObject> ToSingleDimensionDictionary(JSONObject obj)
        {
            var jsonDictionary = new Dictionary<string, JSONObject>();
            foreach (var key in obj.keys)
            {
                if (obj.type.Equals(JSONObject.Type.OBJECT))
                    jsonDictionary.Concat(ToSingleDimensionDictionary(obj[key]));
                else
                {
                    jsonDictionary.Add(key, obj[key]); 
                }
            }
            return jsonDictionary;
        }

        public Dictionary<string, object> Decode(JSONObject obj)
        {
            switch (obj.type)
            {
                case JSONObject.Type.OBJECT:
                    var jsonDictionary = new Dictionary<string, object>();
                    for (var i = 0; i < obj.list.Count; i++)
                    {
                        var key = obj.keys[i];
                        var j = obj.list[i];
                        jsonDictionary.Add(key, Decode(j));
                    }
                    return jsonDictionary;
                case JSONObject.Type.ARRAY:
                    var jsonArray = new object[obj.list.Count];
                    for (var i = 0; i < obj.list.Count; i++)
                    {
                        jsonArray[i] = Decode(obj.list[i]).Values;
                    }
                    return new Dictionary<string, object>() { { "TEMP", jsonArray.ToArray() } };
                case JSONObject.Type.STRING:
                    return new Dictionary<string, object>() { { "TEMP", obj.ToString() } };
                case JSONObject.Type.NUMBER:
                    return new Dictionary<string, object>() { { "TEMP", int.Parse(obj.ToString())} };
                case JSONObject.Type.BOOL:
                    return new Dictionary<string, object>() { { "TEMP", bool.Parse(obj.ToString()) } };
                case JSONObject.Type.NULL:
                    return new Dictionary<string, object>() { { "TEMP", null } };
                case JSONObject.Type.BAKED:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return null;
        }
    }
}
