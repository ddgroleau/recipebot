using PBC.Shared.Common;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public abstract class AbstractRecipeFactory : IFactory<Recipe>
    {
        public abstract Recipe Make();

        public abstract IRecipeDTO MakeRecipeDTO();

        public abstract IRecipeServiceDTO MakeRecipeServiceDTO();
        
    }
}
