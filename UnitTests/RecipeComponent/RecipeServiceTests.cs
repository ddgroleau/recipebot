using Microsoft.Extensions.Logging;
using PBC.Server.Data;
using PBC.Server.Data.Repositories;
using PBC.Shared;
using PBC.Shared.Common;
using PBC.Shared.RecipeComponent;
using PBC.Shared.SubscriptionComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Data;
using UnitTests.MockObjects;
using Xunit;

namespace UnitTests.RecipeComponent
{
    public class RecipeServiceTests : IDisposable
    {
        AbstractRecipeFactory RecipeFactory;
        public ApplicationDbContext Db;
        public IUserState UserState;
        ILogger<ISubscriberState> StateLogger;
        ISubscriberState SubscriberState;
        IRecipeServiceDTO RecipeServiceDTO;
        IBuilder<IRecipeServiceDTO, IRecipeDTO> RecipeBuilder;
        IRecipeDTO RecipeDTO;
        IRecipeRepository RecipeRepository;
        IRecipeService RecipeService;

        public RecipeServiceTests()
        {
            RecipeFactory = new RecipeFactory();
            Db = new MockDbContext().Context;
            UserState = new MockUserState();
            StateLogger = new LoggerFactory().CreateLogger<ISubscriberState>();
            SubscriberState = new SubscriberState(StateLogger);
            RecipeServiceDTO = new RecipeServiceDTO();
            RecipeDTO = new RecipeDTO();
            RecipeBuilder = new RecipeBuilder(RecipeFactory);
            RecipeRepository = new RecipeRepository(RecipeFactory,Db,UserState);
            RecipeService = new RecipeService(RecipeBuilder, RecipeRepository, SubscriberState);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
  
        [Fact]
        public void CreateRecipe_WithValidRecipeDTO_ShouldReturnRecipeServiceDTO()
        {
            var recipeDTO = RecipeDTO;

            recipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            recipeDTO.Title = "Test";
            recipeDTO.Description = "Test";
            recipeDTO.RecipeType = "Breakfast";
            recipeDTO.Ingredients = new List<string>();
            recipeDTO.Instructions = new List<string>();

            recipeDTO.Instructions.Add("Test");
            recipeDTO.Ingredients.Add("Test");

            var result = RecipeService.CreateRecipe(recipeDTO);

            Assert.Equal(result.URL, recipeDTO.URL);
            Assert.Equal(result.Title, recipeDTO.Title);
            Assert.Equal(result.Description, recipeDTO.Description);
            Assert.Equal(result.Ingredients, result.Ingredients);
            Assert.Equal(result.Instructions, result.Instructions);
        }

        [Fact]
        public void CreateRecipe_WithInvalidRecipeType_ShouldThrowException()
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

            Assert.Throws<InvalidOperationException>(() => RecipeService.CreateRecipe(recipeDTO));
        }

        [Fact]
        public void CreateRecipe_WithInvalidRecipeURL_ShouldThrowException()
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

            Assert.Throws<InvalidOperationException>(() => RecipeService.CreateRecipe(recipeDTO));
        }

        [Fact]
        public void CreateRecipe_WithInvalidRecipeTitle_ShouldThrowException()
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

            Assert.Throws<InvalidOperationException>(() => RecipeService.CreateRecipe(recipeDTO));
        }

        [Fact]
        public void CreateRecipe_WithInvalidIngredients_ShouldThrowException()
        {
            var recipeDTO = RecipeDTO;

            recipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            recipeDTO.Title = "Test";
            recipeDTO.Description = "Test";
            recipeDTO.RecipeType = "Dinner";
            recipeDTO.Ingredients = new List<string>();
            recipeDTO.Instructions = new List<string>();

            recipeDTO.Instructions.Add("Test");

            Assert.Throws<InvalidOperationException>(() => RecipeService.CreateRecipe(recipeDTO));
        }

        [Fact]
        public void CreateRecipe_WithInvalidInstructions_ShouldThrowException()
        {
            var recipeDTO = RecipeDTO;

            recipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            recipeDTO.Title = "Test";
            recipeDTO.Description = "Test";
            recipeDTO.RecipeType = "Dinner";
            recipeDTO.Ingredients = new List<string>();
            recipeDTO.Instructions = new List<string>();

            recipeDTO.Ingredients.Add("Test");

            Assert.Throws<InvalidOperationException>(() => RecipeService.CreateRecipe(recipeDTO));
        }

        [Fact]
        public void CreateRecipe_WithInvalidDescription_ShouldThrowException()
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

            Assert.Throws<InvalidOperationException>(() => RecipeService.CreateRecipe(recipeDTO));
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
