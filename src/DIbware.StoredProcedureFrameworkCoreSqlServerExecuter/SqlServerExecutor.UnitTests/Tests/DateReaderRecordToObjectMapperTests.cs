using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Dibware.StoredProcedureFrameworkCore.SqlServerExecutor.Helpers;
using FluentAssertions;

namespace Dibware.StoredProcedureFrameworkCore.SqlServerExecutor.UnitTests.Tests
{
    [TestClass]
    public class DateReaderRecordToObjectMapperTests
    {
        #region Constructors

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        public void PopulateMappedTargetFromReaderRecord_WithObjectThatHasADefaultConstructor_ShouldNotThrowMissingException()
        {
            // ARRANGE
            var dataReaderMock = new Mock<IDataReader>();
            var expectedReader = dataReaderMock.Object;
            var expectedType = typeof(TestObject); ;
            var mapper = new DateReaderRecordToObjectMapper(expectedReader, expectedType);

            // ACT
            Action actual = () => mapper.PopulateMappedTargetFromReaderRecord();

            // ASSERT
            actual.ShouldNotThrow<MissingMemberException>();
        }

        [TestMethod]
        public void PopulateMappedTargetFromReaderRecord_WithObjectThatHasNoDefaultConstructor_ThrowsException()
        {
            // ARRANGE
            var dataReaderMock = new Mock<IDataReader>();
            var expectedReader = dataReaderMock.Object;
            var expectedType = typeof(TestObjectWithoutConstructor);
            var mapper = new DateReaderRecordToObjectMapper(expectedReader, expectedType);

            // ACT
            Action actual = () => mapper.PopulateMappedTargetFromReaderRecord();

            // ASSERT
            actual.ShouldThrow<MissingMemberException>();
        }

        #endregion

        internal class TestObject
        {
            public TestObject()
            {
                
            }

            public int Id { get; set; }
            public string Name { get; set; }
        }

        internal class TestObjectWithoutConstructor
        {
            public TestObjectWithoutConstructor(int id)
            {
                Id = id;
            }

            private int Id { get; set; }
            public string Name { get; set; }
        }
    }
}