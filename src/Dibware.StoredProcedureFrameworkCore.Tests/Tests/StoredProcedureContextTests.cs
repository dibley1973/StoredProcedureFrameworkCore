using Dibware.StoredProcedureFrameworkCore.Tests.Fakes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFrameworkCore.Tests.Tests
{
    [TestClass]
    public class StoredProcedureContextTests
    {
        [TestMethod]
        public void ProcedureProperty_AfterInstantiation_IsNotNull()
        {
            // ARRANGE
            IStoredProcedureExecutor executer = new FakeStoredProcedureExecutor();
            var context = new FakeStoredProcedureContext(executer);

            // ACT
            var actual = context.Procedure1;

            // ASSERT
            actual.Should().NotBeNull();
        }
    }

    public class FakeStoredProcedureExecutor : IStoredProcedureExecutor
    {
        public TResultSetType ExecuteStoredProcedure<TResultSetType>(StoredProcedure<TResultSetType> procedure)
        {
            throw new System.NotImplementedException();
        }
    }
}