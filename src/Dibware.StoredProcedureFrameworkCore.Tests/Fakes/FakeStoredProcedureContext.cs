using System.Collections.Generic;
using Dibware.StoredProcedureFrameworkCore.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFrameworkCore.Tests.Fakes
{
    public class FakeStoredProcedureContext : StoredProcedureContext
    {
        public FakeStoredProcedureContext(IStoredProcedureExecutor storedProcedureExecutor)
            : base(storedProcedureExecutor)
        { }

        public StoredProcedure<List<string>> Procedure1 { get; private set; }

        [Name("ProcedureX")]
        public StoredProcedure<List<string>> Procedure2 { get; private set; }

        [Schema("log")]
        public StoredProcedure<List<string>> Procedure3 { get; private set; }

        public List<string> NotAStoredProcedure { get; private set; }
        public string AlsoNotAStoredProcedure { get; private set; }
    }
}