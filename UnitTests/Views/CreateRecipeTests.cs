using Microsoft.Extensions.Logging;
using PBC.Shared;
using PBC.Shared.Lazor;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static PBC.Client.Components.CreateRecipe;

namespace UnitTests.Views
{
    public class CreateRecipeTests
    {
        [Fact]
        public async void HandleSubmit_WithValidParameters_ShouldReturnRecipeDTO()
        {
            var lazor = new Lazor();
            var recipeUrlDTO = new RecipeUrlDTO();
            var recipeDTO = new RecipeDTO();
            var logger = new LoggerFactory().CreateLogger<IRecipeUrlDTO>();
            var Http = new HttpClient();

            var actual = await CreateRecipeView.HandleSubmit(lazor, recipeUrlDTO, recipeDTO, logger, Http);

            Assert.Equal(recipeDTO, actual);
        }

        [Fact]
        public void ResetView_WithValidParameters_ShouldResetObjects()
        {
            var lazor = new Lazor
            {
                Loading = true,
                isSuccess = true
            };
            var recipeUrlDTO = new RecipeUrlDTO
            {
                URL = "TestURL"
            };
            var recipeDTO = new RecipeDTO
            {
                Title = "TestTitle",
                Description = "TestDescription",
                URL = "TestURL"
            };
            recipeDTO.Ingredients.Add("TestIngredient");
            recipeDTO.Instructions.Add("TestInstruction");

            CreateRecipeView.ResetView(lazor, recipeUrlDTO, recipeDTO);

            Assert.False(lazor.Loading);
            Assert.False(lazor.isSuccess);
            Assert.Null(recipeUrlDTO.URL);
            Assert.Null(recipeDTO.Title);
            Assert.Null(recipeDTO.Description);
            Assert.Null(recipeDTO.URL);
            Assert.Equal(recipeDTO.Ingredients, new List<string>());
            Assert.Equal(recipeDTO.Instructions, new List<string>());
        }
    }
}
