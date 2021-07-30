using PBC.Shared;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.RecipeComponent
{
    public class RecipeServiceTests : IDisposable
    {
        IFactory<IIngredient> IngredientFactory;
        IFactory<IInstruction> InstructionFactory;
        IRecipeModel RecipeModel;
        IRecipeBuilder RecipeBuilder;
        IRecipeDTO RecipeDTO;
        IRecipeEntity RecipeEntity;
        IRepository<IRecipeEntity> RecipeRepository;
        IRecipeService RecipeService;
        public RecipeServiceTests()
        {
            IngredientFactory = new IngredientFactory();
            InstructionFactory = new InstructionFactory();
            RecipeModel = new RecipeModel();
            RecipeBuilder = new RecipeBuilder(RecipeModel, InstructionFactory, IngredientFactory);
            RecipeDTO = new RecipeDTO();
            RecipeEntity = new RecipeEntity();
            RecipeRepository = new RecipeRepository();
            RecipeService = new RecipeService(RecipeBuilder, RecipeRepository);
        }

        public void Dispose()
        {
            IngredientFactory = new IngredientFactory();
            InstructionFactory = new InstructionFactory();
            RecipeModel = new RecipeModel();
            RecipeBuilder = new RecipeBuilder(RecipeModel, InstructionFactory, IngredientFactory);
            RecipeDTO = new RecipeDTO();
            RecipeEntity = new RecipeEntity();
            RecipeRepository = new RecipeRepository();
            RecipeService = new RecipeService(RecipeBuilder, RecipeRepository);
        }
        [Fact]
        public void RecipeIsValid_WithValidRecipeDTO_ShouldReturnTrue()
        {
            var recipeDTO = RecipeDTO;

            recipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            recipeDTO.Title = "Test";
            recipeDTO.Description = "Test";
            recipeDTO.Ingredients = new List<string>();
            recipeDTO.Instructions = new List<string>();

            recipeDTO.Instructions.Add("Test");
            recipeDTO.Ingredients.Add("Test");

            var isValid = RecipeService.RecipeIsValid(recipeDTO);

            Assert.True(isValid);
        }

        [Fact]
        public void RecipeIsValid_WithInvalidRecipeDTO_ShouldReturnFalse()
        {
            var recipeDTO = RecipeDTO;

            recipeDTO.URL = "https://www.allrecipes.com/recip";
            recipeDTO.Title = "";
            recipeDTO.Description = "Test";
          
            var isValid = RecipeService.RecipeIsValid(recipeDTO);

            Assert.False(isValid);
        }

        [Fact]
        public void CreateRecipeModel_WithValidRecipeDTO_ShouldReturnRecipeModel()
        {
            var recipeDTO = RecipeDTO;

            recipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            recipeDTO.Title = "Test";
            recipeDTO.Description = "Test";
            recipeDTO.Ingredients = new List<string>();
            recipeDTO.Instructions = new List<string>();

            recipeDTO.Instructions.Add("Test");
            recipeDTO.Ingredients.Add("Test");

            var result = RecipeService.CreateRecipeModel(recipeDTO);

            Assert.IsAssignableFrom<IRecipeModel>(result);
        }

        [Fact]
        public void SaveRecipe_WithValidRecipeModel_ShouldSucceed()
        {
            RecipeModel.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            RecipeModel.Title = "Test";
            RecipeModel.Description = "Test";
            RecipeModel.RecipeModelId = Guid.NewGuid().ToString();
            RecipeModel.Instructions.Add(InstructionFactory.Make());
            RecipeModel.Ingredients.Add(IngredientFactory.Make());

            Assert.IsAssignableFrom<IRecipeModel>(RecipeService.SaveRecipe(RecipeModel));
        }
    }
}
