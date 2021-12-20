using Recipebot.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.RecipeComponent
{
    public class RecipeFactory : AbstractRecipeFactory
    {
        public override Recipe Make()
        {
            return new Recipe();
        }

        public override IRecipeDTO MakeRecipeDTO()
        {
            return new RecipeDTO();
        }

        public override IRecipeServiceDTO MakeRecipeServiceDTO()
        {
            return new RecipeServiceDTO();
        }
    }
}
