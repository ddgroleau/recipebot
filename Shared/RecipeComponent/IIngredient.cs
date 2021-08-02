using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public interface IIngredient
    {
        public string IngredientId { get; set; }
        public void SetRecipeModelId(string id);
        public string IngredientDescription { get; set; }
    }
}
