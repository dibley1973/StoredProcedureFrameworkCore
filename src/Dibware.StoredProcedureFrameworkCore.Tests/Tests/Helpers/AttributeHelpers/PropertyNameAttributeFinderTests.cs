using System;
using System.Reflection;
using System.Linq;
using Dibware.StoredProcedureFrameworkCore.Generics;
using Dibware.StoredProcedureFrameworkCore.Helpers.AttributeHelpers;
using Dibware.StoredProcedureFrameworkCore.StoredProcedureAttributes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFrameworkCore.Tests.Tests
{
    [TestClass]
    public class PropertyNameAttributeFinderTests
    {
        #region Constructor

        [TestMethod]
        public void Constructor_WhenConstructedWithNullProperty_ThrowsException()
        {
            // ARRANGE

            // ACT
            // ReSharper disable once ObjectCreationAsStatement
            Action actual = () => new PropertyNameAttributeFinder(null);

            // ASSERT
            actual.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void Constructor_WhenGivenValidValue_DoesNotThrowException()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Name1");

            // ACT
            // ReSharper disable once ObjectCreationAsStatement
            Action actual = () => new PropertyNameAttributeFinder(property);

            // ASSERT
            actual.ShouldNotThrow<ArgumentNullException>();
        }

        #endregion

        #region HasFoundAttribute

        [TestMethod]
        public void HasAttribute_WhenCalledAfterInstantiationAndPropertyDoesNotHaveAtrribute_ReturnsFalse()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Name1");

            // ACT
            var actual = new PropertyNameAttributeFinder(property).HasFoundAttribute;

            // ASSERT
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void HasAttribute_WhenCalledAfterInstantiationAndPropertyDoesHaveAtrribute_ReturnsTrue()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Name2");

            // ACT
            bool actual = new PropertyNameAttributeFinder(property).HasFoundAttribute;

            // ASSERT
            actual.Should().BeTrue();
        }

        #endregion

        #region GetResult

        [TestMethod]
        public void GetResult_WhenCalledAfterInstantiationAndPropertyDoesNotHaveAtrribute_ReturnsEmptyMaybe()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Name1");

            // ACT
            Maybe<NameAttribute> maybeActual = new PropertyNameAttributeFinder(property).GetResult();
            
            // ASSERT
            var actual = maybeActual.FirstOrDefault();
            actual.Should().BeNull();
        }

        [TestMethod]
        public void GetResult_WhenCalledAfterInstantiationAndPropertyDoesHaveAtrribute_ReturnsMaybePopulatedWithInstanceOfAttribute()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Name2");

            // ACT
            Maybe<NameAttribute> maybeActual = new PropertyNameAttributeFinder(property).GetResult();
            
            // ASSERT
            var actual = maybeActual.FirstOrDefault();
            actual.Should().NotBeNull();
            actual.Should().BeOfType<NameAttribute>();
        }

        #endregion

        #region Mock object

        private class TestObject
        {
            // ReSharper disable once UnusedMember.Local
            public string Name1 { get; set; }

            [Name("Address")]
            // ReSharper disable once UnusedMember.Local
            public string Name2 { get; set; }
        }

        #endregion
    }
}