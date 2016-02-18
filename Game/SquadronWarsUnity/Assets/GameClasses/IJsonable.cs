using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Assets.GameClasses
{
    public interface IJsonable
    {
        string GetJsonObjectName();
        List<PropertyInfo> GetJsonObjectParameters();
        void SetJsonObjectParameters(Dictionary<string, object> parameters);
    }
}
