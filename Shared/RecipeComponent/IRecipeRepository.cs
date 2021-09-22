using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public interface IRecipeRepository
     {
        public void CreateRecipe(IRecipeServiceDTO recipe);
        public IEnumerable<IRecipeServiceDTO> SearchRecipes(string text);
        public IRecipeServiceDTO FindOne(int id);
        public void UpdateRecipe(IRecipeServiceDTO recipe);
        public Task<IEnumerable<IRecipeServiceDTO>> GetUserRecipes();
    }
}
