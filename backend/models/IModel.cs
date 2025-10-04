using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace backend.models
{
    public interface IModel
    {

    }

    public static class ModelExtensions
    {
        /// <summary>
        /// A marker interface extension to allow Patching of a source objects properties to the target
        /// objects properties. Used primarily in PATCH request logic to bring Dto properties from the request
        /// to the object created from database entries.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        public static void Patch(this IModel target, IModel source)
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