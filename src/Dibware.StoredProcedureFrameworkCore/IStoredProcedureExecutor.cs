namespace Dibware.StoredProcedureFrameworkCore
{
    public interface IStoredProcedureExecutor
    {
        TResultSetType ExecuteStoredProcedure<TResultSetType>(StoredProcedure<TResultSetType> procedure);
    }
}