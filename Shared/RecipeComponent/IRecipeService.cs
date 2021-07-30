using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public interface IRecipeService
    {
        public IRecipeModel CreateRecipe(IRecipeDTO recipeDTO);
       
    }
}
