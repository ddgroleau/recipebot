using Microsoft.Extensions.Logging;
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
        ILogger<ISubscriberState> Logger;

        public SubscriberStateTests()
        {
            Logger = new LoggerFactory().CreateLogger<ISubscriberState>();
            SubscriberState = new SubscriberState(Logger);
        }
        public void Dispose()
        {
            Logger = new LoggerFactory().CreateLogger<ISubscriberState>();
            SubscriberState = new SubscriberState(Logger);
        }
            
        [Fact]
        public async Task UpdateState_WithKeyThatDoesNotExist_ShouldAddKeyValuePair()
        {
            int id = 1111;

            SubscriberState.UpdateState(id);
            
            var subscriptions = await SubscriberState.GetRecipeSubscriptions();

            Assert.True(subscriptions[id]);
        }
    }
}
