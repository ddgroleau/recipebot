﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PBC.Server.Controllers;
using PBC.Shared;
using PBC.Shared.Common;
using PBC.Shared.RecipeComponent;
using PBC.Shared.SubscriptionComponent;
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
        public ILogger<ISubscriberState> StateLogger;
        public ISubscriberState SubscriberState;
        public ILogger<RecipeController> Logger;
        public IRecipeDTO RecipeDTO;
        public IAllRecipesScraper Scraper;
        public IRecipeUrlDTO RecipeUrlDTO;
        public RecipeController RecipeController;
        public IRecipeRepository RecipeRepository;
        public IRecipeService RecipeService;
        public IBuilder<IRecipeServiceDTO, IRecipeDTO> RecipeBuilder;
        public IRecipeServiceDTO RecipeServiceDTO;

        public RecipeControllerFixture()
        {
            StateLogger = new LoggerFactory().CreateLogger<ISubscriberState>();
            SubscriberState = new SubscriberState(StateLogger);
            RecipeServiceDTO = new RecipeServiceDTO();
            Logger = new LoggerFactory().CreateLogger<RecipeController>();
            RecipeDTO = new RecipeDTO();
            RecipeBuilder = new RecipeBuilder(RecipeServiceDTO, RecipeDTO);
            Scraper = new AllRecipesScraper();
            RecipeUrlDTO = new RecipeUrlDTO();
            RecipeRepository = new RecipeRepository();
            RecipeService = new RecipeService(RecipeBuilder, RecipeRepository, SubscriberState);
            RecipeController = new RecipeController(Logger, RecipeDTO, Scraper, RecipeService);
        }

        public void Dispose()
        {
            StateLogger = new LoggerFactory().CreateLogger<ISubscriberState>();
            SubscriberState = new SubscriberState(StateLogger);
            RecipeServiceDTO = new RecipeServiceDTO();
            Logger = new LoggerFactory().CreateLogger<RecipeController>();
            RecipeDTO = new RecipeDTO();
            RecipeBuilder = new RecipeBuilder(RecipeServiceDTO, RecipeDTO);
            Scraper = new AllRecipesScraper();
            RecipeUrlDTO = new RecipeUrlDTO();
            RecipeRepository = new RecipeRepository();
            RecipeService = new RecipeService(RecipeBuilder, RecipeRepository, SubscriberState);
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
        public void CreateOrUpdateRecipe_WithInvalidRecipeDTO_ShouldReturn400()
        {
            var recipe = Fixture.RecipeDTO;
            recipe.Title = null;
            var postResult = Fixture.RecipeController.CreateOrUpdateRecipe((RecipeDTO)recipe);

            Assert.IsType<BadRequestResult>(postResult);
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
        public void SearchRecipes_WithValidSearchParameter_ShouldReturnRecipes()
        {
            string searchText = "Test";

            var results = Fixture.RecipeController.SearchRecipes(searchText);

            Assert.IsAssignableFrom<IEnumerable<IRecipeDTO>>(results);
        }
    }
}
