using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public class RecipeBuilder : IRecipeBuilder
    {
        private readonly IRecipeModel _recipeModel;
        private readonly IFactory<IInstruction> _instructionFactory;
        private readonly IFactory<IIngredient> _ingredientFactory;

        public RecipeBuilder(IRecipeModel recipeModel, IFactory<IInstruction> instructionFactory, IFactory<IIngredient> ingredientFactory )
        {
            _recipeModel = recipeModel;
            _instructionFactory = instructionFactory;
            _ingredientFactory = ingredientFactory;
        }

        public IRecipeModel Build(IRecipeDTO recipeDTO)
        {
            IRecipeModel recipeModel = _recipeModel;
            recipeModel.SetRecipeModelId(recipeDTO.RecipeDtoId);
            recipeModel.URL = recipeDTO.URL;
            recipeModel.Title = recipeDTO.Title;
            recipeModel.Description = recipeDTO.Description;

            for (int i = 0; i < recipeDTO.Instructions.Count; i++)
            {
                var instruction = _instructionFactory.Make();
                
                instruction.StepNumber = i + 1;
                instruction.InstructionDescription = recipeDTO.Instructions.ElementAt(i);
                instruction.SetRecipeModelId(recipeDTO.RecipeDtoId);
                
                recipeModel.Instructions.Add(instruction);
            }

            foreach (var item in recipeDTO.Ingredients)
            {
                var ingredient = new Ingredient
                {
                    IngredientDescription = item
                };
                ingredient.SetRecipeModelId(recipeDTO.RecipeDtoId);
                recipeModel.Ingredients.Add(ingredient);
            }
            return recipeModel;
        }
    }
}
