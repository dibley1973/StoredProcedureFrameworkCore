using System.Reflection;
using Dibware.StoredProcedureFrameworkCore.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFrameworkCore.Helpers.AttributeHelpers
{
    public class PropertySchemaAttributeFinder : PropertyAttributeFinder<SchemaAttribute>
    {
        public PropertySchemaAttributeFinder(PropertyInfo property)  : base(property) { }
    }
}