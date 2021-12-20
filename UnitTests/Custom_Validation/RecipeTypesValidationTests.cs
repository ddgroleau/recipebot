using Recipebot.Shared.Custom_Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Custom_Validation
{
    public class RecipeTypesValidationTests : IDisposable
    {
        MustBeValidType Validation;
        public RecipeTypesValidationTests()
        {
            Validation = new MustBeValidType();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        [Fact]
        public void IsValid_WithValidObject_ShouldReturnTrue()
        {
            var result = Validation.IsValid("Breakfast");

            Assert.True(result);
        }
        [Fact]
        public void IsValid_WithInvalidObject_ShouldReturnFalse()
        {
            bool passed = false;
            try
            {
                passed = Validation.IsValid("Supper");
            }
            catch(Exception)
            {
                Assert.False(passed);
            }
            Assert.False(passed);
        }
    }
}
