namespace Dibware.StoredProcedureFrameworkCore.Tests.Tests
{
    public class FakeStoredProcedureExecutor : IStoredProcedureExecutor
    {
        public string DefaultSchemaName => "dbo";

        public bool HasDefaultSchemaName => !string.IsNullOrEmpty(DefaultSchemaName);

        public TResultSetType ExecuteStoredProcedure<TResultSetType>(StoredProcedure<TResultSetType> procedure)
        {
            throw new System.NotImplementedException();
        }
    }
}