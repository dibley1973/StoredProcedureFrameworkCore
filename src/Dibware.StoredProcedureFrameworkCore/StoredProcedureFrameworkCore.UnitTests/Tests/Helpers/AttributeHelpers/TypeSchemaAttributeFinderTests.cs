using System;
using System.Linq;
using Dibware.StoredProcedureFrameworkCore.Generics;
using Dibware.StoredProcedureFrameworkCore.Helpers.AttributeHelpers;
using Dibware.StoredProcedureFrameworkCore.StoredProcedureAttributes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFrameworkCore.UnitTests.Tests.Helpers.AttributeHelpers
{
    [TestClass]
    public class TypeSchemaAttributeFinderTests
    {
        #region Constructor

        [TestMethod]
        public void Constructor_WhenConstructedWithNullProperty_ThrowsException()
        {
            // ARRANGE

            // ACT
            // ReSharper disable once ObjectCreationAsStatement
            Action actual = () => new TypeSchemaAttributeFinder(null);

            // ASSERT
            actual.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void Constructor_WhenGivenValidValue_DoesNotThrowException()
        {
            // ARRANGE
            Type testType = typeof(TestObject1);

            // ACT
            // ReSharper disable once ObjectCreationAsStatement
            Action actual = () => new TypeSchemaAttributeFinder(testType);

            // ASSERT
            actual.ShouldNotThrow<ArgumentNullException>();
        }

        #endregion

        #region HasFoundAttribute

        [TestMethod]
        public void HasAttribute_WhenCalledAfterInstantiationAndPropertyDoesNotHaveAtrribute_ReturnsFalse()
        {
            // ARRANGE
            Type testType = typeof(TestObject1);

            // ACT
            bool actual = new TypeSchemaAttributeFinder(testType).HasFoundAttribute;

            // ASSERT
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void HasAttribute_WhenCalledAfterInstantiationAndPropertyDoesHaveAtrribute_ReturnsTrue()
        {
            // ARRANGE
            Type testType = typeof(TestObject2);

            // ACT
            bool actual = new TypeSchemaAttributeFinder(testType).HasFoundAttribute;

            // ASSERT
            actual.Should().BeTrue();
        }

        #endregion

        #region GetResult

        [TestMethod]
        public void GetResult_WhenCalledAfterInstantiationAndPropertyDoesNotHaveAtrribute_ReturnsEmptyMaybe()
        {
            // ARRANGE
            Type testType = typeof(TestObject1);

            // ACT
            Maybe<SchemaAttribute> maybeActual = new TypeSchemaAttributeFinder(testType).GetResult();

            // ASSERT
            var actual = maybeActual.FirstOrDefault();
            actual.Should().BeNull();
        }

        [TestMethod]
        public void GetResult_WhenCalledAfterInstantiationAndPropertyDoesHaveAtrribute_ReturnsMaybePopulatedWithInstanceOfAttribute()
        {
            // ARRANGE
            Type testType = typeof(TestObject2);

            // ACT
            Maybe<SchemaAttribute> maybeActual = new TypeSchemaAttributeFinder(testType).GetResult();


            // ASSERT
            var actual = maybeActual.FirstOrDefault();
            actual.Should().NotBeNull();
            actual.Should().BeOfType<SchemaAttribute>();
            actual.Value.Should().Be("log");
        }

        #endregion

        #region Mock object

        private class TestObject1
        {
        }

        [Schema("log")]
        private class TestObject2
        {
        }

        #endregion
    }
}