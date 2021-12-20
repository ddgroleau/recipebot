using Recipebot.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.DOM_Events.ComponentEvents
{
    public interface ISearchBarEvent
    {
        public ILazor Lazor { get; set; }
        public string SearchText { get; set; }
        public IEnumerable<IRecipeDTO> SearchResults { get; set; }
        public Task<IEnumerable<IRecipeDTO>> HandleKeyPress();
        public Task HandleClick();
    }
}
