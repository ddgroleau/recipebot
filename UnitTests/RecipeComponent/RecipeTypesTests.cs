using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.RecipeComponent
{
    public class RecipeTypesTests
    {
        [Fact]
        public void GetRecipeType_WithValidType_ShouldReturnType()
        {
            string type = "Breakfast";

            var recipeTypes = new RecipeTypes();

            Assert.Equal(type, recipeTypes.GetRecipeType(type));
        }

        [Fact]
        public void GetRecipeType_WithInvalidType_ShouldThrowException()
        {
            string type = "Supper";

            var recipeTypes = new RecipeTypes();

            Assert.Throws<KeyNotFoundException>(() => recipeTypes.GetRecipeType(type));
        }
    }
}
