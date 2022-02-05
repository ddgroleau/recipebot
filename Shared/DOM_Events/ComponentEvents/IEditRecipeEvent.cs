using Recipebot.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.DOM_Events.ComponentEvents
{
    public interface IEditRecipeEvent
    {
        public string NewIngredient { get; set; }
        public string NewInstruction { get; set; }
        public Task<IRecipeDTO> HandleValidSubmit(ILazor lazor, IRecipeDTO RecipeDTO);
        public void ResetRecipe(IRecipeDTO RecipeDTO);
        public IRecipeDTO AddIngredient(IRecipeDTO RecipeDTO);
        public IRecipeDTO AddInstruction(IRecipeDTO RecipeDTO);
    }
}
