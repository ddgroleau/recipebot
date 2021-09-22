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
using UnitTests.MockObjects;
using Xunit;

namespace UnitTests.SubscriptionComponent
{
    public class SubscriptionServiceFixture : IDisposable
    {
        public ApplicationDbContext Db;
        public IUserState UserState;
        public AbstractRecipeFactory RecipeFactory;
        public IRecipeServiceDTO RecipeServiceDTO;
        public IBuilder<IRecipeServiceDTO, IRecipeDTO> RecipeBuilder;
        public IRecipeDTO RecipeDTO;
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
            UserState = new MockUserState();
            RecipeServiceDTO = new RecipeServiceDTO();
            RecipeBuilder = new RecipeBuilder(RecipeFactory);
            SubscriptionFactory = new SubscriptionFactory();
            StateLogger = new LoggerFactory().CreateLogger<ISubscriberState>();
            SubscriptionRepository = new SubscriptionRepository(SubscriptionFactory, Db, UserState);
            SubscriberState = new SubscriberState(StateLogger);
            SubscriptionService = new SubscriptionService(SubscriberState, SubscriptionRepository, RecipeBuilder);
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
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
        public void Subscribe_WithValidId_ShouldNotifyState()
        {
            Fixture.SubscriptionService.Subscribe(1); // This will notify the state object to change its HasChanged flag to true.
            Task result = Fixture.SubscriberState.GetUserRecipes(); // This will attempt to send an HTTP request if HasChanged is true.
            Assert.ThrowsAsync<HttpRequestException>(() => result); // This will test that Subscribe updated the state object so that it will send an HTTP request to get its new state.
        }
        [Fact]
        public void Unsubscribe_WithValidId_ShouldNotifyState()
        {
            Fixture.SubscriptionService.Unsubscribe(1);
            Task result = Fixture.SubscriberState.GetUserRecipes();
            Assert.ThrowsAsync<HttpRequestException>(() => result);
        }

    }
}
