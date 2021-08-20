using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PBC.Server.Controllers;
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
        public ILogger<ISubscriberState> StateLogger;
        public ISubscriptionRepository SubscriptionRepository;
        public ISubscriberState SubscriberState;
        public ILogger<SubscriptionController> Logger;
        public ISubscriptionService SubscriptionService;
        public SubscriptionController Controller;
            
        public SubscriptionControllerFixture()
        {
            StateLogger = new LoggerFactory().CreateLogger<ISubscriberState>();
            SubscriptionRepository = new SubscriptionRepository();
            SubscriberState = new SubscriberState(StateLogger);
            SubscriptionService = new SubscriptionService(SubscriberState, SubscriptionRepository);
            Logger = new LoggerFactory().CreateLogger<SubscriptionController>();
            Controller = new SubscriptionController(Logger, SubscriptionService);
        }

        public void Dispose()
        {
            StateLogger = new LoggerFactory().CreateLogger<ISubscriberState>();
            SubscriptionRepository = new SubscriptionRepository();
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
