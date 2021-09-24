using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public interface IRecipeRepository
     {
        public Task CreateRecipe(IRecipeServiceDTO recipeServiceDTO);
        public IEnumerable<IRecipeServiceDTO> SearchRecipes(string searchText);
        public Task<int> FindRecipe(IRecipeServiceDTO recipeServiceDTO);
        public IRecipeServiceDTO FindRecipeById(int id);
        public void UpdateRecipe(IRecipeServiceDTO recipe);
        public Task<IEnumerable<IRecipeServiceDTO>> GetUserRecipes();
    }
}
