using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.ListComponent
{
    public interface IListBuilder
    {
        public IListDayDTO Build(IEnumerable<IRecipeDTO> userRecipes);
        public IListDTO Build(IListGeneratorDTO listGeneratorDTO);
    }
}
