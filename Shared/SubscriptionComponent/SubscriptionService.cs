using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.SubscriptionComponent
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
        public void CreateSubscription(int id)
        {
            _subscriberState.UpdateState();
            _subscriptionRepository.CreateSubscription(id);
        }

        public void UpdateSubscription(int id)
        {
            _subscriberState.UpdateState();
            _subscriptionRepository.UpdateSubscription(id);
        }
    }
}
