using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.DOM_Events.ComponentEvents
{
    public interface ICreateRecipeEvent
    {
        public IRecipeDTO RecipeDTO { get; set; }
        public IRecipeUrlDTO RecipeUrlDTO { get; set; }
        public ILazor Lazor { get; set; }
        public Task<IRecipeDTO> HandleSubmit();
        public void ResetView();
    }
}
