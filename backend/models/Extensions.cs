using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace backend.models
{
    public static class Extensions
    {
        public static void Patch(this object target, object source)
        {
            foreach (var p in source.GetType().GetProperties())
            {
                object? newValue = p.GetValue(source);
                PropertyInfo? bpProp = target.GetType().GetProperty(p.Name);
                if (newValue == null || bpProp == null) continue;
                bpProp.SetValue(target, newValue);
            }
        }
    }
}