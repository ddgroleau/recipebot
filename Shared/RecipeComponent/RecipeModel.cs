using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public class RecipeModel : IRecipeModel
    {
        private string RecipeModelId { get; set; }
        public void SetRecipeModelId(string id)
        {
            RecipeModelId = id;
        }

        public string URL { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<IIngredient> Ingredients { get; set; } = new List<IIngredient>();
        public ICollection<IInstruction> Instructions { get; set; } = new List<IInstruction>();
    }
}
