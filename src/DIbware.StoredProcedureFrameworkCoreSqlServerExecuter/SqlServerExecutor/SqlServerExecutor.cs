using System;
using System.Data;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFrameworkCore.SqlServerExecutor
{
    public class SqlServerExecutor : IStoredProcedureExecutor
    {
        private  SqlConnection _sqlConnection;
        private readonly bool _ownsConnection;

        public SqlServerExecutor(string nameOrConnectionString)
        {
            if (string.IsNullOrWhiteSpace(nameOrConnectionString)) throw new ArgumentNullException(nameof(nameOrConnectionString));

            _sqlConnection = new SqlConnection(nameOrConnectionString);
            _ownsConnection = true;
        }

        public SqlServerExecutor(SqlConnection sqlConnection)
        {
            if (sqlConnection == null) throw new ArgumentNullException(nameof(sqlConnection));

            _sqlConnection = sqlConnection;
            _ownsConnection = false;
        }

        #region Dispose and Finalise

        /// <summary>
        /// Gets a value indicating whether this <see cref="SqlServerExecutor"/> 
        /// is disposed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if disposed; otherwise, <c>false</c>.
        /// </value>
        private bool Disposed { get; set; }

        /// <summary>
        /// Finalizes an instance of the <see cref="SqlServerExecutor"/> class.
        /// </summary>
        ~SqlServerExecutor()
        {
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, 
        /// or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; 
        /// <c>false</c> to release only unmanaged resources.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    if (_ownsConnection) DisposeConnection();
                }

                // There are no unmanaged resources to release, but
                // if we add them, they need to be released here.
            }
            Disposed = true;
        }

        private void DisposeConnection()
        {
            var connectionIsNotOwned = !_ownsConnection;
            if (connectionIsNotOwned) return;

            var connectionDoesNotNeedDisposing = _sqlConnection == null;
            if (connectionDoesNotNeedDisposing) return;

            var connectionStateIsNotclosed = _sqlConnection.State != ConnectionState.Closed;
            if (connectionStateIsNotclosed) _sqlConnection.Close();

            _sqlConnection.Dispose();
            _sqlConnection = null;
        }

        #endregion

        public string DefaultSchemaName => "dbo";
        public bool HasDefaultSchemaName => !string.IsNullOrWhiteSpace(DefaultSchemaName);

        //protected IDbCommand Command { get; private set; }


        //private bool HasSingleRecordSetOnly
        //{
        //    get { return _resultSetType.ImplementsICollectionInterface(); }
        //}

        //protected static bool HasNoReturnType
        //{
        //    get { return (typeof(TResultSetType) == typeof(NullStoredProcedureResult)); }
        //}

        //private bool HasParameters
        //{
        //    get { return _procedureParameters != null; }
        //}

        //private bool HasTransaction
        //{
        //    get { return _transaction != null; }
        //}


        //private void AddMoreInformativeInformationToExecuteError(ref Exception ex)
        //{
        //    //var detailedMessage = string.Format(
        //    //    ExceptionMessages.ErrorReadingStoredProcedure,
        //    //    _programmabilityObjectName,
        //    //    ex.Message);
        //    //Type exceptionType = ex.GetType();
        //    //var fieldInfo = exceptionType.GetField("_message", BindingFlags.Instance | BindingFlags.NonPublic);

        //    //var messageFieldFound = fieldInfo != null;
        //    //if (messageFieldFound) fieldInfo.SetValue(ex, detailedMessage);
        //}

        //private void CacheOriginalConnectionState()
        //{
        //    _connectionAlreadyOpen = (_sqlConnection.State == ConnectionState.Open);
        //}


        //private void CreateCommand()
        //{
        //    DisposeCommand();

        //    IDbCommandCreator creator = CreateCommandCreator();

        //    bool hasCommandTimeoutOverride = _commandTimeoutOverride.HasValue;
        //    if (hasCommandTimeoutOverride)
        //    {
        //        creator.WithCommandTimeout(_commandTimeoutOverride.Value);
        //    }

        //    if (HasParameters)
        //    {
        //        creator.WithParameters(_procedureParameters);
        //    }

        //    if (HasTransaction)
        //    {
        //        creator.WithTransaction(_transaction);
        //    }

        //    Command = creator
        //            .BuildCommand()
        //            .Command;
        //}

        //internal IDbCommandCreator CreateCommandCreator();

        //private void DisposeCommand()
        //{
        //    var noCommandToDispose = Command == null;
        //    if (noCommandToDispose) return;

        //    Command.Dispose();
        //    Command = null;
        //}

        //protected override void ExecuteCommand()
        //{
        //    if (HasNoReturnType)
        //    {
        //        ExecuteCommandWithNoReturnType();
        //        return;
        //    }

        //    ExecuteCommandWithResultSet();
        //}

        public TResultSetType ExecuteStoredProcedure<TResultSetType>(StoredProcedure<TResultSetType> storedProcedure)
        {
            if (storedProcedure == null) throw new ArgumentNullException(nameof(storedProcedure));
            if (Disposed) throw new ObjectDisposedException("Cannot call Execute when this object is disposed");

            var storedProcedureExecuter = new SqlServerStoredProcedureExecutor(_sqlConnection);
            return storedProcedureExecuter.ExecuteStoredProcedure(storedProcedure);
        }

        public TResultSetType ExecuteStoredProcedureFor<TResultSetType, TParameterType>(StoredProcedure<TResultSetType, TParameterType> storedProcedure,
            TParameterType parameters)
        {
            if (storedProcedure == null) throw new ArgumentNullException(nameof(storedProcedure));
            if (Disposed) throw new ObjectDisposedException("Cannot call Execute when this object is disposed");

            var storedProcedureExecuter = new SqlServerStoredProcedureExecutor(_sqlConnection);
            return storedProcedureExecuter.ExecuteStoredProcedureFor(storedProcedure, parameters);
        }
    }
}