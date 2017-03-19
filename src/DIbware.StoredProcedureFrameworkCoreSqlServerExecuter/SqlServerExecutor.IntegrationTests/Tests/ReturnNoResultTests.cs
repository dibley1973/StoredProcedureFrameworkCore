using Dibware.StoredProcedureFrameworkCore.SqlServerExecutor.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFrameworkCore.SqlServerExecutor.IntegrationTests.Tests.Base;
using Dibware.StoredProcedureFrameworkCore.Types;
using FluentAssertions;
using NUnit.Framework;

namespace Dibware.StoredProcedureFrameworkCore.SqlServerExecutor.IntegrationTests.Tests
{
    [TestFixture]
    public class ReturnNoResultTests : SqlServerExecuterTestBase
    {
        [Test]
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