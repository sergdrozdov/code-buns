using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBuns.Dotnet.Common.Validators.Tests
{
    public class ValidatorsTests
    {
        private readonly IValidators _validators;

        public ValidatorsTests()
        {
            _validators = new Validators(); // Instantiate the implementation of IValidators
        }

        [Fact]
        public void ValidateEmail_ShouldReturnTrue_ForValidEmails()
        {
            // Arrange
            var validEmails = new[]
            {
                "test@example.com",
                "user.name+tag+sorting@example.com",
                "x@example.com",
                "example-indeed@strange-example.com",
                "mailhost!username@example.org",
                "user%example.com@example.org"
            };

            // Act & Assert
            foreach (var email in validEmails)
            {
                Assert.True(_validators.ValidateEmail(email), $"Expected true for email: {email}");
            }
        }

        [Fact]
        public void ValidateEmail_ShouldReturnFalse_ForInvalidEmails()
        {
            // Arrange
            var invalidEmails = new[]
            {
                "plainaddress",
                "@missingusername.com",
                "username@.com",
                "username@com"
            };

            // Act & Assert
            foreach (var email in invalidEmails)
            {
                Assert.False(_validators.ValidateEmail(email), $"Expected false for email: {email}");
            }
        }

        [Fact]
        public void ValidateEmail_ShouldReturnFalse_ForNullOrEmptyStrings()
        {
            // Arrange
            var invalidInputs = new[] { null, "", "   " };

            // Act & Assert
            foreach (var input in invalidInputs)
            {
                Assert.False(_validators.ValidateEmail(input), $"Expected false for input: {input}");
            }
        }

        [Fact]
        public void ValidateEmail_ShouldHandleLargeInputsGracefully()
        {
            // Arrange
            var largeInput = new string('a', 255) + "@example.com";

            // Act
            var result = _validators.ValidateEmail(largeInput);

            // Assert
            Assert.False(result, "Expected false for email exceeding 254 characters.");
        }
    }
}
