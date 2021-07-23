using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PBC.Server.Controllers;
using PBC.Shared;
using PBC.Shared.RecipeComponent;
using PBC.Shared.WebScraper;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var recipeControllerLogger = new LoggerFactory().CreateLogger<RecipeController>();
            var recipeUrlDtoLogger = new LoggerFactory().CreateLogger<RecipeUrlDTO>();
            var recipeDtoLogger = new LoggerFactory().CreateLogger<RecipeDTO>();
            var recipeDTO = new RecipeDTO(recipeDtoLogger);
            var allRecipesScraper = new AllRecipesScraper();
            var recipeUrlDTO = new RecipeUrlDTO(recipeUrlDtoLogger);

            var controller = new RecipeController(recipeControllerLogger, recipeDTO, allRecipesScraper);

            var postResult = controller.PostRecipeUrl(recipeUrlDTO);
            Assert.IsAssignableFrom<IRecipeDTO>(postResult);
        }

        [Fact]
        public void PostNewRecipe_WithValidURL_ShouldReturn200()
        {
            var logger = new LoggerFactory().CreateLogger<RecipeController>();
            var recipeDtoLogger = new LoggerFactory().CreateLogger<RecipeDTO>();
            var recipeDTO = new RecipeDTO(recipeDtoLogger);
            var allRecipesScraper = new AllRecipesScraper();

            var controller = new RecipeController(logger, recipeDTO, allRecipesScraper);

            var postResult = controller.PostNewRecipe(recipeDTO);
            Assert.IsType<OkObjectResult>(postResult);
        }

    }
}
