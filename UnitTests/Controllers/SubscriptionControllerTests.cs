using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PBC.Server.Controllers;
using PBC.Shared.Common;
using PBC.Shared.RecipeComponent;
using PBC.Shared.SubscriptionComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Controllers
{
    public class SubscriptionControllerFixture : IDisposable
    {
        Recipe Recipe;
        RecipeSubscription Subscription;
        IFactory<RecipeSubscription> SubscriptionFactory;
        public ILogger<ISubscriberState> StateLogger;
        public ISubscriptionRepository SubscriptionRepository;
        public ISubscriberState SubscriberState;
        public ILogger<SubscriptionController> Logger;
        public ISubscriptionService SubscriptionService;
        public SubscriptionController Controller;
            
        public SubscriptionControllerFixture()
        {
            Recipe = new Recipe();
            Subscription = new RecipeSubscription();
            SubscriptionFactory = new SubscriptionFactory(Subscription, Recipe);
            StateLogger = new LoggerFactory().CreateLogger<ISubscriberState>();
            SubscriptionRepository = new SubscriptionRepository(SubscriptionFactory);
            SubscriberState = new SubscriberState(StateLogger);
            SubscriptionService = new SubscriptionService(SubscriberState, SubscriptionRepository);
            Logger = new LoggerFactory().CreateLogger<SubscriptionController>();
            Controller = new SubscriptionController(Logger, SubscriptionService);
        }

        public void Dispose()
        {
            Recipe = new Recipe();
            Subscription = new RecipeSubscription();
            SubscriptionFactory = new SubscriptionFactory(Subscription, Recipe);
            StateLogger = new LoggerFactory().CreateLogger<ISubscriberState>();
            SubscriptionRepository = new SubscriptionRepository(SubscriptionFactory);
            SubscriberState = new SubscriberState(StateLogger);
            SubscriptionService = new SubscriptionService(SubscriberState, SubscriptionRepository);
            Logger = new LoggerFactory().CreateLogger<SubscriptionController>();
            Controller = new SubscriptionController(Logger, SubscriptionService);
        }
    }


    public class SubscriptionControllerTests : IClassFixture<SubscriptionControllerFixture>
    {
        readonly SubscriptionControllerFixture Fixture;

        public SubscriptionControllerTests(SubscriptionControllerFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public void Subscribe_WithValidId_ShouldReturnOk()
        {
            var result = Fixture.Controller.Subscribe(1);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Unsubscribe_WithValidId_ShouldReturnOk()
        {
            var result = Fixture.Controller.Unsubscribe(1);
            Assert.IsType<OkResult>(result);
        }

    }

}
