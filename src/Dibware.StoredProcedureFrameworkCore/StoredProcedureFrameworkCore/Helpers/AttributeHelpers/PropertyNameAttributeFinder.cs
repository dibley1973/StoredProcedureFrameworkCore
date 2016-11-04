using System.Reflection;
using Dibware.StoredProcedureFrameworkCore.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFrameworkCore.Helpers.AttributeHelpers
{
    public class PropertyNameAttributeFinder : PropertyAttributeFinder<NameAttribute>
    {
        public PropertyNameAttributeFinder(PropertyInfo property) : base(property) { }
    }
}