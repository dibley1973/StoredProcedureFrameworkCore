namespace Dibware.StoredProcedureFrameworkCore.Tests.Fakes
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