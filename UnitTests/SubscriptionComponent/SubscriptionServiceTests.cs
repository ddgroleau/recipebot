using Microsoft.Extensions.Logging;
using PBC.Server.Data;
using PBC.Server.Data.Repositories;
using PBC.Shared;
using PBC.Shared.Common;
using PBC.Shared.RecipeComponent;
using PBC.Shared.SubscriptionComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UnitTests.Data;
using Xunit;

namespace UnitTests.SubscriptionComponent
{
    public class SubscriptionServiceFixture : IDisposable
    {
        public ApplicationDbContext Db;
        public AbstractRecipeFactory RecipeFactory;
        public IRecipeServiceDTO RecipeServiceDTO;
        public IBuilder<IRecipeServiceDTO, IRecipeDTO> RecipeBuilder;
        public IRecipeDTO RecipeDTO;
        public Recipe Recipe;
        public RecipeSubscription Subscription;
        public IFactory<RecipeSubscription> SubscriptionFactory;
        public ILogger<ISubscriberState> StateLogger;
        public ISubscriptionRepository SubscriptionRepository;
        public ISubscriberState SubscriberState;
        public ISubscriptionService SubscriptionService;

        public SubscriptionServiceFixture()
        {
            Db = new MockDbContext().Context;
            RecipeFactory = new RecipeFactory();
            RecipeDTO = new RecipeDTO();
            RecipeServiceDTO = new RecipeServiceDTO();
            RecipeBuilder = new RecipeBuilder(RecipeFactory);
            Recipe = new Recipe();
            Subscription = new RecipeSubscription();
            SubscriptionFactory = new SubscriptionFactory(Subscription, Recipe);
            StateLogger = new LoggerFactory().CreateLogger<ISubscriberState>();
            SubscriptionRepository = new SubscriptionRepository(SubscriptionFactory, Db);
            SubscriberState = new SubscriberState(StateLogger);
            SubscriptionService = new SubscriptionService(SubscriberState, SubscriptionRepository, RecipeBuilder);
        }

        public void Dispose()
        {
            Db = null;
            RecipeFactory =null;
            RecipeDTO =null;
            RecipeServiceDTO = null;
            RecipeBuilder = null;
            Recipe = null;
            Subscription = null;
            SubscriptionFactory = null;
            StateLogger =null;
            SubscriptionRepository = null;
            SubscriberState = null;
            SubscriptionService = null;
        }
    }

    public class SubscriptionServiceTests : IClassFixture<SubscriptionServiceFixture>
    {
        private readonly SubscriptionServiceFixture Fixture;
        public SubscriptionServiceTests(SubscriptionServiceFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public void CreateSubscription_WithValidId_ShouldNotifyState()
        {
            Fixture.SubscriptionService.CreateSubscription(1); // This will notify the state object to change its HasChanged flag to true.
            Task result = Fixture.SubscriberState.GetRecipeSubscriptions(1); // This will attempt to send an HTTP request if HasChanged is true.
            Assert.ThrowsAsync<HttpRequestException>(() => result); // This will test that CreateSubscription updated the state object so that it will send an HTTP request to get its new state.
        }
        [Fact]
        public void UpdateSubscription_WithValidId_ShouldNotifyState()
        {
            Fixture.SubscriptionService.UpdateSubscription(1);
            Task result = Fixture.SubscriberState.GetRecipeSubscriptions(1);
            Assert.ThrowsAsync<HttpRequestException>(() => result);
        }

        [Fact]
        public void GetSubscriptions_WithValidId_ShouldReturnRecipes()
        {
            var result = Fixture.SubscriptionService.GetUserRecipes(123);
            Assert.IsAssignableFrom<IEnumerable<IRecipeDTO>>(result);
        }

    }
}
