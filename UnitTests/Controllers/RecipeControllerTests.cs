using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PBC.Server.Controllers;
using PBC.Shared;
using PBC.Shared.RecipeComponent;
using PBC.Shared.WebScraper;
using System;
using System.Linq;
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
        IRecipeEntity RecipeEntity;
        IRepository<IRecipeEntity> RecipeRepository;
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
            RecipeEntity = new RecipeEntity();
            RecipeRepository = new RecipeRepository();
            RecipeService = new RecipeService(RecipeBuilder, RecipeRepository);
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
            RecipeEntity = new RecipeEntity();
            RecipeRepository = new RecipeRepository();
            RecipeService = new RecipeService(RecipeBuilder, RecipeRepository);
            RecipeController = new RecipeController(Logger, RecipeDTO, Scraper, RecipeService);
        }

        [Fact]
        public void ProcessRecipeUrl_WithValidURL_ShouldReturnIRecipeUrlDTO()
        {
            var postResult = RecipeController.ProcessRecipeUrl((RecipeUrlDTO)RecipeUrlDTO);
            Assert.IsAssignableFrom<IRecipeDTO>(postResult);
        }

        [Fact]
        public void CreateOrUpdateRecipe_WithValidRecipeDTO_ShouldReturn200()
        {
            RecipeDTO.URL = "https://www.allrecipes.com/recipe/264739/lemon-garlic-chicken-kebabs/";
            RecipeDTO.Title = "test";
            RecipeDTO.RecipeType = "BreakFast";
            RecipeDTO.Ingredients.Add("test");
            RecipeDTO.Instructions.Add("test");

            var postResult = RecipeController.CreateOrUpdateRecipe((RecipeDTO)RecipeDTO);

            Assert.IsType<OkResult>(postResult);
        }
        [Fact]
        public void CreateOrUpdateRecipe_WithInvalidRecipeDTO_ShouldReturn400()
        {
            var postResult = RecipeController.CreateOrUpdateRecipe((RecipeDTO)RecipeDTO);

            Assert.IsType<BadRequestResult>(postResult);
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
