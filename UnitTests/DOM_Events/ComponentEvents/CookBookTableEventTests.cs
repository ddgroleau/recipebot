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
    public class CookBookTableEventTests
    {
        [Fact]
        public async void GetRecipesAsync_WithValidParameters_ShouldBeCorrectType()
        {
            var recipeDTO = new RecipeDTO();
            var retrievedRecipes = new List<IRecipeDTO>();
            var lazor = new Lazor();
            var logger = new LoggerFactory().CreateLogger<IRecipeDTO>();
            var http = new HttpClient();
            var cookbook = new CookbookTableEvent(lazor, recipeDTO, logger, http, retrievedRecipes);

            var recipes = await cookbook.GetRecipesAsync(false, "TestUser");

            Assert.IsAssignableFrom<IEnumerable<IRecipeDTO>>(recipes);
        }

        [Fact]
        public void HandleClick_WithLazorObject_ShouldChangeIsToggled()
        {
            var recipeDTO = new RecipeDTO();
            var retrievedRecipes = new List<IRecipeDTO>();
            var lazor = new Lazor();
            var logger = new LoggerFactory().CreateLogger<IRecipeDTO>();
            var http = new HttpClient();
            var cookbook = new CookbookTableEvent(lazor, recipeDTO, logger, http, retrievedRecipes);

            cookbook.HandleClick();

            Assert.False(cookbook.Lazor.isToggled);

        }

        [Fact]
        public void HandleDelete_WithLazorObject_ShouldChangeIsShown()
        {
            var recipeDTO = new RecipeDTO();
            var retrievedRecipes = new List<IRecipeDTO>();
            var lazor = new Lazor();
            var logger = new LoggerFactory().CreateLogger<IRecipeDTO>();
            var http = new HttpClient();
            var cookbook = new CookbookTableEvent(lazor, recipeDTO, logger, http, retrievedRecipes);

            cookbook.HandleDelete();

            Assert.True(cookbook.Lazor.isShown);
            Assert.True(cookbook.IsDeleteAction);
            Assert.Equal(cookbook.Message, $"Are you sure you want to delete \"{recipeDTO.Title}\"?");
        }

        [Fact]
        public void HandleDetails_WithLazorObject_ShouldChangeIsShown()
        {
            var recipeDTO = new RecipeDTO();
            var retrievedRecipes = new List<IRecipeDTO>();
            var lazor = new Lazor();
            var logger = new LoggerFactory().CreateLogger<IRecipeDTO>();
            var http = new HttpClient();
            var cookbook = new CookbookTableEvent(lazor, recipeDTO, logger, http, retrievedRecipes);

            cookbook.HandleDetails();
            
            Assert.True(cookbook.Lazor.isShown);
            Assert.False(cookbook.IsDeleteAction);
            Assert.Equal(cookbook.Message, $"{recipeDTO.Title}");
        }
    }
}
