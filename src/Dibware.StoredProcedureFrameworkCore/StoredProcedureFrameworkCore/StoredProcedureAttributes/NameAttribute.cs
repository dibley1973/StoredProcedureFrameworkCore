using System;

namespace Dibware.StoredProcedureFrameworkCore.StoredProcedureAttributes
{
    /// <summary>
    /// Parameter name override. Default value for parameter name is the name of the
    /// property. This overrides that default with a user defined name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class |
        AttributeTargets.Struct |
        AttributeTargets.Property)]
    public class NameAttribute : Attribute
    {
        public string Value { get; private set; }

        public NameAttribute(string value)
        {
            Value = value;
        }
    }
}