using System;
using System.Collections.Generic;
using System.Reflection;

namespace Dibware.StoredProcedureFrameworkCore
{
    public abstract class StoredProcedureContext
    {
        private readonly List<PropertyInfo> _storedProcedureProperties;
        private readonly IStoredProcedureExecutor _storedProcedureExecutor;

        protected StoredProcedureContext(IStoredProcedureExecutor storedProcedureExecutor)
        {
            if (storedProcedureExecutor == null) throw new ArgumentNullException(nameof(storedProcedureExecutor));

            _storedProcedureExecutor = storedProcedureExecutor;
            _storedProcedureProperties = GetPropertiesWhichAreStoredProcedures();

            InitializeStoredProcedureProperties();
        }

        private void InitializeStoredProcedureProperties()
        {
            foreach (var storedProcedureProperty in _storedProcedureProperties)
            {
                InitializeStoredProcedureProperty(storedProcedureProperty);
            }
        }

        private void InitializeStoredProcedureProperty(PropertyInfo storedProcedurePropertyInfo)
        {
            var executerType = _storedProcedureExecutor.GetType();
            var constructorInfo = storedProcedurePropertyInfo.PropertyType.GetConstructor(new[] { executerType });
            if (constructorInfo == null) return;

            object procedure = constructorInfo.Invoke(new object[] { _storedProcedureExecutor });
            //SetStoredProcedureName(storedProcedurePropertyInfo, procedure);
            //SetStoredProcedureSchemaName(storedProcedurePropertyInfo, procedure);

            storedProcedurePropertyInfo.SetValue(this, procedure, null);
        }

        private List<PropertyInfo> GetPropertiesWhichAreStoredProcedures()
        {
            //var properties = GetType().GetTypeInfo().DeclaredProperties;
            var properties = GetType().GetProperties(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);

            var storedProcedureProperties = new List<PropertyInfo>();

            foreach (var propertyInfo in properties)
            {
                if (!propertyInfo.PropertyType.IsConstructedGenericType) continue;

                if (typeof(StoredProcedure).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    storedProcedureProperties.Add(propertyInfo);
                }

                if (propertyInfo.PropertyType == typeof(StoredProcedure))
                {
                    storedProcedureProperties.Add(propertyInfo);
                }
            }

            return storedProcedureProperties;
        }

        //static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        //{
        //    while (toCheck != null && toCheck != typeof(object))
        //    {
        //        var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
        //        if (generic == cur)
        //        {
        //            return true;
        //        }
        //        toCheck = toCheck.BaseType;
        //    }
        //    return false;
        //}

        //private static string GetStoredProcedureName(PropertyInfo storedProcedurePropertyInfo)
        //{
        //    Maybe<string> overriddenProcedureNameResult =
        //        GetOverriddenStoredProcedureName(storedProcedurePropertyInfo)
        //            .Or(GetOverriddenStoredProcedureName(storedProcedurePropertyInfo.PropertyType));

        //    string defaultProcedureName = storedProcedurePropertyInfo.Name;
        //    string procedureName = overriddenProcedureNameResult.SingleOrDefault(defaultProcedureName);

        //    return procedureName;
        //}


        //private static void SetStoredProcedureName(PropertyInfo storedProcedurePropertyInfo, object procedure)
        //{
        //    var name = GetStoredProcedureName(storedProcedurePropertyInfo);

        //    if (name != null)
        //    {
        //        ((StoredProcedureBase)procedure).SetProcedureName(name);
        //    }
        //    else
        //    {
        //        throw new NullReferenceException("procedure name was not set");
        //    }
        //}

        //private static void SetStoredProcedureSchemaName(PropertyInfo storedProcedurePropertyInfo, object procedure)
        //{
        //    Maybe<string> overriddenProcedureSchemaResult =
        //        GetOverriddenStoredProcedureSchemaName(storedProcedurePropertyInfo)
        //        .Or(GetOverriddenStoredProcedureSchemaName(storedProcedurePropertyInfo.PropertyType));

        //    if (overriddenProcedureSchemaResult.HasItem)
        //    {
        //        ((StoredProcedureBase)procedure).SetSchemaName(overriddenProcedureSchemaResult.Single());
        //    }
        //}

    }
}
