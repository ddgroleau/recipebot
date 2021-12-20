using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.ListComponent
{
    public interface IListRepository
    {
        public Task CreateListAsync(IListDTO list);
      
    }
}
