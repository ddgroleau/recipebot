using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public class IngredientFactory : IFactory<IIngredient>
    {
        public IIngredient Make()
        {
            return new Ingredient();
        }
    }
}
