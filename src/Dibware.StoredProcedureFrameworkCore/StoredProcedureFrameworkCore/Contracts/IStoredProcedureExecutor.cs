using System;

namespace Dibware.StoredProcedureFrameworkCore.Contracts
{
    public interface IStoredProcedureExecutor : IDisposable, IDisposed
    {
        string DefaultSchemaName { get; }
        bool HasDefaultSchemaName { get; }
        TResultSetType ExecuteStoredProcedure<TResultSetType>(StoredProcedure<TResultSetType> storedProcedure);
        TResultSetType ExecuteStoredProcedureFor<TResultSetType, TParameterType>(StoredProcedure<TResultSetType, TParameterType> storedProcedure, TParameterType parameters);
    }
}