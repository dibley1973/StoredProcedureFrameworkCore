using System.Data.SqlClient;

namespace Dibware.StoredProcedureFrameworkCore.SqlServerExecutor.IntegrationTests
{
    public class SqlServerExecuterTestBase
    {
        private string _connectionString;
        private SqlConnection _connection;
        //private TransactionScope _transaction;

        protected SqlConnection Connection => _connection;

        public SqlServerExecuterTestBase()
        {

        }
    }
}
