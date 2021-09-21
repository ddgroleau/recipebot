using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PBC.Server.Controllers;
using PBC.Server.Data;
using PBC.Server.Data.Repositories;
using PBC.Shared;
using PBC.Shared.Common;
using PBC.Shared.RecipeComponent;
using PBC.Shared.SubscriptionComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Data;
using UnitTests.MockObjects;
using Xunit;

namespace UnitTests.Controllers
{
    public class SubscriptionControllerFixture : IDisposable
    {
        public ApplicationDbContext Db;
        public AbstractRecipeFactory RecipeFactory;
        public IUserState UserState;
        public IRecipeServiceDTO RecipeServiceDTO;
        public IBuilder<IRecipeServiceDTO, IRecipeDTO> RecipeBuilder;
        public IRecipeDTO RecipeDTO;
        public IFactory<RecipeSubscription> SubscriptionFactory;
        public ILogger<ISubscriberState> StateLogger;
        public ISubscriptionRepository SubscriptionRepository;
        public ISubscriberState SubscriberState;
        public ILogger<SubscriptionController> Logger;
        public ISubscriptionService SubscriptionService;
        public SubscriptionController Controller;
            
        public SubscriptionControllerFixture()
        {
            Db = new MockDbContext().Context;
            UserState = new MockUserState();
            RecipeFactory = new RecipeFactory();
            RecipeDTO = new RecipeDTO();
            RecipeServiceDTO = new RecipeServiceDTO();
            RecipeBuilder = new RecipeBuilder(RecipeFactory);
            SubscriptionFactory = new SubscriptionFactory();
            StateLogger = new LoggerFactory().CreateLogger<ISubscriberState>();
            SubscriptionRepository = new SubscriptionRepository(SubscriptionFactory,Db, UserState);
            SubscriberState = new SubscriberState(StateLogger);
            SubscriptionService = new SubscriptionService(SubscriberState, SubscriptionRepository, RecipeBuilder);
            Logger = new LoggerFactory().CreateLogger<SubscriptionController>();
            Controller = new SubscriptionController(Logger, SubscriptionService);
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
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
