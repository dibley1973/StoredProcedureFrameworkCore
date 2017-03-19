using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Dibware.StoredProcedureFrameworkCore.SqlServerExecutor.IntegrationTests.Tests.Base
{
    [SetUpFixture]
    public class SqlServerExecuterTestBase
    {
        private string _connectionString;
        private SqlConnection _connection;
        private SqlTransaction _transaction;
        //private TransactionScope _transaction; // Will have to wait until DotNetCore supports TransactionScope objects
        private SqlServerStoredProcedureExecutor _executor;
        private IConfigurationRoot _config;

        protected SqlConnection Connection => _connection;
        protected SqlServerStoredProcedureExecutor Executor => _executor;

        [SetUp]
        public void TestSetup()
        {
            BuildConfig();

            _connectionString = _config.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
            _executor = new SqlServerStoredProcedureExecutor(_connection);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        private void BuildConfig()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            _config = builder.Build();
        }

        [TearDown]
        public void TestCleanup()
        {
            CleanupTransaction();
            CleanupConnection();
        }

        private void CleanupConnection()
        {
            if (_connection == null) return;

            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
            _connection.Dispose();
        }

        private void CleanupTransaction()
        {
            if (_transaction == null) return;

            _transaction.Rollback();
            _transaction.Dispose();
        }
    }
}
