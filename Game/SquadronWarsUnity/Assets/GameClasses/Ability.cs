using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.GameClasses;

namespace Assets.GameClasses
 {
    public class Ability : Effect, IJsonable
    {
        public string Name
        {
            get { return base.Name; } 
            set { base.Name = value; }
        }
        public int AbilityId { get; set; }
        public int CharacterId { get; set; }
        public int AbilityLevel { get; set; }


        public string GetJsonObjectName()
        {
            return "Abilities";
        }

        public List<PropertyInfo> GetJsonObjectParameters()
        {
            return GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).ToList();
        }

        public void SetJsonObjectParameters(Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public string GetJSONString()
        {
            // removed from string       ", \"CharacterId\" : " + CharacterId +
            return "{ \"Name\" : \"" + Name + "\", \"AbilityId\" : \"" + AbilityId + "\", \"AbilityLevel\" : \"" + AbilityLevel +  "\" }";
        }
    }
}

public class AbilityPreReq : IJsonable
{
    public int AbilityId { get; set; }
    public int PrereqAbilityId { get; set; }
    public int PrereqAbilityLevel { get; set; }
    public string GetJsonObjectName()
    {
        return "AbilityPreReqs";
    }

    public List<PropertyInfo> GetJsonObjectParameters()
    {
        throw new NotImplementedException();
    }

    public void SetJsonObjectParameters(Dictionary<string, object> parameters)
    {
        throw new NotImplementedException();
    }
}