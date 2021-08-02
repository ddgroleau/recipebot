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
        private string RecipeModelId { get; set; }
        public void SetRecipeModelId(string id)
        {
            RecipeModelId = id;
        }
        public string IngredientDescription { get; set; }
    }
}
