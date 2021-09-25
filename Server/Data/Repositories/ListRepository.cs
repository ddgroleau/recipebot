using PBC.Shared.Common;
using PBC.Shared.ListComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Server.Data.Repositories
{
    public class ListRepository : IListRepository
    {
        private readonly AbstractRecipeFactory _recipeFactory;
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserState _userState;

        public ListRepository(AbstractRecipeFactory recipeFactory,ApplicationDbContext context,IUserState userState)
        {
            _recipeFactory = recipeFactory;
            _dbContext = context;
            _userState = userState;
        }
        public async Task CreateListAsync(IListDTO list)
        {

        }
    }
}
