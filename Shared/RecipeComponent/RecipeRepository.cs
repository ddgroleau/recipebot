using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public class RecipeRepository : IRecipeRepository
    {
        public void InsertOne(Object record) 
        {
            // Add a recipe to recipe table
            // Add a recipe to RecipeSubscriptions
        }
        public IEnumerable<IRecipeServiceDTO> FindMany(string text)
        {
            return new List<RecipeServiceDTO>();
        }

    }
}
