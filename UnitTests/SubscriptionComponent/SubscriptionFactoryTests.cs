using PBC.Shared.Common;
using PBC.Shared.SubscriptionComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.SubscriptionComponent
{
    public class SubscriptionFactoryFixture : IDisposable
    {
       public RecipeSubscription RecipeSubscription;
        public IFactory<RecipeSubscription> SubscriptionFactory;

        public SubscriptionFactoryFixture()
        {
            RecipeSubscription = new RecipeSubscription();
            SubscriptionFactory = new SubscriptionFactory(RecipeSubscription);
        }

        public void Dispose()
        {
            RecipeSubscription = new RecipeSubscription();
            SubscriptionFactory = new SubscriptionFactory(RecipeSubscription);
        }
    }

    public class SubscriptionFactoryTests : IClassFixture<SubscriptionFactoryFixture>
    {
        private readonly SubscriptionFactoryFixture Fixture;
        public SubscriptionFactoryTests(SubscriptionFactoryFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public void Make_WithNoParameters_ShouldReturnRecipeSubscriptionObject()
        {
            var newSubscription = Fixture.SubscriptionFactory.Make();
            Assert.Equal(Fixture.RecipeSubscription, newSubscription);
        }
    }
}
