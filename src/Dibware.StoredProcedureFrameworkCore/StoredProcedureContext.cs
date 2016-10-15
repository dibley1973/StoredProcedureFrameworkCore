using System;
using System.Collections.Generic;
using System.Reflection;

namespace Dibware.StoredProcedureFrameworkCore
{
    public class StoredProcedureContext
    {
        private static List<PropertyInfo> _storedProcedureProperties;
        private readonly IStoredProcedureExecutor _storedProcedureExecutor;

        static StoredProcedureContext()
        {
            _storedProcedureProperties = GetPropertiesWhichAreStoredProcedures();
        }

        public StoredProcedureContext(IStoredProcedureExecutor storedProcedureExecutor)
        {
            if (storedProcedureExecutor == null) throw new ArgumentNullException(nameof(storedProcedureExecutor));

            _storedProcedureExecutor = storedProcedureExecutor;

            InitializeStoredProcedures();
        }

        private void InitializeStoredProcedures()
        {
            //var assembly = GetType().GetTypeInfo().Assembly;
            //var properties = GetType().GetTypeInfo().DeclaredProperties;
            //var storedProcedureProperties = GetPropertiesWhichAreStoredProcedures();

            foreach (var storedProcedureProperty in _storedProcedureProperties)
            {
                //storedProcedureProperty.GetCustomAttribute()

                //CustomAttributeData c = new CustomAttributeData();
                //c.Constructor
                //var constructorInfo = storedProcedureProperty.Getc PropertyType.GetConstructor(new[] { contextType });


            }

            throw new NotImplementedException();
        }

        private static List<PropertyInfo> GetPropertiesWhichAreStoredProcedures()
        {
            var properties = typeof(StoredProcedureContext).GetTypeInfo().DeclaredProperties;
            var storedProcedureProperties = new List<PropertyInfo>();

            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.PropertyType == typeof(StoredProcedure))
                {
                    storedProcedureProperties.Add(propertyInfo);
                }
            }

            return storedProcedureProperties;
        }
    }
}
