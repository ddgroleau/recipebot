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
        private HttpClient Http = new();
        private IEnumerable<IRecipeDTO> RecipeSubscriptions { get; set; } = new List<IRecipeDTO>();
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

        public async Task<IEnumerable<IRecipeDTO>> GetRecipeSubscriptions(int userId)
        {
            try
            {
                if (SubscriptionsHaveChanged)
                {
                    RecipeSubscriptions = await Http.GetFromJsonAsync<List<RecipeDTO>>($"https://localhost:4001/api/Subscription/Subscriptions/{userId}");
                    UpdateState();
                    return RecipeSubscriptions;
                }
            }
            catch (Exception)
            {
                _logger.LogError($"SubscriberState failed to update its RecipeSubscriptions property. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            }
            return RecipeSubscriptions;
        }
    }
}
