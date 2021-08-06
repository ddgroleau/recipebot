using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public class RecipeBuilder : IRecipeBuilder
    {
        private readonly IRecipeServiceDTO _recipeServiceDTO;

        public RecipeBuilder(IRecipeServiceDTO recipeServiceDTO)
        {
            _recipeServiceDTO = recipeServiceDTO;
        }

        public IRecipeServiceDTO Build(IRecipeDTO recipeDTO)
        {
            IRecipeServiceDTO recipeServiceDTO = _recipeServiceDTO;
            recipeServiceDTO.URL = recipeDTO.URL;
            recipeServiceDTO.Title = recipeDTO.Title;
            recipeServiceDTO.Description = recipeDTO.Description;
            recipeServiceDTO.RecipeType = recipeDTO.RecipeType;
            recipeServiceDTO.Ingredients = recipeDTO.Ingredients;
            recipeServiceDTO.Instructions = recipeDTO.Instructions;

            return recipeServiceDTO;
        }
    }
}
