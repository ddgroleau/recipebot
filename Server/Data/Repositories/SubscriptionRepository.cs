using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recipebot.Server.Models;
using Recipebot.Shared.Common;
using Recipebot.Shared.RecipeComponent;
using Recipebot.Shared.SubscriptionComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Server.Data.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly IFactory<RecipeSubscription> _subscriptionFactory;
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserState _userState;

        public SubscriptionRepository(IFactory<RecipeSubscription> subscriptionFactory, ApplicationDbContext context, IUserState userState)
        {
            _subscriptionFactory = subscriptionFactory;
            _dbContext = context;
            _userState = userState;
        }

        public async Task Subscribe(int recipeId)
        {
            Recipe recipe = await _dbContext.Recipes.FindAsync(recipeId);
            
            if(recipe != null)
            {
                RecipeSubscription recipeSubscription = BuildSubscription(recipe.RecipeId);
                
                bool previousSubscription = await _dbContext.RecipeSubscriptions
                    .Where(s => s.RecipeId.Equals(recipeSubscription.RecipeId) 
                        && s.ApplicationUserId.Equals(recipeSubscription.ApplicationUserId))
                    .AnyAsync();

                await SaveSubscription(previousSubscription, recipeSubscription);
            }
            return;
        }

        public async Task Unsubscribe(int recipeId)
        {
            var entity = await _dbContext.RecipeSubscriptions.FindAsync(recipeId);
            if(entity != null)
            {
                entity.IsSubscribed = false;
                _dbContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private RecipeSubscription BuildSubscription(int recipeId)
        {
            RecipeSubscription subscription = _subscriptionFactory.Make();

            subscription.RecipeId = recipeId;
            subscription.ApplicationUserId = _userState.GetCurrentUserId();
            subscription.IsSubscribed = true;
            subscription.CreationDate = DateTime.Now;
            subscription.LastModifed = DateTime.Now;
            
            return subscription;
        }

        private async Task SaveSubscription(bool previousSubscription, RecipeSubscription recipeSubscription)
        {
              if (previousSubscription)
                {
                    var entity = await _dbContext.RecipeSubscriptions.FirstOrDefaultAsync(s => s.RecipeId == recipeSubscription.RecipeId &&
                                    s.ApplicationUserId == recipeSubscription.ApplicationUserId);
                    entity.IsSubscribed = true;
                }
                else
                {
                    await _dbContext.RecipeSubscriptions.AddAsync(recipeSubscription);
                }
                await _dbContext.SaveChangesAsync();
        }
    }
}
