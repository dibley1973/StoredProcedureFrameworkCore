using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Dibware.StoredProcedureFrameworkCore.Contracts;
using Dibware.StoredProcedureFrameworkCore.SqlServerExecutor.Constants;

namespace Dibware.StoredProcedureFrameworkCore.SqlServerExecutor
{
    public class SqlServerStoredProcedureExecutor : IStoredProcedureExecutor
    {
        private SqlConnection _sqlConnection;
        private readonly bool _ownsConnection;
        private bool _connectionAlreadyOpen;
        private string _programmabilityObjectName;
        protected IDbCommand Command { get; private set; }
        public bool Disposed { get; private set; }

        public SqlServerStoredProcedureExecutor(string nameOrConnectionString)
        {
            if (string.IsNullOrWhiteSpace(nameOrConnectionString)) throw new ArgumentNullException(nameof(nameOrConnectionString));

            _sqlConnection = new SqlConnection(nameOrConnectionString);
            _ownsConnection = true;

        }

        public SqlServerStoredProcedureExecutor(SqlConnection sqlConnection)
        {
            if (sqlConnection == null) throw new ArgumentNullException(nameof(sqlConnection));

            _sqlConnection = sqlConnection;

            _ownsConnection = _sqlConnection.State != ConnectionState.Open;

        }

        public string DefaultSchemaName => "dbo";
        public bool HasDefaultSchemaName => !string.IsNullOrWhiteSpace(DefaultSchemaName);

        public TResultSetType ExecuteStoredProcedure<TResultSetType>(StoredProcedure<TResultSetType> storedProcedure)
        {
            if (storedProcedure == null) throw new ArgumentNullException(nameof(storedProcedure));

            //return ExecuteStoredProcedureFor<TResultSetType, NullStoredProcedureParameters>(storedProcedure);
            throw new NotImplementedException();
        }

        public TResultSetType ExecuteStoredProcedureFor<TResultSetType, TParameterType>(StoredProcedure<TResultSetType, TParameterType> storedProcedure,
            TParameterType parameters)
        {
            if (storedProcedure == null) throw new ArgumentNullException(nameof(storedProcedure));
            if (Disposed) throw new ObjectDisposedException("Cannot call Execute when this object is disposed");

            storedProcedure.EnsureIsFullyConstructed();

            _programmabilityObjectName = storedProcedure.GetTwoPartName();

            CacheOriginalConnectionState();

            try
            {
                OpenClosedConnection();
                CreateCommand();
                //ExecuteCommand();
            }
            catch (Exception ex)
            {
                AddMoreInformativeInformationToExecuteError(ref ex);
                throw;
            }
            finally
            {
                DisposeCommand();
                RestoreOriginalConnectionState();
            }
            
            throw new NotImplementedException();
        }

        ~SqlServerStoredProcedureExecutor()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (Disposed) return;

            if (disposing)
            {
                // Free other state (managed objects).
            }
            // Free your own state (unmanaged objects).
            // Set large fields to null.
            CleanUpConnectionIfOwned();

            Disposed = true;
        }

        private void AddMoreInformativeInformationToExecuteError(ref Exception ex)
        {
            var detailedMessage = string.Format(
                ExceptionMessages.ErrorReadingStoredProcedure,
                _programmabilityObjectName,
                ex.Message);
            Type exceptionType = ex.GetType();
            var fieldInfo = exceptionType.GetField("_message", BindingFlags.Instance | BindingFlags.NonPublic);

            if (fieldInfo != null) fieldInfo.SetValue(ex, detailedMessage);
        }

        private void CacheOriginalConnectionState()
        {
            _connectionAlreadyOpen = _sqlConnection.State == ConnectionState.Open;
        }

        private void CleanUpConnectionIfOwned()
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

        private void CreateCommand()
        {
            DisposeCommand();

            //IDbCommandCreator creator = CreateCommandCreator();

            //bool hasCommandTimeoutOverride = _commandTimeoutOverride.HasValue;
            //if (hasCommandTimeoutOverride)
            //{
            //    creator.WithCommandTimeout(_commandTimeoutOverride.Value);
            //}

            //if (HasParameters)
            //{
            //    creator.WithParameters(_procedureParameters);
            //}

            //if (HasTransaction)
            //{
            //    creator.WithTransaction(_transaction);
            //}

            //Command = creator
            //        .BuildCommand()
            //        .Command;
        }

        private void DisposeCommand()
        {
            if (Command == null) return;

            Command.Dispose();
            Command = null;
        }

        private void OpenClosedConnection()
        {
            if (!_connectionAlreadyOpen) _sqlConnection.Open();
        }

        private void RestoreOriginalConnectionState()
        {
            if (!_connectionAlreadyOpen) _sqlConnection.Close();
        }
    }
}
