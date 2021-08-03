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
    public class RecipeBuilderTests : IDisposable
    {
        IFactory<IIngredient> IngredientFactory;
        IFactory<IInstruction> InstructionFactory;
        IRecipeModel RecipeModel;
        IRecipeBuilder RecipeBuilder;
        IRecipeDTO RecipeDTO;

        public RecipeBuilderTests()
        {
            IngredientFactory = new IngredientFactory();
            InstructionFactory = new InstructionFactory();
            RecipeModel = new RecipeModel();
            RecipeBuilder = new RecipeBuilder(RecipeModel, InstructionFactory, IngredientFactory);
            RecipeDTO = new RecipeDTO();
        }

        public void Dispose()
        {
            IngredientFactory = new IngredientFactory();
            InstructionFactory = new InstructionFactory();
            RecipeModel = new RecipeModel();
            RecipeBuilder = new RecipeBuilder(RecipeModel, InstructionFactory, IngredientFactory);
            RecipeDTO = new RecipeDTO();
        }

        [Fact]
        public void Build_WithValidRecipeDTO_ShouldReturnRecipeModel()
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

            var result = RecipeBuilder.Build(recipeDTO);

            Assert.Equal(result.URL, recipeDTO.URL);
            Assert.Equal(result.Title, recipeDTO.Title);
            Assert.Equal(result.Description, recipeDTO.Description);
            Assert.IsAssignableFrom<ICollection<IIngredient>>(result.Ingredients);
            Assert.IsAssignableFrom<ICollection<IInstruction>>(result.Instructions);
        }
    }
}
