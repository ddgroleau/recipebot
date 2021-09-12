using PBC.Shared.Custom_Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Custom_Validation
{
    public class RegisterPasswordValidationTests : IDisposable
    {
        RegisterPassword Validation;
        public RegisterPasswordValidationTests()
        {
            Validation = new RegisterPassword();
        }

        public void Dispose()
        {
            Validation = new RegisterPassword();
        }

        [Fact]
        public void IsValid_WithValidPassword_ShouldReturnTrue()
        {
            var result = Validation.IsValid("Password!1");

            Assert.True(result);
        }
        [Fact]
        public void IsValid_WithoutNumber_ShouldReturnFalse()
        {
            bool passed = false;
            try
            {
                passed = Validation.IsValid("Password!");
            }
            catch(Exception)
            {
                Assert.False(passed);
            }
            Assert.False(passed);
        }
        [Fact]
          public void IsValid_WithoutSpecialChar_ShouldReturnFalse()
        {
            bool passed = false;
            try
            {
                passed = Validation.IsValid("Password1");
            }
            catch(Exception)
            {
                Assert.False(passed);
            }
            Assert.False(passed);
        }
          [Fact]
          public void IsValid_WithoutLowercase_ShouldReturnFalse()
        {
            bool passed = false;
            try
            {
                passed = Validation.IsValid("PASSWORD!1");
            }
            catch(Exception)
            {
                Assert.False(passed);
            }
            Assert.False(passed);
        }
        [Fact]
          public void IsValid_WithoutUppercase_ShouldReturnFalse()
        {
            bool passed = false;
            try
            {
                passed = Validation.IsValid("password!1");
            }
            catch(Exception)
            {
                Assert.False(passed);
            }
            Assert.False(passed);
        }
    }
}
