using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public class IngredientFactory : IFactory<Ingredient>
    {
        private readonly Ingredient _ingredient;

        public IngredientFactory(Ingredient ingredient)
        {
            _ingredient = ingredient;
        }

        public Ingredient Make()
        {
            Ingredient newIngredient = _ingredient;
            return newIngredient;
        }
    }
}
