using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.DOM_Events.ComponentEvents
{
    public interface IGeneratedDayEvent
    {
        public Dictionary<string, bool> Loading { get; set; }
        public Dictionary<string, string> RefreshSymbol { get; set; }
        public Task<IRecipeDTO> RegenerateRecipe(string recipeType);
    }
}
