using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public interface IRecipeModel
    {
        public void SetRecipeModelId(string id);
        public string RecipeType { get; set; }

        public string URL { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<IIngredient> Ingredients { get; set; }
        public ICollection<IInstruction> Instructions { get; set; }
    }
}
