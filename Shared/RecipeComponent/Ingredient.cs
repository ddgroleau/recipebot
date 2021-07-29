using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public class Ingredient : IIngredient
    {
        public string IngredientId { get; set; } = Guid.NewGuid().ToString();
        public string RecipeModelId { get; set; }
        public string IngredientDescription { get; set; }
    }
}
