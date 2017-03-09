using Dibware.StoredProcedureFrameworkCore.Contracts;

namespace Dibware.StoredProcedureFrameworkCore.UnitTests.Fakes
{
    public class FakeStoredProcedureExecutor : IStoredProcedureExecutor
    {
        public string DefaultSchemaName => "dbo";

        public bool HasDefaultSchemaName => !string.IsNullOrEmpty(DefaultSchemaName);

        public TResultSetType ExecuteStoredProcedure<TResultSetType>(StoredProcedure<TResultSetType> procedure)
        {
            throw new System.NotImplementedException();
        }

        public TResultSetType ExecuteStoredProcedureFor<TResultSetType, TParameterType>(StoredProcedure<TResultSetType, TParameterType> storedProcedure,
            TParameterType parameters)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose() { /* No-Op */ }
        public bool Disposed => false;
    }
}