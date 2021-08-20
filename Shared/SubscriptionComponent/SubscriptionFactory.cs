using PBC.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.SubscriptionComponent
{
    public class SubscriptionFactory : IFactory<RecipeSubscription>
    {
        private readonly RecipeSubscription _recipeSubscription;

        public SubscriptionFactory(RecipeSubscription recipeSubscription)
        {
            _recipeSubscription = recipeSubscription;
        }
        public RecipeSubscription Make()
        {
            return _recipeSubscription;
        }
    }
}
