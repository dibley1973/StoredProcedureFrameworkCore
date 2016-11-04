using System;
using Dibware.StoredProcedureFrameworkCore.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFrameworkCore.Helpers.AttributeHelpers
{
    public class TypeNameAttributeFinder : TypeAttributeFinder<NameAttribute>
    {
        public TypeNameAttributeFinder(Type type) : base(type)
        {
        }
    }
}