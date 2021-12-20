using Recipebot.Shared.Common;
using Recipebot.Shared.RecipeComponent;
using Recipebot.Shared.SubscriptionComponent;
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
        public IFactory<RecipeSubscription> SubscriptionFactory;

        public SubscriptionFactoryFixture()
        {
            SubscriptionFactory = new SubscriptionFactory();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
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
            Assert.IsAssignableFrom<RecipeSubscription>(newSubscription);
        }
    }
}
