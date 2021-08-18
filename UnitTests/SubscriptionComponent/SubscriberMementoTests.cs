using PBC.Shared.SubscriptionComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.SubscriptionComponent
{
    public class SubscriberMementoTests : IDisposable
    {
        ISubscriberMemento SubscriberMemento;

        public SubscriberMementoTests()
        {
            SubscriberMemento = new SubscriberMemento();
        }
        public void Dispose()
        {
            SubscriberMemento = new SubscriberMemento();
        }
            
        [Fact]
        public void UpdateState_WithKeyThatDoesNotExist_ShouldAddKeyValuePair()
        {
            int id = 1111;
            
            SubscriberMemento.UpdateState(id);
            
            var subscriptions = SubscriberMemento.GetRecipeSubscriptions();

            Assert.True(subscriptions[id]);
        }
    }
}
