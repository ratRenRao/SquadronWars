using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.GameClasses;

namespace Assets.GameClasses
{
    public class Ability : IEffectable, IJsonable
    {
        public string Name { get; set; }
        public int AbilityId { get; set; }
        public int CharacterId { get; set; }
        public int AbilityLevel { get; set; }

        public void Execute(ref Stats characterStats)
        {
            throw new NotImplementedException();
        }

        public void ImmediateEffect(ref Stats characterStats)
        {
            throw new NotImplementedException();
        }

        public void LingeringEffect(ref Stats characterStats)
        {
            throw new NotImplementedException();
        }

        public void RemoveEffect(ref Stats characterStats)
        {
            throw new NotImplementedException();
        }

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