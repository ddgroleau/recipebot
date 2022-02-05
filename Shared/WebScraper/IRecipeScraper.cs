using Recipebot.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.WebScraper
{
    public interface IRecipeScraper
    {
        public IRecipeDTO ScrapeRecipe(string URL, IRecipeDTO RecipeDTO);
    }
}
