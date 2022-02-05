using Recipebot.Shared.Common;
using Recipebot.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.SubscriptionComponent
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriberState _subscriberState;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriberState subscriberState, ISubscriptionRepository subscriptionRepository)
        {
            _subscriberState = subscriberState;
            _subscriptionRepository = subscriptionRepository;
        }
        public void Subscribe(int recipeId)
        {
            _subscriberState.UpdateState();
            _subscriptionRepository.Subscribe(recipeId);
        }

        public void Unsubscribe(int recipeId)
        {
            _subscriberState.UpdateState();
            _subscriptionRepository.Unsubscribe(recipeId);
        }

    }
}
