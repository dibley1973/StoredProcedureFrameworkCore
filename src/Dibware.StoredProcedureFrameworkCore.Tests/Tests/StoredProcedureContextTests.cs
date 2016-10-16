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

        [TestMethod]
        public void ProcedurePropertyWithoutNameAttribute_AfterInstantiation_HasCorrectName()
        {
            // ARRANGE
            IStoredProcedureExecutor executer = new FakeStoredProcedureExecutor();
            var context = new FakeStoredProcedureContext(executer);

            // ACT
            var actual = context.Procedure1;

            // ASSERT
            actual.ProcedureName.Should().Be("Procedure1");
        }

        [TestMethod]
        public void ProcedurePropertyWithNameAttribute_AfterInstantiation_HasCorrectName()
        {
            // ARRANGE
            IStoredProcedureExecutor executer = new FakeStoredProcedureExecutor();
            var context = new FakeStoredProcedureContext(executer);

            // ACT
            var actual = context.Procedure2;

            // ASSERT
            actual.ProcedureName.Should().Be("ProcedureX");
        }
    }
}