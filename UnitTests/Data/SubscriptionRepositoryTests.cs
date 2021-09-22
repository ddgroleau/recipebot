using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PBC.Server.Data;
using PBC.Server.Data.Repositories;
using PBC.Server.Models;
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
        public IUserState UserState;
        public MockObject MockObject;
        public ISubscriptionRepository SubscriptionRepository;
        public IFactory<RecipeSubscription> SubscriptionFactory;

        public SubscriptionRepositoryTests()
        {
            Db = new MockDbContext().Context;
            UserState = new MockUserState();
            MockObject = new MockObject();
            SubscriptionFactory = new SubscriptionFactory();
            SubscriptionRepository = new SubscriptionRepository(SubscriptionFactory, Db, UserState);
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task Unsubscribe_WithValidId_ShouldbeTrue()
        {
            var subscription = MockObject.RecipeSubscription;
            await Db.RecipeSubscriptions.AddAsync(subscription);

            await SubscriptionRepository.Unsubscribe(subscription.RecipeSubscriptionId);
            var changedEntity = await Db.RecipeSubscriptions.FindAsync(subscription.RecipeSubscriptionId);
            bool isSubscribed = changedEntity.IsSubscribed;

            Assert.False(isSubscribed);
        }

        [Fact]
        public async Task Unsubscribe_WithInvalidId_ShouldThrow()
        {
            var subscription = MockObject.RecipeSubscription;
            await Db.RecipeSubscriptions.AddAsync(subscription);

            await Assert.ThrowsAsync<InvalidOperationException>(
                async () => await SubscriptionRepository.Unsubscribe(2));
        }

        [Fact]
        public async Task Subscribe_WithValidNewId_ShouldBeTrue()
        {
            var userid = await UserState.CurrentUserIdAsync();

            await Db.Recipes.AddAsync(MockObject.Recipe);
            Db.SaveChanges();

            await SubscriptionRepository.Subscribe(MockObject.Recipe.RecipeId);

            bool isSubscribed = await Db.RecipeSubscriptions
                .Where(x => x.RecipeId.Equals(MockObject.Recipe.RecipeId) 
                    && x.ApplicationUserId.Equals(userid))
                .AnyAsync();

            Assert.True(isSubscribed);
        }

        [Fact]
        public async Task Subscribe_WithValidPreviousId_ShouldBeTrue()
        {
            var userid = await UserState.CurrentUserIdAsync();
            
            await Db.Recipes.AddAsync(MockObject.Recipe); // Add the recipe

            var previousSubscription = MockObject.RecipeSubscription;

            previousSubscription.IsSubscribed = false;
            
            await Db.RecipeSubscriptions.AddAsync(previousSubscription); // Add a subscription for that recipe
            Db.SaveChanges();

            var recipe = MockObject.Recipe;

            await SubscriptionRepository.Subscribe(recipe.RecipeId); // subscribe to that same recipe

            var recipeSubscription = await Db.RecipeSubscriptions
                .Where(x => x.RecipeId.Equals(recipe.RecipeId)
                    && x.ApplicationUserId.Equals(userid))
                .SingleAsync();

            bool isSubscribed = recipeSubscription.IsSubscribed;

            Assert.True(isSubscribed);
        }

        [Fact]
        public async Task Subscribe_WithInvalidId_ShouldBeFalse()
        {
            await SubscriptionRepository.Subscribe(2); // Try to subscribe to a RecipeId that does not exist in the Recipes table.

            var isSubscribed = await Db.RecipeSubscriptions.Where(x => x.RecipeId.Equals(2)).AnyAsync();

            Assert.False(isSubscribed);
        }


    }
}
