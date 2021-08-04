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
        public string NewIngredient { get; set; }
        public string NewInstruction { get; set; }
        public Task<IRecipeDTO> HandleValidSubmit(ILazor lazor, IRecipeDTO recipeDTO);
        public void ResetRecipe(IRecipeDTO recipeDTO);
        public IRecipeDTO AddIngredient(IRecipeDTO recipeDTO);
        public IRecipeDTO AddInstruction(IRecipeDTO recipeDTO);
    }
}
