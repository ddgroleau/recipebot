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

    }
}
