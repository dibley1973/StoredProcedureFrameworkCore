using System;
using Dibware.StoredProcedureFrameworkCore.Exceptions;
using Dibware.StoredProcedureFrameworkCore.Helpers;

namespace Dibware.StoredProcedureFrameworkCore
{
    public class StoredProcedure<TResultSetType, TParameterType> : StoredProcedure
    {
        private readonly IStoredProcedureExecutor _storedProcedureExecutor;

        public StoredProcedure(IStoredProcedureExecutor storedProcedureExecutor)
        {
            if (storedProcedureExecutor == null) throw new ArgumentNullException(nameof(storedProcedureExecutor));

            _storedProcedureExecutor = storedProcedureExecutor;

            SetProcedureName();
        }

        public TResultSetType ExecuteFor(TParameterType parameters)
        {
            return _storedProcedureExecutor.ExecuteStoredProcedureFor(this, parameters);
        }

        public string GetTwoPartName()
        {
            EnsureProcedureHasName();

            return string.Format("{0}.{1}", SchemaName, ProcedureName);
        }

        private void EnsureProcedureHasName()
        {
            if (HasProcedureName()) return;

            var message = "Stored procedure does not have a name set. Consider using " +
                nameof(SetProcedureName) +
                " before calling this method.";
            throw ExceptionHelper.CreateStoredProcedureConstructionException(message);
        }

        /// <summary>
        /// Ensures this instance is fully construcuted.
        /// </summary>
        /// <exception cref="StoredProcedureConstructionException">
        /// this instance is not fully constrcuted
        /// </exception>
        public void EnsureIsFullyConstructed()
        {
            if (IsFullyConstructed()) return;

            var message = ExceptionMessages.StoredProcedureIsNotFullyConstructed;
            throw ExceptionHelper.CreateStoredProcedureConstructionException(message);
        }

        private bool HasReturnType()
        {
            return ReturnType != null;
        }

        /// <summary>
        /// Determines if the procedure is fully constructed and in a valid 
        /// state which can be called and executed
        /// </summary>
        /// <returns></returns>
        public bool IsFullyConstructed()
        {
            return HasProcedureName() && HasReturnType();
        }

        /// <summary>
        /// Gets the type of object to be returned as the result.
        /// </summary>
        /// <value>
        /// The type of the result.
        /// </value>
        public Type ReturnType
        {
            get
            {
                return typeof(TResultSetType);
            }
        }
    }
}
