using Dibware.StoredProcedureFrameworkCore.Tests.Fakes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFrameworkCore.Tests.Tests
{
    [TestClass]
    public class StoredProcedureContextTests
    {
        private static readonly IStoredProcedureExecutor Executor;

        static StoredProcedureContextTests()
        {
            Executor = new FakeStoredProcedureExecutor();
        }

        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void ProcedureProperty_AfterInstantiation_IsNotNull()
        {
            // ARRANGE
            var context = new FakeStoredProcedureContext(Executor);

            // ACT
            var actual = context.Procedure1;

            // ASSERT
            actual.Should().NotBeNull();
        }

        [TestMethod]
        public void ProcedurePropertyWithoutNameAttribute_AfterInstantiation_HasCorrectName()
        {
            // ARRANGE
            var context = new FakeStoredProcedureContext(Executor);

            // ACT
            var actual = context.Procedure1;

            // ASSERT
            actual.ProcedureName.Should().Be("Procedure1");
        }

        [TestMethod]
        public void ProcedurePropertyWithNameAttribute_AfterInstantiation_HasCorrectName()
        {
            // ARRANGE
            var context = new FakeStoredProcedureContext(Executor);

            // ACT
            var actual = context.Procedure2;

            // ASSERT
            actual.ProcedureName.Should().Be("ProcedureX");
        }

        [TestMethod]
        public void ProcedurePropertyWithoutSchemaAttribute_AfterInstantiation_HasDefaultSchema()
        {
            // ARRANGE
            var context = new FakeStoredProcedureContext(Executor);

            // ACT
            var actual = context.Procedure1;

            // ASSERT
            actual.SchemaName.Should().Be("dbo");
        }

        [TestMethod]
        public void ProcedurePropertyWithSchemaAttribute_AfterInstantiation_HasCorrectSchema()
        {
            // ARRANGE
            var context = new FakeStoredProcedureContext(Executor);

            // ACT
            var actual = context.Procedure3;

            // ASSERT
            actual.SchemaName.Should().Be("log");
        }
    }
}