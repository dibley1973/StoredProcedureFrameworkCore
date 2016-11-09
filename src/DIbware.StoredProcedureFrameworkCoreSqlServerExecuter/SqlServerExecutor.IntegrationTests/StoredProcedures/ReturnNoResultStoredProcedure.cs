
using Dibware.StoredProcedureFrameworkCore.Types;

namespace Dibware.StoredProcedureFrameworkCore.SqlServerExecutor.IntegrationTests.StoredProcedures
{
    internal class ReturnNoResultStoredProcedure
        : StoredProcedure<
            NullStoredProcedureResult,
            NullStoredProcedureParameters>
    {
        public ReturnNoResultStoredProcedure(IStoredProcedureExecutor storedProcedureExecutor) 
            : base(storedProcedureExecutor)
        {
        }
    }
}
