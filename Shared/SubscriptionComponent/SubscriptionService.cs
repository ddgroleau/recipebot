using PBC.Shared.Common;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.SubscriptionComponent
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriberState _subscriberState;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IBuilder<IRecipeServiceDTO, IRecipeDTO> _recipeBuilder;

        public SubscriptionService(ISubscriberState subscriberState, ISubscriptionRepository subscriptionRepository, IBuilder<IRecipeServiceDTO, IRecipeDTO> recipeBuilder)
        {
            _subscriberState = subscriberState;
            _subscriptionRepository = subscriptionRepository;
            _recipeBuilder = recipeBuilder;
        }
        public void CreateSubscription(int recipeId)
        {
            _subscriberState.UpdateState();
            _subscriptionRepository.CreateSubscription(recipeId);
        }

        public void UpdateSubscription(int recipeId)
        {
            _subscriberState.UpdateState();
            _subscriptionRepository.UpdateSubscription(recipeId);
        }

        public IEnumerable<IRecipeDTO> GetUserRecipes(int userId)
        {
            List<IRecipeDTO> userRecipes = new List<IRecipeDTO>();
            var subscriptions = _subscriptionRepository.GetUserRecipes(userId);
            foreach(var recipe in subscriptions)
            {
                var recipeDTO =_recipeBuilder.Build(recipe.Recipe);
                userRecipes.Add(recipeDTO);
            }
            return userRecipes;
        }
    }
}
