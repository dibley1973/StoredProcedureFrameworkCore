using System;
using System.Linq;
using System.Reflection;
using Dibware.StoredProcedureFrameworkCore.Generics;
using Dibware.StoredProcedureFrameworkCore.Helpers.AttributeHelpers;
using Dibware.StoredProcedureFrameworkCore.StoredProcedureAttributes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFrameworkCore.Tests.Tests.Helpers.AttributeHelpers
{
    [TestClass]
    public class PropertyNameAttributeFinderTests
    {
        #region Constructor

        [TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenConstructedWithNullProperty_ThrowsException()
        {
            // ARRANGE

            // ACT
            // ReSharper disable once ObjectCreationAsStatement
            Action action = () => new PropertyNameAttributeFinder(null);

            // ASSERT
            action.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void Constructor_WhenGivenValidValue_DoesNotThrowException()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Name1");

            // ACT
            // ReSharper disable once ObjectCreationAsStatement
            Action action = () => new PropertyNameAttributeFinder(property);

            // ASSERT
            action.ShouldNotThrow<Exception>();
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
            bool actual = new PropertyNameAttributeFinder(property)
                .HasFoundAttribute;

            // ASSERT
            Assert.IsFalse(actual);
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
            Maybe<NameAttribute> actual = new PropertyNameAttributeFinder(property).GetResult();

            // ASSERT
            Assert.IsNull(actual.FirstOrDefault());
        }

        [TestMethod]
        public void GetResult_WhenCalledAfterInstantiationAndPropertyDoesHaveAtrribute_ReturnsMaybePopulatedWithInstanceOfAttribute()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Name2");

            // ACT
            Maybe<NameAttribute> actual = new PropertyNameAttributeFinder(property).GetResult();

            // ASSERT
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual.FirstOrDefault(), typeof(NameAttribute));
            Assert.IsTrue(actual.HasItem);
        }

        #endregion

        #region Fake Object

        private class TestObject
        {
            // ReSharper disable once UnusedMember.Local
            public string Name1 { get; set; }

            // ReSharper disable once UnusedMember.Local
            [Name("Address")]
            public string Name2 { get; set; }
        }

        #endregion
    }
}