using PBC.Shared;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.RecipeComponent
{
    public class RecipeServiceTests : IDisposable
    {
        IRecipeModel RecipeModel;
        IRecipeBuilder RecipeBuilder;
        IRecipeDTO RecipeDTO;
        RecipeEntity RecipeEntity;
        IRepository<RecipeEntity> RecipeRepository;
        RecipeService RecipeService;

        public RecipeServiceTests()
        {
            RecipeModel = new RecipeModel();
            RecipeBuilder = new RecipeBuilder(RecipeModel);
            RecipeDTO = new RecipeDTO();
            RecipeEntity = new RecipeEntity();
            RecipeRepository = new RecipeRepository();
            RecipeService = new RecipeService(RecipeBuilder, RecipeRepository);
        }

        public void Dispose()
        {
            RecipeModel = new RecipeModel();
            RecipeBuilder = new RecipeBuilder(RecipeModel);
            RecipeDTO = new RecipeDTO();
            RecipeEntity = new RecipeEntity();
            RecipeRepository = new RecipeRepository();
            RecipeService = new RecipeService(RecipeBuilder, RecipeRepository);
        }
  
        [Fact]
        public void CreateRecipe_WithValidRecipeDTO_ShouldReturnRecipeModel()
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

    }
}
