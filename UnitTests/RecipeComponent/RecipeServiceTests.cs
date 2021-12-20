﻿using Microsoft.Extensions.Logging;
using Recipebot.Server.Data;
using Recipebot.Server.Data.Repositories;
using Recipebot.Shared;
using Recipebot.Shared.Common;
using Recipebot.Shared.RecipeComponent;
using Recipebot.Shared.SubscriptionComponent;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTests.Data;
using UnitTests.MockObjects;
using Xunit;

namespace UnitTests.RecipeComponent
{
    public class RecipeServiceTests : IDisposable
    {
        public IFactory<Ingredient> IngredientFactory;
        public IFactory<Instruction> InstructionFactory;
        AbstractRecipeFactory RecipeFactory;
        public ApplicationDbContext Db;
        public IUserState UserState;
        ILogger<ISubscriberState> StateLogger;
        ISubscriberState SubscriberState;
        IBuilder<IRecipeServiceDTO, IRecipeDTO> RecipeBuilder;
        IRecipeDTO RecipeDTO;
        IRecipeRepository RecipeRepository;
        IRecipeService RecipeService;

        public RecipeServiceTests()
        {
            IngredientFactory = new IngredientFactory();
            InstructionFactory = new InstructionFactory();
            RecipeFactory = new RecipeFactory();
            Db = new MockDbContext().Context;
            UserState = new MockUserState();
            StateLogger = new LoggerFactory().CreateLogger<ISubscriberState>();
            SubscriberState = new SubscriberState(StateLogger);
            RecipeDTO = new RecipeDTO();
            RecipeBuilder = new RecipeBuilder(RecipeFactory);
            RecipeRepository = new RecipeRepository(
                        RecipeFactory,
                        Db,
                        UserState,
                        IngredientFactory,
                        InstructionFactory);
            RecipeService = new RecipeService(RecipeBuilder, RecipeRepository, SubscriberState);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
  
        [Fact]
        public async Task CreateRecipe_WithValidRecipeDTO_ShouldReturnRecipeServiceDTO()
        {
            var recipeDTO = RecipeDTO;

            recipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            recipeDTO.Title = "Test";
            recipeDTO.Description = "Test";
            recipeDTO.RecipeType = "Breakfast";
            recipeDTO.Ingredients = new List<string>()
            {
                "Test"
            };
            recipeDTO.Instructions = new List<string>
            {
                "Test"
            };
           
            var result = await RecipeService.CreateRecipe(recipeDTO);

            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task CreateRecipe_WithInvalidRecipeType_ShouldThrowException()
        {
            var recipeDTO = RecipeDTO;

            recipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            recipeDTO.Title = "Test";
            recipeDTO.Description = "Test";
            recipeDTO.RecipeType = "Supper";
            recipeDTO.Ingredients = new List<string>();
            recipeDTO.Instructions = new List<string>();

            recipeDTO.Instructions.Add("Test");
            recipeDTO.Ingredients.Add("Test");

            await Assert.ThrowsAsync<InvalidOperationException>(async ()=> await RecipeService.CreateRecipe(recipeDTO));
        }

        [Fact]
        public async Task CreateRecipe_WithInvalidRecipeURL_ShouldThrowException()
        {
            var recipeDTO = RecipeDTO;

            recipeDTO.URL = "https://www.allrecipes.com";
            recipeDTO.Title = "Test";
            recipeDTO.Description = "Test";
            recipeDTO.RecipeType = "Dinner";
            recipeDTO.Ingredients = new List<string>();
            recipeDTO.Instructions = new List<string>();
            recipeDTO.Instructions.Add("Test");
            recipeDTO.Ingredients.Add("Test");

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await RecipeService.CreateRecipe(recipeDTO));
        }

        [Fact]
        public async Task CreateRecipe_WithInvalidRecipeTitle_ShouldThrowException()
        {
            var recipeDTO = RecipeDTO;

            recipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            recipeDTO.Title = "";
            recipeDTO.Description = "Test";
            recipeDTO.RecipeType = "Dinner";
            recipeDTO.Ingredients = new List<string>();
            recipeDTO.Instructions = new List<string>();

            recipeDTO.Instructions.Add("Test");
            recipeDTO.Ingredients.Add("Test");

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await RecipeService.CreateRecipe(recipeDTO));
        }

        [Fact]
        public async Task CreateRecipe_WithInvalidIngredients_ShouldThrowException()
        {
            var recipeDTO = RecipeDTO;

            recipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            recipeDTO.Title = "Test";
            recipeDTO.Description = "Test";
            recipeDTO.RecipeType = "Dinner";
            recipeDTO.Ingredients = new List<string>();
            recipeDTO.Instructions = new List<string>();

            recipeDTO.Instructions.Add("Test");

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await RecipeService.CreateRecipe(recipeDTO));
        }

        [Fact]
        public async Task CreateRecipe_WithInvalidInstructions_ShouldThrowException()
        {
            var recipeDTO = RecipeDTO;

            recipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            recipeDTO.Title = "Test";
            recipeDTO.Description = "Test";
            recipeDTO.RecipeType = "Dinner";
            recipeDTO.Ingredients = new List<string>();
            recipeDTO.Instructions = new List<string>();

            recipeDTO.Ingredients.Add("Test");

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await RecipeService.CreateRecipe(recipeDTO));

        }

        [Fact]
        public async Task CreateRecipe_WithInvalidDescription_ShouldThrowException()
        {
            var recipeDTO = RecipeDTO;

            recipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            recipeDTO.Title = "Test";
            recipeDTO.Description = "The central part of a computer, the part that carries out the individual steps that make up our programs," +
                                    " is called the processor. The programs we have seen so far are things that will keep the processor busy " +
                                    "until they have finished their work. The speed at which something like a loop that manipulates numbers can " +
                                    "be executed depends pretty much entirely on the speed of the processor";
            recipeDTO.RecipeType = "Dinner";
            recipeDTO.Ingredients = new List<string>();
            recipeDTO.Instructions = new List<string>();

            recipeDTO.Ingredients.Add("Test");
            recipeDTO.Instructions.Add("Test");

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await RecipeService.CreateRecipe(recipeDTO));

        }

        [Fact]
        public void SearchRecipes_WithSearchText_ShouldReturnRecipeServiceDTOList()
        {
            var results = RecipeService.SearchRecipes("Test");

            Assert.IsAssignableFrom<IEnumerable<IRecipeDTO>>(results);
        }

        [Fact]
        public async Task GetUserRecipes_ShouldReturnRecipeDTOs()
        {
            var result = await RecipeService.GetUserRecipes();

            Assert.IsAssignableFrom<IEnumerable<IRecipeDTO>>(result);
        }

    }
}
