using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.DOM_Events.ComponentEvents
{
    public interface IEditRecipeEvent
    {
        public Task<IRecipeDTO> HandleValidSubmit(ILazor lazor, IRecipeDTO recipeDTO);
    }
}
