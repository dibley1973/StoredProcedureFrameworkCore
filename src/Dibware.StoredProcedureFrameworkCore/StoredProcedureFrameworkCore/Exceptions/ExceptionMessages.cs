namespace Dibware.StoredProcedureFrameworkCore.Exceptions
{
    internal class ExceptionMessages
    {
        internal const string FieldNotFoundForName = "The ResultSet returned from the stored procedure does not contain a field of the name '{0}' found in return type '{1}' ";
        internal const string FieldNotNullableTypeFormat = "Field '{0}' had an expected non-nullable type of '{1}' but DBNull.Value was returned for a nullable type of '{2}'";
        internal const string StoredProcedureIsNotFullyConstructed = "Stored procedure is not fully constructed! Ensure stored procedure has name and return type. ";
    }
}