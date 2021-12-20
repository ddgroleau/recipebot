using Recipebot.Shared.Common;
using Recipebot.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.SubscriptionComponent
{
    public class SubscriptionFactory : IFactory<RecipeSubscription>
    {
        public RecipeSubscription Make()
        {
            return new RecipeSubscription();
        }
    }
}
