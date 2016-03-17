using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.GameClasses;
using UnityEngine;

namespace Assets.Data
{
    public class Utilities : MonoBehaviour
    {
        private JSONObject _jsonObject = new JSONObject(); 
      
        public JSONObject DeserializeData(string data)
        {
            var obj = new JSONObject(data);
            //var obj = JsonUtility.FromJson<object>(data);
            _jsonObject = obj;
            return obj;
        }

        public T BuildObjectFromJsonData<T>(string data) where T : IJsonable
        {
            var deserializedJson = DeserializeData(data);
            _jsonObject = deserializedJson;
            Debug.Log(deserializedJson.ToString());
            var obj = Activator.CreateInstance<T>();
            obj = (T) Decode(FindJsonObject(deserializedJson, GlobalConstants.GetJsonObjectName(obj)), typeof(T));

            if (obj == null)
                obj = (T) Decode(FindJsonObject(deserializedJson, GlobalConstants.GetJsonObjectName(typeof (T).Name.ToLower())), typeof(T));

            return obj;
        }

        private JSONObject FindJsonObject(JSONObject jsonObject, string objName)
        {
            JSONObject result = null;

            if (jsonObject.type == JSONObject.Type.NULL) return null; 

            foreach (var key in jsonObject.keys)
            {
                if (string.Equals(key, objName, StringComparison.CurrentCultureIgnoreCase))
                    result = jsonObject[key];
                else if (jsonObject[key].IsObject)
                    result = FindJsonObject(jsonObject[key], objName);

                if(result != null)                    
                    return result;
            }

            return result;
        }

        public object Decode(JSONObject obj, Type type)
        {
            if (obj == null || obj.type == JSONObject.Type.NULL)
                return null;

            var objectAttributes = GetParameterList(type);
                //type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Select(x => x.Name)
                //.ToList();;

            switch (obj.type)
            {
                case JSONObject.Type.OBJECT:
                    var builder = Activator.CreateInstance(type);

                    foreach (var param in objectAttributes.AsEnumerable())
                    {
                        JSONObject j = null;
                        //var keyIndex = obj.keys.Where(key => key.ToLower().Equals(param.Name.ToLower())).ToList();
                        var keyIndex = obj.keys.SingleOrDefault(key => key.ToLower().Equals(param.Name.ToLower()));

                        // If json object isnt found in the subset, search all the json data for it 
                        if (keyIndex == null)
                            j = FindJsonObject(_jsonObject, GlobalConstants.GetJsonObjectName(param.Name.ToLower()));
                        else
                            j = obj[keyIndex];

                        builder.GetType()
                            .GetProperty(param.Name)
                            .SetValue(builder, j != null 
                                ? Decode(j, builder.GetType().GetProperty(param.Name).PropertyType) 
                                : null,
                                null);
                    }

                    return builder;

                case JSONObject.Type.ARRAY:
                    var listBuilder = Activator.CreateInstance(type);
                    Type listType = type.GetGenericArguments().Single();
                    
                    foreach (var value in obj.list)
                    {
                        var item = Decode(value, listType);
                        if (item != null)
                            (listBuilder as System.Collections.IList).Add(item);
                    }
                        return listBuilder;

                case JSONObject.Type.STRING:
                    if(type != typeof(string))
                        return ChangeJsonType(obj, type);
                    else
                        return obj.str;

                case JSONObject.Type.NUMBER:
                    return obj.n;

                case JSONObject.Type.BOOL:
                    return obj.b;

                case JSONObject.Type.NULL:
                    return null;

            }

            return null;
        }

