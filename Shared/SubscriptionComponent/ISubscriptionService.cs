using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.SubscriptionComponent
{
    public interface ISubscriptionService
    {
        public void Subscribe(int id);
        public void Unsubscribe(int id);
        public IEnumerable<IRecipeDTO> GetUserRecipes(int userId);

    }
}
