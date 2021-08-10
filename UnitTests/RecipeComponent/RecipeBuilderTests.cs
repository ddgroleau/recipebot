using PBC.Shared;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.RecipeComponent
{
    public class RecipeBuilderTests : IDisposable
    {
        IRecipeServiceDTO RecipeServiceDTO;
        IRecipeBuilder RecipeBuilder;
        IRecipeDTO RecipeDTO;

        public RecipeBuilderTests()
        {
            RecipeDTO = new RecipeDTO();
            RecipeServiceDTO = new RecipeServiceDTO();
            RecipeBuilder = new RecipeBuilder(RecipeServiceDTO, RecipeDTO);
        }

        public void Dispose()
        {
            RecipeDTO = new RecipeDTO();
            RecipeServiceDTO = new RecipeServiceDTO();
            RecipeBuilder = new RecipeBuilder(RecipeServiceDTO, RecipeDTO);
        }

        [Fact]
        public void Build_WithValidRecipeDTO_ShouldReturnRecipeSeviceDTO()
        {
            var recipeDTO = RecipeDTO;

            recipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            recipeDTO.Title = "Test";
            recipeDTO.Description = "Test";
            recipeDTO.RecipeType = "Breakfast";
            recipeDTO.Ingredients = new List<string>();
            recipeDTO.Instructions = new List<string>();

            recipeDTO.Instructions.Add("Test");
            recipeDTO.Ingredients.Add("Test");

            var result = RecipeBuilder.Build(recipeDTO);

            Assert.Equal(result.URL, recipeDTO.URL);
            Assert.Equal(result.Title, recipeDTO.Title);
            Assert.Equal(result.Description, recipeDTO.Description);
            Assert.Equal(result.Ingredients, result.Ingredients);
            Assert.Equal(result.Instructions, result.Instructions);
        }

        [Fact]
        public void Build_WithValidRecipeServiceDTO_ShouldReturnRecipeDTO()
        {
            var recipeDTO = RecipeServiceDTO;

            recipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            recipeDTO.Title = "Test";
            recipeDTO.Description = "Test";
            recipeDTO.RecipeType = "Breakfast";
            recipeDTO.Ingredients = new List<string>();
            recipeDTO.Instructions = new List<string>();

            recipeDTO.Instructions.Add("Test");
            recipeDTO.Ingredients.Add("Test");

            var result = RecipeBuilder.Build(recipeDTO);

            Assert.Equal(result.URL, recipeDTO.URL);
            Assert.Equal(result.Title, recipeDTO.Title);
            Assert.Equal(result.Description, recipeDTO.Description);
            Assert.Equal(result.Ingredients, result.Ingredients);
            Assert.Equal(result.Instructions, result.Instructions);
            Assert.IsAssignableFrom<IRecipeDTO>(result);
        }
    }
}