        public static List<PropertyInfo> GetParameterList(Type type)
        {
            switch (type.Name)
            {
                case "Character":
                    return ParameterLists.CharacterParams;
                case "CharacterData":
                    return ParameterLists.CharacterDataParams;
                case "Player":
                    return ParameterLists.PlayerParams;
                case "Item":
                    return ParameterLists.ItemParams;
                case "Ability":
                    return ParameterLists.AbilityParams;
                case "AbilityPreReq":
                    return ParameterLists.AbilityPreReqParams;
                case "InventoryElement":
                    return ParameterLists.InventoryElementParams;
                case "StartupData":
                    return type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static).ToList();
                default:
                    return type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).ToList();
            }            
        }

        public static class ParameterLists
        {
            static Func<Type, List<PropertyInfo>> buildParameterListFunc = (type) => type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).ToList();

            public static List<PropertyInfo> CharacterParams = buildParameterListFunc(typeof (Character));
            public static List<PropertyInfo> CharacterDataParams = buildParameterListFunc(typeof(StartupData.CharacterData));
            public static List<PropertyInfo> PlayerParams = buildParameterListFunc(typeof(Player));
            public static List<PropertyInfo> ItemParams = buildParameterListFunc(typeof(Item));
            public static List<PropertyInfo> AbilityParams = buildParameterListFunc(typeof(Ability));
            public static List<PropertyInfo> AbilityPreReqParams = buildParameterListFunc(typeof(AbilityPreReq));
            public static List<PropertyInfo> InventoryElementParams = buildParameterListFunc(typeof(StartupData.InventoryElement));

        }

        private object ChangeJsonType(JSONObject obj, Type type)
        {
            if (type == typeof (int))
                return int.Parse(obj.str); 
            if (type == typeof (bool))
                return bool.Parse(obj.str);

            return obj.str;
        }

        public static Type GetType(string TypeName)
        {

            // Try Type.GetType() first. This will work with types defined
            // by the Mono runtime, in the same assembly as the caller, etc.
            var type = Type.GetType(TypeName);

            // If it worked, then we're done here
            if (type != null)
                return type;

            // If the TypeName is a full name, then we can try loading the defining assembly directly
            if (TypeName.Contains("."))
            {

                // Get the name of the assembly (Assumption is that we are using 
                // fully-qualified type names)
                var assemblyName = TypeName.Substring(0, TypeName.IndexOf('.'));

                // Attempt to load the indicated Assembly
                var assembly = Assembly.Load(assemblyName);
                if (assembly == null)
                    return null;

                // Ask that assembly to return the proper Type
                type = assembly.GetType(TypeName);
                if (type != null)
                    return type;

            }

            // If we still haven't found the proper type, we can enumerate all of the 
            // loaded assemblies and see if any of them define the type
            var currentAssembly = Assembly.GetExecutingAssembly();
            var referencedAssemblies = currentAssembly.GetReferencedAssemblies();
            foreach (var assemblyName in referencedAssemblies)
            {

                // Load the referenced assembly
                var assembly = Assembly.Load(assemblyName);
                if (assembly != null)
                {
                    // See if that assembly defines the named type
                    type = assembly.GetType(TypeName);
                    if (type != null)
                        return type;
                }
            }

            // The type just couldn't be found...
            return null;

        }

        private bool IsIJsonable(Type type)
        {
            return type.GetInterfaces().Any(x =>
                x.IsGenericType &&
                x.GetGenericTypeDefinition() == typeof (IJsonable));
        }

        public string ObjectToString(object obj)
        {
            string attributes = "";
            foreach (var attribute in obj.GetType().GetProperties())
                string.Format(attributes += "{0}: {1} ", attribute.Name, attribute.ToString());

            return attributes;
        }

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

        public JSONObject CreateNestedJsonObject(object obj, Type type, string scope = "public")
        {
            var properties = new List<PropertyInfo>();
            var json = new JSONObject();
            switch (scope)
            {
                case "public":
                    properties = obj.GetType()
                        .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                        .Where(attribute => !string.IsNullOrEmpty(attribute.ToString())).ToList();
                    break;
                case "private":
                    properties = obj.GetType()
                        .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance)
                        .Where(attribute => !string.IsNullOrEmpty(attribute.ToString())).ToList();
                    break;
                case "all":
                    properties = obj.GetType()
                        .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | 
                            BindingFlags.Instance)
                        .Where(attribute => !string.IsNullOrEmpty(attribute.ToString())).ToList();
                    break;
            }

            foreach (var property in properties)
            {
                var attribute = property.GetValue(obj, null);
                var propertyType = attribute.GetType();
                if (propertyType.ToString().Contains("System.Collections.Generic.List"))
                {
                    var tmpJson = new JSONObject();
                    foreach (var element in (IEnumerable)property.GetValue(obj, null))
                    {
                        tmpJson.Add(CreateNestedJsonObject(element,
                            property.PropertyType.GetGenericArguments().Single(), scope));
                    }
                    json.AddField(property.Name, tmpJson);
                }
                if (propertyType.ToString().StartsWith("System"))
                    json.AddField(property.Name, attribute.ToString());
                else
                    json.AddField(property.Name,
                        CreateNestedJsonObject(attribute, propertyType, scope));
            }

            return json;
        } 

        /// <summary>
        /// Creates a dictionary of string objects containing only private parameters of the 
        /// object passed in, and their corresponding values.
        /// </summary>
        /// <param name="obj">Object to create param dictionary from</param>
        /// <returns>Dictionary&ltstring, string&gt</returns>
        public Dictionary<string, string> CreatePrivatePropertyDictionary<T>(T obj)
        {
            return obj.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance)
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

        public T MapJsonToObject<T>(ref T obj)
        {
            //var obj = new T(); 
            var attributes = GetParameterList(typeof (T));
                //obj.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            foreach (
                var attribute in
                    attributes.Where(atr => _jsonObject.keys.Select(key => key.ToLower()).Contains(atr.Name.ToLower())))
            {
                if (attribute.PropertyType == typeof(int) || attribute.PropertyType == typeof(string) ||
                    attribute.PropertyType == typeof(bool))
                {
                    var key = _jsonObject.keys.Single(k => k.ToLower() == attribute.Name.ToLower());

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

            for (var i = 0; i < jsonObject.list.Count; i++)
            {
                if (!jsonObject[i].IsNull)
                {
                    if (jsonObject[i].IsObject || jsonObject[i].IsArray)
                    {
                        //string keyObject = jsonObject[key].keys[i];
                        //JSONObject listObject = item.list[i];
                        var flattenResult = FlattenJsonObject(jsonObject[i]);
                        if (!flattenResult.IsNull)
                            for (var x = 0; x < flattenResult.list.Count; x++)
                            {
                                flattenedJson.Add(flattenResult[x]);
                            }
                    }
                    else
                        flattenedJson.Add(jsonObject);
                }
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

        public Dictionary<string, JSONObject> GetMatchingJsonAttributes(JSONObject jsonObject, System.Collections.Generic.List<PropertyInfo> attributes)
        {
            var parsedJson = new Dictionary<string, JSONObject>();
            foreach (var key in jsonObject.keys.Where(key => attributes.Select(attribute => attribute.Name.ToLower())
                .Contains(key.ToLower())))
            {
                parsedJson.Add(attributes.Single(attribute => attribute.Name.ToLower() == key.ToLower()).Name, jsonObject[key]);
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

        public Character GetCharacterById(int id)
        {
            return GlobalConstants.Player.Characters.Single(x => x.CharacterId == id);
        }
    }
} 