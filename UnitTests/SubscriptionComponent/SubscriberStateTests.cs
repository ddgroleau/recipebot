using PBC.Shared.SubscriptionComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.SubscriptionComponent
{
    public class SubscriberStateTests : IDisposable
    {
        ISubscriberState SubscriberState;

        public SubscriberStateTests()
        {
            SubscriberState = new SubscriberState();
        }
        public void Dispose()
        {
            SubscriberState = new SubscriberState();
        }
            
        [Fact]
        public void UpdateState_WithKeyThatDoesNotExist_ShouldAddKeyValuePair()
        {
            int id = 1111;

            SubscriberState.UpdateState(id);
            
            var subscriptions = SubscriberState.GetRecipeSubscriptions();

            Assert.True(subscriptions[id]);
        }
    }
}
