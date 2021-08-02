using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.DOM_Events.ComponentEvents
{
    public interface ISearchBarEvent
    {
        public string SearchText { get; set; }
        public List<IRecipeDTO> RecipesFound { get; set; }

        public Task<List<IRecipeDTO>> HandleKeyPress();
        public Task HandleClick();
    }
}
