using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.SubscriptionComponent
{
    public interface ISubscriberState
    {
        public bool UpdateState();
        public Task<IEnumerable<IRecipeDTO>> GetUserRecipes();
    }
}
