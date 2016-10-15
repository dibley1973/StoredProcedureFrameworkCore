namespace Dibware.StoredProcedureFrameworkCore.Tests.Fakes
{
    public class FakeStoredProcedureContext : StoredProcedureContext
    {
        public FakeStoredProcedureContext(IStoredProcedureExecutor storedProcedureExecutor)
            : base(storedProcedureExecutor)
        {

        }

        public StoredProcedure<string> Procedure1 { get; private set; }
    }
}