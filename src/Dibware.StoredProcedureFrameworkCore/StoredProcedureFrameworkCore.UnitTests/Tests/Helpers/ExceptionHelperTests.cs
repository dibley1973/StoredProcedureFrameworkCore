using System;
using NUnit.Framework;
using Dibware.StoredProcedureFrameworkCore.Helpers;

namespace Dibware.StoredProcedureFrameworkCore.UnitTests.Tests.Helpers
{
    [TestFixture]
    public class ExceptionHelperTests
    {
        [Test]
        public void CreateStoredProcedureConstructionException_ConstructedWithMessage_ReturnsCorrectMessage()
        {
            // ARRANGE
            const string expectedMessage = "Test Message";

            // ACT
            var exception = ExceptionHelper.CreateStoredProcedureConstructionException(expectedMessage);
            var actualMessage = exception.Message;

            // ASSERT
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [Test]
        public void
            CreateStoredProcedureConstructionException_ConstructedWithMessageAndInnerException_ReturnsBothCorrectly()
        {
            // ARRANGE
            const string expectedMessage = "Test Message";
            var expectedInnerException = new ArgumentNullException();
            //var expectedInnerExceptinType = innerException.GetType() typeof(ArgumentNullException);

            // ACT
            var exception = ExceptionHelper.CreateStoredProcedureConstructionException(expectedMessage,
                expectedInnerException);
            var actualMessage = exception.Message;

            // ASSERT
            Assert.AreEqual(expectedMessage, actualMessage);
            Assert.AreEqual(exception.InnerException, expectedInnerException);
        }

        [Test]
        public void CreateSqlFunctionConstructionException_ConstructedWithMessage_ReturnsCorrectMessage()
        {
            // ARRANGE
            const string expectedMessage = "Test Message";

            // ACT
            var exception = ExceptionHelper.CreateSqlFunctionConstructionException(expectedMessage);
            var actualMessage = exception.Message;

            // ASSERT
            Assert.AreEqual(expectedMessage, actualMessage);
        }

    }
}