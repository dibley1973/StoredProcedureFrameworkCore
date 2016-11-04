using System;
using System.Linq;
using System.Reflection;

namespace Dibware.StoredProcedureFrameworkCore.Extensions
{
    internal static class PropertyInfoExtensions
    {
        /// <summary>
        /// Get an attribute for a property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(this PropertyInfo propertyInfo)
            where T : Attribute
        {
            var attribute = propertyInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
            return (T)attribute;
        }
    }
}
