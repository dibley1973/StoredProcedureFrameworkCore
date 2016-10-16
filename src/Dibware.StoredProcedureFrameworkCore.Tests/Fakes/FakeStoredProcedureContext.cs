using System.Collections.Generic;

namespace Dibware.StoredProcedureFrameworkCore.Tests.Fakes
{
    public class FakeStoredProcedureContext : StoredProcedureContext
    {
        public FakeStoredProcedureContext(IStoredProcedureExecutor storedProcedureExecutor)
            : base(storedProcedureExecutor)
        { }

        public StoredProcedure<List<string>> Procedure1 { get; private set; }
        public List<string> NotAStoredProcedure { get; private set; }
        public string AlsoNotAStoredProcedure { get; private set; }
    }
}