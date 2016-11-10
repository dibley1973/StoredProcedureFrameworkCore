﻿using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFrameworkCore.SqlServerExecutor.IntegrationTests.Tests.Base
{
    public class SqlServerExecuterTestBase
    {
        private string _connectionString;
        private SqlConnection _connection;
        private SqlTransaction _transaction;
        //private TransactionScope _transaction; // Will have to wait until DotNetCore supports TransactionScope objects
        private SqlServerStoredProcedureExecutor _executor;

        protected SqlConnection Connection => _connection;
        protected SqlServerStoredProcedureExecutor Executor => _executor;

        [TestInitialize]
        public void TestSetup()
        {

            //var configurationBuilder = new ConfigurationBuilder()
            //    .AddJsonFile("config.json")
            //    .AddEnvironmentVariables();
            //var config = configurationBuilder.Build();

            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            builder.AddInMemoryCollection();
            builder.AddEnvironmentVariables();
            var config = builder.Build();

            //var folderSettings = ConfigurationBinder.Bind<ConnectionStrings>(config.GetSection("ConnectionStrings"));
            //var path = folderSettings.D["TestFolder1"];


            _connectionString = config.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
            _executor = new SqlServerStoredProcedureExecutor(_connection);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            CleanupConnection();
            CleanupTransaction();
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
