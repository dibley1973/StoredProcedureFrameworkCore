﻿using System;
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
                return null; //TODO: implement typeof(TReturn);
            }
        }
    }
}