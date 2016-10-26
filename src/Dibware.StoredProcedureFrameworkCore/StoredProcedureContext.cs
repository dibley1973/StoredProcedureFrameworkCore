using System;
using System.Collections.Generic;
using System.Reflection;
using Dibware.StoredProcedureFrameworkCore.Extensions;
using Dibware.StoredProcedureFrameworkCore.Generics;
using Dibware.StoredProcedureFrameworkCore.Helpers.AttributeHelpers;
using System.Linq;
using Dibware.StoredProcedureFrameworkCore.StoredProcedureAttributes;

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
            SetStoredProcedureName(storedProcedurePropertyInfo, procedure);
            SetStoredProcedureSchemaName(storedProcedurePropertyInfo, procedure);

            storedProcedurePropertyInfo.SetValue(this, procedure, null);
        }

        private List<PropertyInfo> GetPropertiesWhichAreStoredProcedures()
        {
            var properties = GetType().GetProperties(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance);

            var storedProcedureProperties = new List<PropertyInfo>();

            foreach (var propertyInfo in properties)
            {
                var propertyIsNotGEnericType = !propertyInfo.PropertyType.IsConstructedGenericType;
                if (propertyIsNotGEnericType) continue;

                var propertyIsNotStoredProcedure = !typeof(StoredProcedure).IsAssignableFrom(propertyInfo.PropertyType);
                if (propertyIsNotStoredProcedure) continue;

                storedProcedureProperties.Add(propertyInfo);                
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

        private static string GetStoredProcedureName(PropertyInfo storedProcedurePropertyInfo)
        {
            var overriddenProcedureNameResult =
                GetOverriddenStoredProcedureName(storedProcedurePropertyInfo)
                    .Or(GetOverriddenStoredProcedureName(storedProcedurePropertyInfo.PropertyType));

            var defaultProcedureName = storedProcedurePropertyInfo.Name;
            var procedureName = overriddenProcedureNameResult.SingleOrDefault(defaultProcedureName);

            return procedureName;
        }

        private static Maybe<string> GetOverriddenStoredProcedureName(PropertyInfo storedProcedurePropertyInfo)
        {
            var finderResult = new PropertyNameAttributeFinder(storedProcedurePropertyInfo).GetResult();

            return finderResult.HasItem
                ? new Maybe<string>(finderResult.Single().Value)
                : new Maybe<string>();
        }

        private static Maybe<string> GetOverriddenStoredProcedureName(Type storedProcedurePropertyType)
        {
            var finderResult = new TypeNameAttributeFinder(storedProcedurePropertyType).GetResult();

            return finderResult.HasItem
                ? new Maybe<string>(finderResult.Single().Value)
                : new Maybe<string>();
        }

        private static Maybe<string> GetOverriddenStoredProcedureSchemaName(PropertyInfo storedProcedurePropertyInfo)
        {
            var finderResult =
               new PropertySchemaAttributeFinder(storedProcedurePropertyInfo).GetResult();

            return finderResult.HasItem
                ? new Maybe<string>(finderResult.Single().Value)
                : new Maybe<string>();
        }

        private static Maybe<string> GetOverriddenStoredProcedureSchemaName(Type storedProcedurePropertyType)
        {
            var finderResult =
                 new TypeSchemaAttributeFinder(storedProcedurePropertyType).GetResult();

            return finderResult.HasItem
                ? new Maybe<string>(finderResult.Single().Value)
                : new Maybe<string>();
        }

        private static void SetStoredProcedureName(PropertyInfo storedProcedurePropertyInfo, object procedure)
        {
            var name = GetStoredProcedureName(storedProcedurePropertyInfo);

            if (name != null)
            {
                ((StoredProcedure)procedure).SetProcedureName(name);
            }
            else
            {
                throw new NullReferenceException("procedure name was not set");
            }
        }

        private void SetStoredProcedureSchemaName(PropertyInfo storedProcedurePropertyInfo, object procedure)
        {
            string name = string.Empty;

            Maybe<string> overriddenProcedureSchemaResult =
                GetOverriddenStoredProcedureSchemaName(storedProcedurePropertyInfo)
                .Or(GetOverriddenStoredProcedureSchemaName(storedProcedurePropertyInfo.PropertyType));

            if (overriddenProcedureSchemaResult.HasItem)
            {
                name = overriddenProcedureSchemaResult.Single();
            }
            else if (_storedProcedureExecutor.HasDefaultSchemaName)
            {
                name = _storedProcedureExecutor.DefaultSchemaName;
            }

            ((StoredProcedure)procedure).SetSchemaName(name);
        }

    }
}
