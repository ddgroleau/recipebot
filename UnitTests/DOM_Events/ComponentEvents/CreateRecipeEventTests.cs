using Microsoft.Extensions.Logging;
using PBC.Shared;
using PBC.Shared.DOM_Events.ComponentEvents;
using PBC.Shared.Lazor;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace UnitTests.DOM_Events.ComponentEvents
{
    public class CreateRecipeEventTests
    {
        [Fact]
        public async void HandleSubmit_WithValidParameters_ShouldReturnRecipeDTO()
        {
            var lazor = new Lazor();
            var recipeUrlDTO = new RecipeUrlDTO();
            var recipeDTO = new RecipeDTO();
            var logger = new LoggerFactory().CreateLogger<IRecipeUrlDTO>();
            var Http = new HttpClient();
            var createRecipe = new CreateRecipeEvent(lazor, recipeUrlDTO, recipeDTO, logger, Http);

            var actual = await createRecipe.HandleSubmit();

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

            var logger = new LoggerFactory().CreateLogger<IRecipeUrlDTO>();
            var Http = new HttpClient();
            var createRecipe = new CreateRecipeEvent(lazor, recipeUrlDTO, recipeDTO, logger, Http);

            recipeDTO.Ingredients.Add("TestIngredient");
            recipeDTO.Instructions.Add("TestInstruction");

            createRecipe.ResetView();

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
