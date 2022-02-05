using Microsoft.Extensions.Logging;
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
            RecipeRepository = new RecipeRepository(
                        RecipeFactory,
                        Db,
                        UserState,
                        IngredientFactory,
                        InstructionFactory);
            RecipeService = new RecipeService(RecipeRepository, SubscriberState);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
  
        [Fact]
        public async Task CreateRecipe_WithValidRecipeDTO_ShouldReturnRecipeDTO()
        {
            RecipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            RecipeDTO.Title = "Test";
            RecipeDTO.Description = "Test";
            RecipeDTO.RecipeType = "Breakfast";
            RecipeDTO.Ingredients = new List<string>()
            {
                "Test"
            };
            RecipeDTO.Instructions = new List<string>
            {
                "Test"
            };
           
            var result = await RecipeService.CreateRecipe(RecipeDTO);

            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task CreateRecipe_WithInvalidRecipeType_ShouldThrowException()
        {
            RecipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            RecipeDTO.Title = "Test";
            RecipeDTO.Description = "Test";
            RecipeDTO.RecipeType = "Supper";
            RecipeDTO.Ingredients = new List<string>();
            RecipeDTO.Instructions = new List<string>();

            RecipeDTO.Instructions.Add("Test");
            RecipeDTO.Ingredients.Add("Test");

            await Assert.ThrowsAsync<InvalidOperationException>(async ()=> await RecipeService.CreateRecipe(RecipeDTO));
        }

        [Fact]
        public async Task CreateRecipe_WithInvalidRecipeURL_ShouldThrowException()
        {
            RecipeDTO.URL = "https://www.allrecipes.com";
            RecipeDTO.Title = "Test";
            RecipeDTO.Description = "Test";
            RecipeDTO.RecipeType = "Dinner";
            RecipeDTO.Ingredients = new List<string>();
            RecipeDTO.Instructions = new List<string>();
            RecipeDTO.Instructions.Add("Test");
            RecipeDTO.Ingredients.Add("Test");

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await RecipeService.CreateRecipe(RecipeDTO));
        }

        [Fact]
        public async Task CreateRecipe_WithInvalidRecipeTitle_ShouldThrowException()
        {
            RecipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            RecipeDTO.Title = "";
            RecipeDTO.Description = "Test";
            RecipeDTO.RecipeType = "Dinner";
            RecipeDTO.Ingredients = new List<string>();
            RecipeDTO.Instructions = new List<string>();

            RecipeDTO.Instructions.Add("Test");
            RecipeDTO.Ingredients.Add("Test");

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await RecipeService.CreateRecipe(RecipeDTO));
        }

        [Fact]
        public async Task CreateRecipe_WithInvalidIngredients_ShouldThrowException()
        {
            RecipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            RecipeDTO.Title = "Test";
            RecipeDTO.Description = "Test";
            RecipeDTO.RecipeType = "Dinner";
            RecipeDTO.Ingredients = new List<string>();
            RecipeDTO.Instructions = new List<string>();

            RecipeDTO.Instructions.Add("Test");

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await RecipeService.CreateRecipe(RecipeDTO));
        }

        [Fact]
        public async Task CreateRecipe_WithInvalidInstructions_ShouldThrowException()
        {
            RecipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            RecipeDTO.Title = "Test";
            RecipeDTO.Description = "Test";
            RecipeDTO.RecipeType = "Dinner";
            RecipeDTO.Ingredients = new List<string>();
            RecipeDTO.Instructions = new List<string>();

            RecipeDTO.Ingredients.Add("Test");

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await RecipeService.CreateRecipe(RecipeDTO));

        }

        [Fact]
        public async Task CreateRecipe_WithInvalidDescription_ShouldThrowException()
        {
            RecipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            RecipeDTO.Title = "Test";
            RecipeDTO.Description = "The central part of a computer, the part that carries out the individual steps that make up our programs," +
                                    " is called the processor. The programs we have seen so far are things that will keep the processor busy " +
                                    "until they have finished their work. The speed at which something like a loop that manipulates numbers can " +
                                    "be executed depends pretty much entirely on the speed of the processor";
            RecipeDTO.RecipeType = "Dinner";
            RecipeDTO.Ingredients = new List<string>();
            RecipeDTO.Instructions = new List<string>();

            RecipeDTO.Ingredients.Add("Test");
            RecipeDTO.Instructions.Add("Test");

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await RecipeService.CreateRecipe(RecipeDTO));

        }

        [Fact]
        public void SearchRecipes_WithSearchText_ShouldReturnRecipeDTOList()
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
