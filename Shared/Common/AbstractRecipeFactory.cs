using PBC.Shared.Common;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.Common
{
    public abstract class AbstractRecipeFactory : IFactory<Recipe>
    {
        public Recipe Make()
        {
            return new Recipe();
        }

        public IRecipeDTO MakeRecipeDTO()
        {
            return new RecipeDTO();
        }

        public IRecipeServiceDTO MakeRecipeServiceDTO()
        {
            return new RecipeServiceDTO();
        }
    }
}
