using Microsoft.Extensions.Logging;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.SubscriptionComponent
{
    public class SubscriberState : ISubscriberState
    {
        private HttpClient Http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(1)
        };
        private IEnumerable<IRecipeDTO> UserRecipes { get; set; } = new List<IRecipeDTO>();
        private bool SubscriptionsHaveChanged { get; set; } = true;
        private ILogger<ISubscriberState> _logger;

        public SubscriberState(ILogger<ISubscriberState> logger)
        {
            _logger = logger;
        }
        public bool UpdateState()
        {
            SubscriptionsHaveChanged = !SubscriptionsHaveChanged;
            return SubscriptionsHaveChanged;
        }

        public async Task<IEnumerable<IRecipeDTO>> GetUserRecipes()
        {
            try
            {
                if (SubscriptionsHaveChanged)
                {
                    UserRecipes = await Http.GetFromJsonAsync<List<RecipeDTO>>($"https://localhost:4001/api/recipe/user-recipes");
                    UpdateState();
                    return UserRecipes;
                }
            }
            catch (Exception)
            {
                _logger.LogError($"SubscriberState failed to update its RecipeSubscriptions property. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            }
            return UserRecipes;
        }
    }
}
