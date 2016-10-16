namespace Dibware.StoredProcedureFrameworkCore.Tests.Tests
{
    public class FakeStoredProcedureExecutor : IStoredProcedureExecutor
    {
        public TResultSetType ExecuteStoredProcedure<TResultSetType>(StoredProcedure<TResultSetType> procedure)
        {
            throw new System.NotImplementedException();
        }
    }
}