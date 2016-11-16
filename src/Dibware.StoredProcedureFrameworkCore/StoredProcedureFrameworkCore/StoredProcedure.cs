using System;
using Dibware.StoredProcedureFrameworkCore.Exceptions;
using Dibware.StoredProcedureFrameworkCore.Helpers;

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

        protected bool HasProcedureName()
        {
            return !string.IsNullOrEmpty(ProcedureName);
        }

        protected StoredProcedure()
        {
            SetProcedureName();
        }

        protected void SetProcedureName()
        {
            var procedureName = GetType().Name;
            SetProcedureName(procedureName);
        }

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

    

    
}