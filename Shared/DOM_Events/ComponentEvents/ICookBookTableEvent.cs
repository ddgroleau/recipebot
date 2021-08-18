using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.DOM_Events.ComponentEvents
{
    public interface ICookBookTableEvent
    {
        public IRecipeDTO RecipeDTO { get; set; }
        public IEnumerable<IRecipeDTO> RetrievedRecipes { get; set; }
        public ILazor Lazor { get; set; }
        public string Message { get; set; }
        public bool IsDeleteAction { get; set; }
        public Dictionary<int, bool> Loading { get; set; }
        public Task<IEnumerable<IRecipeDTO>> GetRecipesAsync(string userName);
        public void HandleUpdate();
        public void HandleDelete();
        public void HandleDetails();
        public Task<bool> HandleSubscribe();
    }
}
