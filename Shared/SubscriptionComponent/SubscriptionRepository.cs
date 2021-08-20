using PBC.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.SubscriptionComponent
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly IFactory<RecipeSubscription> _subscriptionFactory;

        public SubscriptionRepository(IFactory<RecipeSubscription> subscriptionFactory)
        {
            _subscriptionFactory = subscriptionFactory;
        }
        public void CreateSubscription(int id)
        {
            RecipeSubscription subscription = _subscriptionFactory.Make();
            subscription.Recipe.RecipeId = id;
        }
        public void UpdateSubscription(int id)
        {

        }
    }
}
