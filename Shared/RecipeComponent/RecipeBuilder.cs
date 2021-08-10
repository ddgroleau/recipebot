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
        private readonly IRecipeDTO _recipeDTO;

        public RecipeBuilder(IRecipeServiceDTO recipeServiceDTO, IRecipeDTO recipeDTO)
        {
            _recipeServiceDTO = recipeServiceDTO;
            _recipeDTO = recipeDTO;
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

        public IRecipeDTO Build(IRecipeServiceDTO recipeServiceDTO)
        {
            IRecipeDTO recipeDTO = _recipeDTO;
            
            recipeDTO.URL = recipeServiceDTO.URL;
            recipeDTO.Title = recipeServiceDTO.Title;
            recipeDTO.Description = recipeServiceDTO.Description;
            recipeDTO.RecipeType = recipeServiceDTO.RecipeType;
            recipeDTO.Ingredients = recipeServiceDTO.Ingredients;
            recipeDTO.Instructions = recipeServiceDTO.Instructions;

            return recipeDTO;        
        }
    }
}
