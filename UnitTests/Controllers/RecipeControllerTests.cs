using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PBC.Server.Controllers;
using PBC.Shared;
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
        public void PostRecipeURL_WithValidURL_ShouldReturn200()
        {
            var logger = new LoggerFactory().CreateLogger<RecipeController>();
            var controller = new RecipeController(logger);
            var recipeDTO = new RecipeDTO();

            var postResult = controller.PostRecipeUrl(recipeDTO);
            Assert.IsType<OkObjectResult>(postResult);
        }

        [Fact]
        public void PostNewRecipe_WithValidURL_ShouldReturn200()
        {
            var logger = new LoggerFactory().CreateLogger<RecipeController>();
            var controller = new RecipeController(logger);
            var recipeDTO = new RecipeDTO();

            var postResult = controller.PostNewRecipe(recipeDTO);
            Assert.IsType<OkObjectResult>(postResult);
        }

    }
}
