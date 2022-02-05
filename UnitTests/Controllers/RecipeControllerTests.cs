using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Recipebot.Server.Controllers;
using Recipebot.Server.Data;
using Recipebot.Server.Data.Repositories;
using Recipebot.Shared;
using Recipebot.Shared.Common;
using Recipebot.Shared.RecipeComponent;
using Recipebot.Shared.SubscriptionComponent;
using Recipebot.Shared.WebScraper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UnitTests.Data;
using UnitTests.MockObjects;
using Xunit;

namespace UnitTests.Controllers
{
    public class RecipeControllerFixture : IDisposable
    {
        public IFactory<Ingredient> IngredientFactory;
        public IFactory<Instruction> InstructionFactory;
        public AbstractRecipeFactory RecipeFactory;
        public ApplicationDbContext Db;
        public IUserState UserState;
        public ILogger<ISubscriberState> StateLogger;
        public ISubscriberState SubscriberState;
        public ILogger<RecipeController> Logger;
        public IRecipeDTO RecipeDTO;
        public IRecipeScraper Scraper;
        public IRecipeUrlDTO RecipeUrlDTO;
        public RecipeController RecipeController;
        public IRecipeRepository RecipeRepository;
        public IRecipeService RecipeService;

        public RecipeControllerFixture()
        {
            IngredientFactory = new IngredientFactory();
            InstructionFactory = new InstructionFactory();
            Db = new MockDbContext().Context;
            UserState = new MockUserState();
            RecipeFactory = new RecipeFactory();
            StateLogger = new LoggerFactory().CreateLogger<ISubscriberState>();
            SubscriberState = new SubscriberState(StateLogger);
            RecipeDTO = new RecipeDTO();
            Logger = new LoggerFactory().CreateLogger<RecipeController>();
            Scraper = new AllRecipesScraper();
            RecipeUrlDTO = new RecipeUrlDTO();
            RecipeRepository = new RecipeRepository(
                                RecipeFactory,
                                Db,
                                UserState,
                                IngredientFactory,
                                InstructionFactory);
            RecipeService = new RecipeService(RecipeRepository, SubscriberState);
            RecipeController = new RecipeController(Logger, RecipeDTO, Scraper, RecipeService, new HttpClient());
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
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
        public async Task CreateOrUpdateRecipe_WithInvalidRecipeDTO_ShouldReturn400()
        {
            var recipe = Fixture.RecipeDTO;
            recipe.Title = null;
            var postResult = await Fixture.RecipeController.CreateOrUpdateRecipe((RecipeDTO)recipe);

            Assert.IsType<BadRequestResult>(postResult);
        }

        [Fact]
        public async Task CreateOrUpdateRecipe_WithValidRecipeDTO_ShouldReturn422()
        {
            var RecipeDTO = Fixture.RecipeDTO;

            RecipeDTO.URL = "https://www.allrecipes.com/recipe/264739/lemon-garlic-chicken-kebabs/";
            RecipeDTO.Title = "test";
            RecipeDTO.RecipeType = "Breakfast";
            RecipeDTO.Ingredients.Add("test");
            RecipeDTO.Instructions.Add("test");

            var postResult = await Fixture.RecipeController.CreateOrUpdateRecipe((RecipeDTO)RecipeDTO);

            Assert.IsType<UnprocessableEntityResult>(postResult);
        }
  
        [Fact]
        public void SearchRecipes_WithValidSearchParameter_ShouldReturnRecipes()
        {
            string searchText = "Test";

            var results = Fixture.RecipeController.SearchRecipes(searchText);

            Assert.IsAssignableFrom<IEnumerable<IRecipeDTO>>(results);
        }

        [Fact]
        public async Task GetUserRecipes_ShouldReturnRecipeDTO()
        {
            var results = await Fixture.RecipeController.GetUserRecipes();

            Assert.IsAssignableFrom<IEnumerable<IRecipeDTO>>(results);
        }
    }
}
