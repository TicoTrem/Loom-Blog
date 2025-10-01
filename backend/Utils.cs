using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace backend.utils
{
    public class Utils
    {
        public void patchInChanges(object ogObj, object newObj)
        {
            foreach (var p in newObj.GetType().GetProperties())
            {
                object? newValue = p.GetValue(newObj);
                PropertyInfo? bpProp = ogObj.GetType().GetProperty(p.Name);
                if (newValue == null || bpProp == null) continue;
                bpProp.SetValue(ogObj, newValue);
            }
        }
    }
}