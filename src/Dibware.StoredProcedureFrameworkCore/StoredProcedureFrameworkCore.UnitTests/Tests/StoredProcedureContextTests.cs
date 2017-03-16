using Dibware.StoredProcedureFrameworkCore.Contracts;
using Dibware.StoredProcedureFrameworkCore.UnitTests.Fakes;
using FluentAssertions;
using NUnit.Framework;

namespace Dibware.StoredProcedureFrameworkCore.UnitTests.Tests
{
    [TestFixture]
    public class StoredProcedureContextTests
    {
        private static readonly IStoredProcedureExecutor Executor;

        static StoredProcedureContextTests()
        {
            Executor = new FakeStoredProcedureExecutor();
        }

        [Test]
        public void ProcedureProperty_AfterInstantiation_IsNotNull()
        {
            // ARRANGE
            var context = new FakeStoredProcedureContext(Executor);

            // ACT
            var actual = context.Procedure1;

            // ASSERT
            actual.Should().NotBeNull();
        }

        [Test]
        public void ProcedurePropertyWithoutNameAttribute_AfterInstantiation_HasCorrectName()
        {
            // ARRANGE
            var context = new FakeStoredProcedureContext(Executor);

            // ACT
            var actual = context.Procedure1;

            // ASSERT
            actual.ProcedureName.Should().Be("Procedure1");
        }

        [Test]
        public void ProcedurePropertyWithNameAttribute_AfterInstantiation_HasCorrectName()
        {
            // ARRANGE
            var context = new FakeStoredProcedureContext(Executor);

            // ACT
            var actual = context.Procedure2;

            // ASSERT
            actual.ProcedureName.Should().Be("ProcedureX");
        }

        [Test]
        public void ProcedurePropertyWithoutSchemaAttribute_AfterInstantiation_HasDefaultSchema()
        {
            // ARRANGE
            var context = new FakeStoredProcedureContext(Executor);

            // ACT
            var actual = context.Procedure1;

            // ASSERT
            actual.SchemaName.Should().Be("dbo");
        }

        [Test]
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