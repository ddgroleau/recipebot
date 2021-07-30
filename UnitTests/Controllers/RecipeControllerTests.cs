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
    public class RecipeControllerTests : IDisposable
    {
        ILogger<RecipeController> Logger;
        IFactory<IIngredient> IngredientFactory;
        IFactory<IInstruction> InstructionFactory;
        IRecipeDTO RecipeDTO;
        IAllRecipesScraper Scraper;
        IRecipeUrlDTO RecipeUrlDTO;
        RecipeController RecipeController;
        IRecipeService RecipeService;
        IRecipeBuilder RecipeBuilder;
        IRecipeModel RecipeModel;

        public RecipeControllerTests()
        {
            RecipeModel = new RecipeModel();
            InstructionFactory = new InstructionFactory();
            IngredientFactory = new IngredientFactory();
            Logger = new LoggerFactory().CreateLogger<RecipeController>();
            RecipeDTO = new RecipeDTO();
            RecipeBuilder = new RecipeBuilder(RecipeModel, InstructionFactory, IngredientFactory);
            Scraper = new AllRecipesScraper();
            RecipeUrlDTO = new RecipeUrlDTO();
            RecipeService = new RecipeService(RecipeBuilder);
            RecipeController = new RecipeController(Logger, RecipeDTO, Scraper, RecipeService);
        }

        public void Dispose()
        {
            RecipeModel = new RecipeModel();
            InstructionFactory = new InstructionFactory();
            IngredientFactory = new IngredientFactory();
            Logger = new LoggerFactory().CreateLogger<RecipeController>();
            RecipeDTO = new RecipeDTO();
            RecipeBuilder = new RecipeBuilder(RecipeModel, InstructionFactory, IngredientFactory);
            Scraper = new AllRecipesScraper();
            RecipeUrlDTO = new RecipeUrlDTO();
            RecipeService = new RecipeService(RecipeBuilder);
            RecipeController = new RecipeController(Logger, RecipeDTO, Scraper, RecipeService);
        }

        [Fact]
        public void PostRecipeURL_WithValidURL_ShouldReturnIRecipeUrlDTO()
        {
            var postResult = RecipeController.PostRecipeUrl((RecipeUrlDTO)RecipeUrlDTO);
            Assert.IsAssignableFrom<IRecipeDTO>(postResult);
        }

        [Fact]
        public void PostRecipe_WithValidURL_ShouldReturn200()
        {
            var postResult = RecipeController.PostRecipe((RecipeDTO)RecipeDTO);
            Assert.IsType<OkObjectResult>(postResult);
        }

        [Fact]
        public void GetAllRecipes_WithNoParameters_ShouldReturnRecipes()
        {
            var retrievedRecipes = RecipeController.GetAllRecipes();

            Assert.True(retrievedRecipes.Any());
        }

        [Fact]
        public void GetUserRecipes_WithValidUserName_ShouldReturnRecipes()
        {
            var retrievedRecipes = RecipeController.GetUserRecipes("UserName");

            Assert.True(retrievedRecipes.Any());
        }

        [Fact]
        public void DeleteRecipe_WithValidRecipe_ShouldBeDeleted()
        {
            var result = RecipeController.DeleteRecipe(RecipeDTO);

            Assert.IsType<OkResult>(result);
        }

    }
}
