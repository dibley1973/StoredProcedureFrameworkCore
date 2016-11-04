﻿using System;
using System.Linq;
using System.Reflection;
using Dibware.StoredProcedureFrameworkCore.Generics;
using Dibware.StoredProcedureFrameworkCore.Helpers.AttributeHelpers;
using Dibware.StoredProcedureFrameworkCore.StoredProcedureAttributes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFrameworkCore.UnitTests.Tests.Helpers.AttributeHelpers
{
    [TestClass]
    public class PropertySchemaAttributeFinderTests
    {
        #region Constructor

        [TestMethod]
        public void Constructor_WhenConstructedWithNullProperty_ThrowsException()
        {
            // ARRANGE

            // ACT
            // ReSharper disable once ObjectCreationAsStatement
            Action actual = () => new PropertySchemaAttributeFinder(null);

            // ASSERT
            actual.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void Constructor_WhenGivenValidValue_DoesNotThrowException()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Procedure1");

            // ACT
            // ReSharper disable once ObjectCreationAsStatement
            Action actual = () => new PropertySchemaAttributeFinder(property);

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
            PropertyInfo property = testType.GetProperty("Procedure1");

            // ACT
            bool actual = new PropertySchemaAttributeFinder(property).HasFoundAttribute;

            // ASSERT
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void HasAttribute_WhenCalledAfterInstantiationAndPropertyDoesHaveAtrribute_ReturnsTrue()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Procedure2");

            // ACT
            bool actual = new PropertySchemaAttributeFinder(property).HasFoundAttribute;

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
            PropertyInfo property = testType.GetProperty("Procedure1");

            // ACT
            Maybe<SchemaAttribute> maybeActual = new PropertySchemaAttributeFinder(property).GetResult();
            var actual = maybeActual.FirstOrDefault();

            // ASSERT
            actual.Should().BeNull();
        }

        [TestMethod]
        public void GetResult_WhenCalledAfterInstantiationAndPropertyDoesHaveAtrribute_ReturnsMaybePopulatedWithInstanceOfAttribute()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Procedure2");

            // ACT
            Maybe<SchemaAttribute> maybeActual = new PropertySchemaAttributeFinder(property).GetResult();
            var actual = maybeActual.FirstOrDefault();

            // ASSERT
            actual.Should().NotBeNull();
            actual.Should().BeOfType<SchemaAttribute>();
            actual.Value.Should().Be("log");
        }

        #endregion

        #region Mock object

        private class TestObject
        {

            // ReSharper disable once UnusedMember.Local
            public string Procedure1 { get; set; }
            [Schema("log")]
            // ReSharper disable once UnusedMember.Local
            public string Procedure2 { get; set; }
        }

        #endregion
    }
}