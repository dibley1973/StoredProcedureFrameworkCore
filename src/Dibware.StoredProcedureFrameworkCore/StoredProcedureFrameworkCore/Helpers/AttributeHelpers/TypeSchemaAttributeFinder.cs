using System;
using Dibware.StoredProcedureFrameworkCore.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFrameworkCore.Helpers.AttributeHelpers
{
    public class TypeSchemaAttributeFinder : TypeAttributeFinder<SchemaAttribute>
    {
        public TypeSchemaAttributeFinder(Type type) : base(type)
        {
        }
    }
}