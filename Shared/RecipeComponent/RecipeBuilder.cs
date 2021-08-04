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

        public RecipeBuilder(IRecipeModel recipeModel)
        {
            _recipeModel = recipeModel;
        }

        public IRecipeModel Build(IRecipeDTO recipeDTO)
        {
            IRecipeModel recipeModel = _recipeModel;
            recipeModel.URL = recipeDTO.URL;
            recipeModel.Title = recipeDTO.Title;
            recipeModel.Description = recipeDTO.Description;
            recipeModel.RecipeType = recipeDTO.RecipeType;
            recipeModel.Ingredients = recipeDTO.Ingredients;
            recipeModel.Instructions = recipeDTO.Instructions;

            return recipeModel;
        }
    }
}
