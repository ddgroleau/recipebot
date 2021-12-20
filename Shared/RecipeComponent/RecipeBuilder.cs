using Recipebot.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.RecipeComponent
{
    public class RecipeBuilder : IBuilder<IRecipeServiceDTO, IRecipeDTO>
    {
        AbstractRecipeFactory _recipeFactory;

        public RecipeBuilder(AbstractRecipeFactory recipeFactory)
        {
            _recipeFactory = recipeFactory;
        }

        public IRecipeServiceDTO Build(IRecipeDTO recipeDTO)
        {
            IRecipeServiceDTO recipeServiceDTO = _recipeFactory.MakeRecipeServiceDTO();

            recipeServiceDTO.RecipeId = recipeDTO.RecipeId;
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
            var recipeDTO = _recipeFactory.MakeRecipeDTO();

            recipeDTO.RecipeId = recipeServiceDTO.RecipeId;
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
