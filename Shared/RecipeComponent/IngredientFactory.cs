using Recipebot.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.RecipeComponent
{
    public class IngredientFactory : IFactory<Ingredient>
    {
        public Ingredient Make()
        {
            return new Ingredient();
        }
    }
}
