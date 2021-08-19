using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PBC.Server.Controllers;
using PBC.Shared;
using PBC.Shared.RecipeComponent;
using PBC.Shared.WebScraper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Controllers
{
    public class RecipeControllerFixture : IDisposable
    {
        public ILogger<IRecipeMemento> MementoLogger;
        public IRecipeMemento RecipeMemento;
        public ILogger<RecipeController> Logger;
        public IRecipeDTO RecipeDTO;
        public IAllRecipesScraper Scraper;
        public IRecipeUrlDTO RecipeUrlDTO;
        public RecipeController RecipeController;
        public IRecipeRepository RecipeRepository;
        public IRecipeService RecipeService;
        public IRecipeBuilder RecipeBuilder;
        public IRecipeServiceDTO RecipeServiceDTO;

        public RecipeControllerFixture()
        {
            MementoLogger = new LoggerFactory().CreateLogger<IRecipeMemento>();
            RecipeMemento = new RecipeMemento(MementoLogger);
            RecipeServiceDTO = new RecipeServiceDTO();
            Logger = new LoggerFactory().CreateLogger<RecipeController>();
            RecipeDTO = new RecipeDTO();
            RecipeBuilder = new RecipeBuilder(RecipeServiceDTO, RecipeDTO);
            Scraper = new AllRecipesScraper();
            RecipeUrlDTO = new RecipeUrlDTO();
            RecipeRepository = new RecipeRepository();
            RecipeService = new RecipeService(RecipeBuilder, RecipeRepository, RecipeMemento);
            RecipeController = new RecipeController(Logger, RecipeDTO, Scraper, RecipeService);
        }

        public void Dispose()
        {
            MementoLogger = new LoggerFactory().CreateLogger<IRecipeMemento>();
            RecipeMemento = new RecipeMemento(MementoLogger);
            RecipeServiceDTO = new RecipeServiceDTO();
            Logger = new LoggerFactory().CreateLogger<RecipeController>();
            RecipeDTO = new RecipeDTO();
            RecipeBuilder = new RecipeBuilder(RecipeServiceDTO, RecipeDTO);
            Scraper = new AllRecipesScraper();
            RecipeUrlDTO = new RecipeUrlDTO();
            RecipeRepository = new RecipeRepository();
            RecipeService = new RecipeService(RecipeBuilder, RecipeRepository, RecipeMemento);
            RecipeController = new RecipeController(Logger, RecipeDTO, Scraper, RecipeService);
        }
    }

    public class RecipeControllerTests : IClassFixture<RecipeControllerFixture>
    {
        private readonly RecipeControllerFixture Fixture;
        public RecipeControllerTests(RecipeControllerFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public void ProcessRecipeUrl_WithValidURL_ShouldReturnIRecipeUrlDTO()
        {
            var postResult = Fixture.RecipeController.ProcessRecipeUrl((RecipeUrlDTO)Fixture.RecipeUrlDTO);
            Assert.IsAssignableFrom<IRecipeDTO>(postResult);
        }

        [Fact]
        public void CreateOrUpdateRecipe_WithValidRecipeDTO_ShouldReturn200()
        {
            var recipeDTO = Fixture.RecipeDTO;

            recipeDTO.URL = "https://www.allrecipes.com/recipe/264739/lemon-garlic-chicken-kebabs/";
            recipeDTO.Title = "test";
            recipeDTO.RecipeType = "Breakfast";
            recipeDTO.Ingredients.Add("test");
            recipeDTO.Instructions.Add("test");

            var postResult = Fixture.RecipeController.CreateOrUpdateRecipe((RecipeDTO)recipeDTO);

            Assert.IsType<OkResult>(postResult);
        }
        [Fact]
        public void CreateOrUpdateRecipe_WithInvalidRecipeDTO_ShouldReturn400()
        {
            var postResult = Fixture.RecipeController.CreateOrUpdateRecipe((RecipeDTO)Fixture.RecipeDTO);

            Assert.IsType<BadRequestResult>(postResult);
        }

        [Fact]
        public void GetUserRecipes_WithValidUserName_ShouldReturnRecipes()
        {
            var retrievedRecipes = Fixture.RecipeController.GetUserRecipes("UserName");

            Assert.True(retrievedRecipes.Any());
        }

        [Fact]
        public void DeleteRecipe_WithValidRecipe_ShouldBeDeleted()
        {
            var result = Fixture.RecipeController.DeleteRecipe(Fixture.RecipeDTO);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void SearchRecipes_WithValidSearchParameter_ShouldReturnRecipes()
        {
            string searchText = "Test";

            var results = Fixture.RecipeController.SearchRecipes(searchText);

            Assert.IsAssignableFrom<IEnumerable<IRecipeDTO>>(results);
        }
    }
}
