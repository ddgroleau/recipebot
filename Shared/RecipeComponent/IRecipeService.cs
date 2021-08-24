using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public interface IRecipeService
    {
        public IRecipeServiceDTO CreateRecipe(IRecipeDTO recipeDTO);
        public IEnumerable<IRecipeDTO> SearchRecipes(string searchText);

    }
}
