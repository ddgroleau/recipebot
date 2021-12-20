using Recipebot.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.ListComponent
{
    public interface IListBuilder
    {
        public IListDayDTO Build(IEnumerable<IRecipeDTO> userRecipes);
        public IListDTO Build(IListGeneratorDTO listGeneratorDTO);
        public RecipeDTO GenerateRandomRecipeByType(IEnumerable<IRecipeDTO> userRecipes, string recipeType);
    }
}
