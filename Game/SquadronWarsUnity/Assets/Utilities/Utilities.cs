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
        private JSONObject _jsonObject = new JSONObject(); 
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
            var deserializedJson = DeserializeData(data);
            Debug.Log(deserializedJson.ToString());
            _jsonObject = FlattenJsonObject(deserializedJson);
            var obj = new T();
            obj = (T) Decode(_jsonObject, typeof(T));

            return obj;
        }

        public T MapJsonToObject<T>(ref T obj) 
        {
            //var obj = new T(); 
            var attributes =
                obj.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            foreach (
                var attribute in
                    attributes.Where(atr => _jsonObject.keys.Select(key => key.ToLower()).Contains(atr.Name.ToLower())))
            {
                Debug.Log(attribute.Name);
                if (attribute.PropertyType == typeof (int) || attribute.PropertyType == typeof (string) ||
                    attribute.PropertyType == typeof (bool))
                {
                    var key = _jsonObject.keys.Single(k => k.ToLower() == attribute.Name.ToLower());

                    Debug.Log(key);

                    obj.GetType()
                        .GetProperty(attribute.Name)
                        .SetValue(obj, ConvertToType(attribute.PropertyType, _jsonObject[key].ToString()), null);
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
            var index = 0;

            foreach (var item in jsonObject.list)
            {
                //Debug.Log(item.ToString());
                if (!item.IsNull)
                {
                    if (item.IsObject)
                    {
                        //string keyObject = jsonObject[key].keys[i];
                        //JSONObject listObject = item.list[i];
                        var flattenResult = FlattenJsonObject(item);
                        if(!flattenResult.IsNull)
                            foreach (var resultList in flattenResult.list)
                                flattenedJson.Add(resultList);
                    }
                    else if (!item.IsNull)
                        flattenedJson.Add(jsonObject[index]);
                }

                index++;
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
        }
        
        public Dictionary<string, JSONObject> GetMatchingJsonAttributes(JSONObject jsonObject, List<PropertyInfo> attributes)
        {
            var parsedJson = new Dictionary<string, JSONObject>();
            foreach (var key in jsonObject.keys.Where(key => attributes.Select(attribute => attribute.Name.ToLower())
                .Contains(key.ToLower())))
            {
                parsedJson.Add(attributes.Single(attribute => attribute.Name.ToLower() == key.ToLower()).Name, jsonObject[key]);
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

        public object Decode(JSONObject obj, Type type)
        {
            var builder = Activator.CreateInstance(type);
            switch (obj.type)
            {
                case JSONObject.Type.OBJECT:
                    var objectAttributes =
                        type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                            .ToList()
                            .Where(attribute => obj.keys.Select(key => key.ToLower()).Contains(attribute.Name.ToLower()));

                    foreach(var param in objectAttributes)
                    {
                        var temp = Activator.CreateInstance(param.PropertyType);
                        var j = obj[obj.keys.Single(key => key.ToLower() == param.Name.ToLower())];
                        Decode(j, param.PropertyType);
                        builder.GetType().GetProperty(param.Name).SetValue(builder, temp, null);
                    }
                    break; 
                case JSONObject.Type.ARRAY:
                    var array = new object[obj.list.Count];
                    foreach (var t in obj.list)
                        Decode(t, type.GetElementType());

                    builder = array;
                    break;
                case JSONObject.Type.STRING:
                    builder = obj.str;
                    break;
                case JSONObject.Type.NUMBER:
                    builder = obj.n;
                    break;
                case JSONObject.Type.BOOL:
                    builder = obj.b;
                    break;
            }

            return builder;
        }
    }
}
