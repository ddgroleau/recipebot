using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PBC.Server.Controllers;
using PBC.Shared;
using PBC.Shared.RecipeComponent;
using PBC.Shared.WebScraper;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.Controllers
{
    public class RecipeControllerTests : IDisposable
    {
        ILogger<RecipeController> Logger;
        IRecipeDTO RecipeDTO;
        IAllRecipesScraper Scraper;
        IRecipeUrlDTO RecipeUrlDTO;
        RecipeController RecipeController;
        IRepository<Recipe> RecipeRepository;
        IRecipeService RecipeService;
        IRecipeBuilder RecipeBuilder;
        IRecipeServiceDTO RecipeServiceDTO;

        public RecipeControllerTests()
        {
            RecipeServiceDTO = new RecipeServiceDTO();
            Logger = new LoggerFactory().CreateLogger<RecipeController>();
            RecipeDTO = new RecipeDTO();
            RecipeBuilder = new RecipeBuilder(RecipeServiceDTO);
            Scraper = new AllRecipesScraper();
            RecipeUrlDTO = new RecipeUrlDTO();
            RecipeRepository = new RecipeRepository();
            RecipeService = new RecipeService(RecipeBuilder, RecipeRepository);
            RecipeController = new RecipeController(Logger, RecipeDTO, Scraper, RecipeService);
        }

        public void Dispose()
        {
            RecipeServiceDTO = new RecipeServiceDTO();
            Logger = new LoggerFactory().CreateLogger<RecipeController>();
            RecipeDTO = new RecipeDTO();
            RecipeBuilder = new RecipeBuilder(RecipeServiceDTO);
            Scraper = new AllRecipesScraper();
            RecipeUrlDTO = new RecipeUrlDTO();
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
            var recipeDTO = RecipeDTO;

            recipeDTO.URL = "https://www.allrecipes.com/recipe/264739/lemon-garlic-chicken-kebabs/";
            recipeDTO.Title = "test";
            recipeDTO.RecipeType = "Breakfast";
            recipeDTO.Ingredients.Add("test");
            recipeDTO.Instructions.Add("test");

            var postResult = RecipeController.CreateOrUpdateRecipe((RecipeDTO)recipeDTO);

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

        [Fact]
        public void GetRecipesByTitle_WithValidSearchParameter_ShouldReturnRecipes()
        {
            string searchText = "Test";

            var results = RecipeController.SearchRecipes(searchText);

            Assert.IsAssignableFrom<List<RecipeDTO>>(results);
        }
    }
}
