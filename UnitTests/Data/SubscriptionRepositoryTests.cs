using IdentityServer4.EntityFramework.Options;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PBC.Server.Data;
using PBC.Server.Data.Repositories;
using PBC.Shared.Common;
using PBC.Shared.RecipeComponent;
using PBC.Shared.SubscriptionComponent;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.MockObjects;
using Xunit;

namespace UnitTests.Data
{
    public class SubscriptionRepositoryTests : IDisposable
    {
        public ApplicationDbContext Db;
        public MockObject MockObject;
        public ISubscriptionRepository SubscriptionRepository;
        public IFactory<RecipeSubscription> SubscriptionFactory;

        public SubscriptionRepositoryTests()
        {
            Db = new MockDbContext().Context;
            MockObject = new MockObject();
            SubscriptionFactory = new SubscriptionFactory(new RecipeSubscription(), new Recipe());
            SubscriptionRepository = new SubscriptionRepository(SubscriptionFactory, Db);
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        [Fact]
        public async Task Unsubscribe()
        {
            var subscription = MockObject.RecipeSubscription;

            await Db.RecipeSubscriptions.AddAsync(subscription);
            
            await SubscriptionRepository.Unsubscribe(subscription.RecipeSubscriptionId);

            var changedEntity = await Db.RecipeSubscriptions.FindAsync(subscription.RecipeSubscriptionId);

            bool isSubscribed = changedEntity.IsSubscribed;

            Assert.False(isSubscribed);
        }
    }
}
