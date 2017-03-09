using System;
using Dibware.StoredProcedureFrameworkCore.Contracts;

namespace Dibware.StoredProcedureFrameworkCore
{
    public class StoredProcedure<TResultSetType> : StoredProcedure
    {
        private readonly IStoredProcedureExecutor _storedProcedureExecutor;

        public StoredProcedure(IStoredProcedureExecutor storedProcedureExecutor)
        {
            if (storedProcedureExecutor == null) throw new ArgumentNullException(nameof(storedProcedureExecutor));

            _storedProcedureExecutor = storedProcedureExecutor;
        }

        public TResultSetType Execute()
        {
            return _storedProcedureExecutor.ExecuteStoredProcedure(this);
        }
    }
}