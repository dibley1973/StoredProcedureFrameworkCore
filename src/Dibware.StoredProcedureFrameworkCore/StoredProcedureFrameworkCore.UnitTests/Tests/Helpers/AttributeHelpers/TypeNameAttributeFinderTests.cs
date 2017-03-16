using System;
using System.Linq;
using Dibware.StoredProcedureFrameworkCore.Generics;
using Dibware.StoredProcedureFrameworkCore.Helpers.AttributeHelpers;
using Dibware.StoredProcedureFrameworkCore.StoredProcedureAttributes;
using FluentAssertions;
using NUnit.Framework;

namespace Dibware.StoredProcedureFrameworkCore.UnitTests.Tests.Helpers.AttributeHelpers
{
    [TestFixture]
    public class TypeNameAttributeFinderTests
    {
        #region Constructor

        [Test]
        public void Constructor_WhenConstructedWithNullProperty_ThrowsException()
        {
            // ARRANGE

            // ACT
            // ReSharper disable once ObjectCreationAsStatement
            Action actual = () => new TypeNameAttributeFinder(null);

            // ASSERT
            actual.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_WhenGivenValidValue_DoesNotThrowException()
        {
            // ARRANGE
            Type testType = typeof(TestObject1);

            // ACT
            // ReSharper disable once ObjectCreationAsStatement
            Action actual = () => new TypeNameAttributeFinder(testType);

            // ASSERT
            actual.ShouldNotThrow<ArgumentNullException>();
        }

        #endregion

        #region HasFoundAttribute

        [Test]
        public void HasAttribute_WhenCalledAfterInstantiationAndPropertyDoesNotHaveAtrribute_ReturnsFalse()
        {
            // ARRANGE
            Type testType = typeof(TestObject1);

            // ACT
            bool actual = new TypeNameAttributeFinder(testType).HasFoundAttribute;

            // ASSERT
            actual.Should().BeFalse();
        }

        [Test]
        public void HasAttribute_WhenCalledAfterInstantiationAndPropertyDoesHaveAtrribute_ReturnsTrue()
        {
            // ARRANGE
            Type testType = typeof(TestObject2);

            // ACT
            bool actual = new TypeNameAttributeFinder(testType).HasFoundAttribute;

            // ASSERT
            actual.Should().BeTrue();
        }

        #endregion

        #region GetResult

        [Test]
        public void GetResult_WhenCalledAfterInstantiationAndPropertyDoesNotHaveAtrribute_ReturnsEmptyMaybe()
        {
            // ARRANGE
            Type testType = typeof(TestObject1);

            // ACT
            Maybe<NameAttribute> maybeActual = new TypeNameAttributeFinder(testType).GetResult();
            
            // ASSERT
            var actual = maybeActual.FirstOrDefault();
            actual.Should().BeNull();
        }

        [Test]
        public void GetResult_WhenCalledAfterInstantiationAndPropertyDoesHaveAtrribute_ReturnsMaybePopulatedWithInstanceOfAttribute()
        {
            // ARRANGE
            Type testType = typeof(TestObject2);

            // ACT
            Maybe<NameAttribute> maybeActual = new TypeNameAttributeFinder(testType).GetResult();


            // ASSERT
            var actual = maybeActual.FirstOrDefault();
            actual.Should().NotBeNull();
            actual.Should().BeOfType<NameAttribute>();
            actual.Value.Should().Be("Address");
        }

        #endregion

        #region Mock object

        private class TestObject1
        {
        }

        [Name("Address")]
        private class TestObject2
        {
        }

        #endregion
    }
}