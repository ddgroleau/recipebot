using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PBC.Server.Controllers;
using PBC.Shared;
using PBC.Shared.RecipeComponent;
using PBC.Shared.WebScraper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Controllers
{
    public class RecipeControllerTests
    {
        [Fact]
        public void PostRecipeURL_WithValidURL_ShouldReturnIRecipeUrlDTO()
        {
            var logger = new LoggerFactory().CreateLogger<RecipeController>();
            var recipeDTO = new RecipeDTO();
            var allRecipesScraper = new AllRecipesScraper();
            var recipeUrlDTO = new RecipeUrlDTO();

            var controller = new RecipeController(logger, recipeDTO, allRecipesScraper);

            var postResult = controller.PostRecipeUrl(recipeUrlDTO);
            Assert.IsAssignableFrom<IRecipeDTO>(postResult);
        }

        [Fact]
        public void PostRecipe_WithValidURL_ShouldReturn200()
        {
            var logger = new LoggerFactory().CreateLogger<RecipeController>();
            var recipeDTO = new RecipeDTO();
            var allRecipesScraper = new AllRecipesScraper();

            var controller = new RecipeController(logger, recipeDTO, allRecipesScraper);

            var postResult = controller.PostRecipe(recipeDTO);
            Assert.IsType<OkObjectResult>(postResult);
        }

        [Fact]
        public void GetAllRecipes_WithNoParameters_ShouldReturnRecipes()
        {
            var logger = new LoggerFactory().CreateLogger<RecipeController>();
            var recipeDTO = new RecipeDTO();
            var allRecipesScraper = new AllRecipesScraper();
            var controller = new RecipeController(logger, recipeDTO, allRecipesScraper);

            var retrievedRecipes = controller.GetAllRecipes();

            Assert.True(retrievedRecipes.Any());
        }

        [Fact]
        public void GetUserRecipes_WithValidUserName_ShouldReturnRecipes()
        {
            var logger = new LoggerFactory().CreateLogger<RecipeController>();
            var recipeDTO = new RecipeDTO();
            var allRecipesScraper = new AllRecipesScraper();
            var controller = new RecipeController(logger, recipeDTO, allRecipesScraper);

            var retrievedRecipes = controller.GetUserRecipes("UserName");

            Assert.True(retrievedRecipes.Any());
        }

        [Fact]
        public void DeleteRecipe_WithValidRecipe_ShouldBeDeleted()
        {
            var logger = new LoggerFactory().CreateLogger<RecipeController>();
            var recipeDTO = new RecipeDTO();
            var allRecipesScraper = new AllRecipesScraper();
            var controller = new RecipeController(logger, recipeDTO, allRecipesScraper);

            var result = controller.DeleteRecipe(recipeDTO);

            Assert.IsType<OkResult>(result);
        }

    }
}
