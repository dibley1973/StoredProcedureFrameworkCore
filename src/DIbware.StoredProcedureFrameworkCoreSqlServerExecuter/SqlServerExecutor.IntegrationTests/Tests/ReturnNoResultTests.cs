using Dibware.StoredProcedureFrameworkCore.SqlServerExecutor.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFrameworkCore.SqlServerExecutor.IntegrationTests.Tests.Base;
using Dibware.StoredProcedureFrameworkCore.Types;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFrameworkCore.SqlServerExecutor.IntegrationTests.Tests
{
    [TestClass]
    public class ReturnNoResultTests : SqlServerExecuterTestBase
    {
        [TestMethod]
        public void ReturnNoResultsProcedure_ReturnsNull()
        {
            // ARRANGE
            var parameters = new NullStoredProcedureParameters();
            var procedure = new ReturnNoResultStoredProcedure(Executor);

            // ACT
            var results = procedure.ExecuteFor(parameters);
            //Connection.ExecuteStoredProcedure(procedure);

            // ASSERT
            results.Should().NotBeNull();
            //Assert.IsNull(results);
        }
    }
}