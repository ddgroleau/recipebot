using PBC.Shared.Common;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.SubscriptionComponent
{
    public class SubscriptionFactory : IFactory<RecipeSubscription>
    {
        public RecipeSubscription Make()
        {
            return new RecipeSubscription();
        }
    }
}
