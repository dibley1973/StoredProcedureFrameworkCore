using System;
using System.Data;
using System.Data.SqlClient;
using Dibware.StoredProcedureFrameworkCore.Types;

namespace Dibware.StoredProcedureFrameworkCore.SqlServerExecutor
{
    public class SqlServerStoredProcedureExecutor : IStoredProcedureExecutor
    {
        private readonly SqlConnection _sqlConnection;
        private readonly bool _ownsConnection = false;

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
    }
}
