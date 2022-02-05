using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.RecipeComponent
{
    public interface IRecipeRepository
     {
        public Task CreateRecipe(IRecipeDTO RecipeDTO);
        public IEnumerable<IRecipeDTO> SearchRecipes(string searchText);
        public Task<int> FindRecipe(IRecipeDTO RecipeDTO);
        public Task<IRecipeDTO> FindRecipeById(int id);
        public Task UpdateRecipe(IRecipeDTO recipe);
        public Task<IEnumerable<IRecipeDTO>> GetUserRecipes();
    }
}
