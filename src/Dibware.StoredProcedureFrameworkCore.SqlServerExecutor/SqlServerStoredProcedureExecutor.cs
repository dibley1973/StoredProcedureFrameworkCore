using System;
using System.Data;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFrameworkCore.SqlServerExecutor
{
    public class SqlServerStoredProcedureExecutor : IStoredProcedureExecutor
    {
        private bool _disposed;
        private SqlConnection _sqlConnection;
        private readonly bool _ownsConnection;

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
            //return ExecuteStoredProcedureFor<TResultSetType, NullStoredProcedureParameters>(storedProcedure);
            throw new NotImplementedException();
        }

        public TResultSetType ExecuteStoredProcedureFor<TResultSetType, TParameterType>(StoredProcedure<TResultSetType, TParameterType> storedProcedure,
            TParameterType parameters)
        {
            if (storedProcedure == null) throw new ArgumentNullException(nameof(storedProcedure));

            string procedureFullName = storedProcedure.GetTwoPartName();

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
            if (_disposed) return;

            if (disposing)
            {
                // Free other state (managed objects).
            }
            // Free your own state (unmanaged objects).
            // Set large fields to null.
            CleanUpConnectionIfOwned();

            _disposed = true;
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
    }
}
