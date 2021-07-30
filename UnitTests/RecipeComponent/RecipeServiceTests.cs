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
        RecipeService RecipeService;

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
        public void CreateRecipe_WithValidRecipeDTO_ShouldReturnRecipeModel()
        {
            var recipeDTO = RecipeDTO;

            recipeDTO.URL = "https://www.allrecipes.com/recipe/234410/no-bake-strawberry-cheesecake/";
            recipeDTO.Title = "Test";
            recipeDTO.Description = "Test";
            recipeDTO.Ingredients = new List<string>();
            recipeDTO.Instructions = new List<string>();

            recipeDTO.Instructions.Add("Test");
            recipeDTO.Ingredients.Add("Test");

            var result = RecipeService.CreateRecipe(recipeDTO);

            Assert.IsAssignableFrom<IRecipeModel>(result);
        }
    }
}
