
using System;

namespace Dibware.StoredProcedureFrameworkCore.SqlServerExecutor.Constants
{
    internal class ExceptionMessages
    {
        internal const string ErrorReadingStoredProcedure = "Error reading from target stored procedure '{0}' : '{1}' ";
        internal const string RecordSetListNotInstatiated = "The RecordSet list property '{0}' in ResultSet object '{1}' was not instantiated!";
        internal const string FieldNotFoundForName = "The ResultSet returned from the stored procedure does not contain a field of the name '{0}' found in return type '{1}' ";
        internal const string IncorrectReturnType = "Wrong return data type for field {0} in return type {1} ";
        internal const string MissingConstructor = "Type {0} does not have a parameterless constructor ";
        internal const string ProcessingReturnColumnError = "Exception processing return column {0} in {1} ";
    }
}