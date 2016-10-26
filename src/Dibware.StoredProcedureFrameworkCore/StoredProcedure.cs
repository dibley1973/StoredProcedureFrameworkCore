using System;

namespace Dibware.StoredProcedureFrameworkCore
{
    public abstract class StoredProcedure
    {
        /// <summary>
        /// Gets the name of the procedure.
        /// </summary>
        /// <value>
        /// The name of the procedure.
        /// </value>
        public string ProcedureName { get; private set; }

        /// <summary>
        /// Gets the name of the schema.
        /// </summary>
        /// <value>
        /// The name of the schema.
        /// </value>
        public string SchemaName { get; private set;  }

        /// <summary>
        /// Sets the procedure name.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <returns>
        /// This instance
        /// </returns>
        /// <exception cref="System.ArgumentNullException">procedureName</exception>
        public void SetProcedureName(string procedureName)
        {
            // Validate argument
            if (procedureName == null) throw new ArgumentNullException(nameof(procedureName));
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException(nameof(procedureName));

            ProcedureName = procedureName;
        }

        /// <summary>
        /// Sets the schema name.
        /// </summary>
        /// <param name="schemaName">The name of the schema.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">schemaName</exception>
        public void SetSchemaName(string schemaName)
        {
            // Validate argument
            if (schemaName == null) throw new ArgumentNullException(nameof(schemaName));
            if (schemaName == string.Empty) throw new ArgumentOutOfRangeException(nameof(schemaName));

            SchemaName = schemaName;
        }
    }

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
            return _storedProcedureExecutor.ExecuteStoredProcedure<TResultSetType>(this);
        }
    }
}