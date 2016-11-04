using System;
using System.Collections.Generic;
using System.Reflection;
using Dibware.StoredProcedureFrameworkCore.StoredProcedureAttributes;
using System.Linq;

namespace Dibware.StoredProcedureFrameworkCore.Extensions
{
    /// <summary>
    /// provides extension methods for the <see cref="System.Type"/>
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Get properties of a type that do not have the 'NotMapped' attribute
        /// </summary>
        /// <param name="instance">Type to examine for properites</param>
        /// <returns>Array of properties that can be filled</returns>
        public static PropertyInfo[] GetMappedProperties(this Type instance)
        {
            var allProperties = instance.GetProperties();
            var mappedProperties = allProperties
                .Where(p => p.GetAttribute<NotMappedAttribute>() == null)
                .Select(p => p);

            return mappedProperties.ToArray();
        }

        /// <summary>
        /// Gets a value indicating if the instance implementses the ICollection interface.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">instance</exception>
        public static bool ImplementsICollectionInterface(this Type instance)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));

            foreach (var @interface in instance.GetTypeInfo().GetInterfaces())
            {
                var interfaceIsNotGenericType = !@interface.GetTypeInfo().IsGenericType;
                if (interfaceIsNotGenericType) continue;

                var interfaceIsOfICollectionType = @interface.GetGenericTypeDefinition() == typeof(ICollection<>);
                if (interfaceIsOfICollectionType) return true;
            }

            return false;
        }
    }
}
