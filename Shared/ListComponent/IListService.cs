using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.ListComponent
{
    public interface IListService
    {
        public Task<IListDayDTO> GenerateDayOfRecipes();
        public IListDTO CreateList(IListGeneratorDTO listGeneratorDTO);
    }
}
