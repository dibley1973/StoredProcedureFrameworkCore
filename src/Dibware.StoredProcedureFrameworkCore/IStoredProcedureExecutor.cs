namespace Dibware.StoredProcedureFrameworkCore
{
    public interface IStoredProcedureExecutor
    {
        string DefaultSchemaName { get; }
        bool HasDefaultSchemaName { get; }
        TResultSetType ExecuteStoredProcedure<TResultSetType>(StoredProcedure<TResultSetType> procedure);
    }
}