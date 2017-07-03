using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

/*
 * http://www.opensource.org/licenses/lgpl-2.1.php
 * JSONTemplates class
 * for use with Unity
 * Copyright Matt Schoen 2010
 */

public static partial class JSONTemplates {
	static readonly HashSet<object> touched = new HashSet<object>();

	public static JSONObject TOJSON(object obj) {		//For a generic guess
		if(touched.Add(obj)) {
			var result = JSONObject.obj;
			//Fields
			var fieldinfo = obj.GetType().GetFields();
			foreach(var fi in fieldinfo) {
				var val = JSONObject.nullJO;
				if(!fi.GetValue(obj).Equals(null)) {
					var info = typeof(JSONTemplates).GetMethod("From" + fi.FieldType.Name);
					if(info != null) {
						var parms = new object[1];
						parms[0] = fi.GetValue(obj);
						val = (JSONObject)info.Invoke(null, parms);
					} else if(fi.FieldType == typeof(string))
						val = JSONObject.CreateStringObject(fi.GetValue(obj).ToString());
					else
						val = JSONObject.Create(fi.GetValue(obj).ToString());
				}
				if(val) {
					if(val.ObjectType != JSONObject.JsonType.NULL)
						result.AddField(fi.Name, val);
					else Debug.LogWarning("Null for this non-null object, property " + fi.Name + " of class " + obj.GetType().Name + ". Object type is " + fi.FieldType.Name);
				}
			}
			//Properties
			var propertyInfo = obj.GetType().GetProperties();
			foreach(var pi in propertyInfo) {
				//This section should mirror part of AssetFactory.AddScripts()
				var val = JSONObject.nullJO;
				if(!pi.GetValue(obj, null).Equals(null)) {
					var info = typeof(JSONTemplates).GetMethod("From" + pi.PropertyType.Name);
					if(info != null) {
						var parms = new object[1];
						parms[0] = pi.GetValue(obj, null);
						val = (JSONObject)info.Invoke(null, parms);
					} else if(pi.PropertyType == typeof(string))
						val = JSONObject.CreateStringObject(pi.GetValue(obj, null).ToString());
					else
						val = JSONObject.Create(pi.GetValue(obj, null).ToString());
				}
				if(val) {
					if(val.ObjectType != JSONObject.JsonType.NULL)
						result.AddField(pi.Name, val);
					else Debug.LogWarning("Null for this non-null object, property " + pi.Name + " of class " + obj.GetType().Name + ". Object type is " + pi.PropertyType.Name);
				}
			}
			return result;
		} 
		Debug.LogWarning("trying to save the same data twice");
		return JSONObject.nullJO;
	}
}

/*
 * Some helpful code templates for the JSON class
 * 
 * LOOP THROUGH OBJECT
for(int i = 0; i < obj.Count; i++){
	if(obj.keys[i] != null){
		switch((string)obj.keys[i]){
			case "key1":
				do stuff with (JSONObject)obj.list[i];
				break;
			case "key2":
				do stuff with (JSONObject)obj.list[i];
				break;		
		}
	}
}
 *
 * LOOP THROUGH ARRAY
foreach(JSONObject ob in obj.list)
	do stuff with ob;
 */
