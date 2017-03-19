using System;
using System.Data;
using Moq;
using Dibware.StoredProcedureFrameworkCore.SqlServerExecutor.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace Dibware.StoredProcedureFrameworkCore.SqlServerExecutor.UnitTests.Tests
{
    [TestFixture]
    public class DateReaderRecordToObjectMapperTests
    {
        #region Constructors

        [Test]
        public void Construct_WhenConstructedWithNullDataReader_ThrowsExecption()
        {
            // ARRANGE
            var expectedType = typeof(TestObject);

            // ACT
            // ReSharper disable once ObjectCreationAsStatement
            Action actual = () => new DateReaderRecordToObjectMapper(null, expectedType);

            // ASSERT
            actual.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Construct_WhenConstructedWithNullTargetType_ThrowsExecption()
        {
            // ARRANGE
            var dataReaderMock = new Mock<IDataReader>();
            var expectedReader = dataReaderMock.Object;

            // ACT
            // ReSharper disable once ObjectCreationAsStatement
            Action actual = () => new DateReaderRecordToObjectMapper(expectedReader, null);

            // ASSERT
            actual.ShouldThrow<ArgumentNullException>();
        }

        #endregion

        #region PopulateMappedTargetFromReaderRecord

        [Test]
        public void PopulateMappedTargetFromReaderRecord_WithObjectThatHasADefaultConstructor_ShouldNotThrowMissingException()
        {
            // ARRANGE
            var dataReaderMock = new Mock<IDataReader>();
            var expectedReader = dataReaderMock.Object;
            var expectedType = typeof(TestObject);

            // ACT
            // ReSharper disable once ObjectCreationAsStatement
            Action actual = () => new DateReaderRecordToObjectMapper(expectedReader, expectedType);

            // ASSERT
            actual.ShouldNotThrow<MissingMemberException>();
        }

        [Test]
        public void PopulateMappedTargetFromReaderRecord_WithObjectThatHasNoDefaultConstructor_ThrowsException()
        {
            // ARRANGE
            var dataReaderMock = new Mock<IDataReader>();
            var expectedReader = dataReaderMock.Object;
            var expectedType = typeof(TestObjectWithoutConstructor);

            // ACT
            // ReSharper disable once ObjectCreationAsStatement
            Action actual = () => new DateReaderRecordToObjectMapper(expectedReader, expectedType);

            // ASSERT
            actual.ShouldThrow<MissingMethodException>();
        }

        #endregion

        internal class TestObject
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        internal class TestObjectWithoutConstructor
        {
            public TestObjectWithoutConstructor(int id)
            {
                Id = id;
            }

            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            private int Id { get; set; }
            public string Name { get; set; }
        }
    }
}