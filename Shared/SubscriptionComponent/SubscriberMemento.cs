using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.SubscriptionComponent
{
    public class SubscriberMemento : ISubscriberMemento
    {
        private Dictionary<int, bool> RecipeSubscriptions { get; set; } = new();
        private bool SubscriptionsHaveChanged { get; set; }
        public void UpdateState(int id)
        {
            bool isSubscribed = RecipeSubscriptions.TryGetValue(id,out _);
            if(isSubscribed)
            {
                RecipeSubscriptions[id] = !RecipeSubscriptions[id];
            }
            else
            {
                RecipeSubscriptions.Add(id, true);
            }
            SubscriptionsHaveChanged = true;
        }

        public Dictionary<int, bool> GetRecipeSubscriptions()
        {
            if(SubscriptionsHaveChanged)
            {
                // get recipe subscriptions from controller
                SubscriptionsHaveChanged = false;
                return RecipeSubscriptions;
            }
            return RecipeSubscriptions;
        }
    }
}
