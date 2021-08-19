using Microsoft.Extensions.Logging;
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
        private Dictionary<int, bool> RecipeSubscriptions { get; set; } = new();
        private bool SubscriptionsHaveChanged { get; set; }
        private ILogger<ISubscriberState> _logger;

        public SubscriberState(ILogger<ISubscriberState> logger)
        {
            _logger = logger;
        }
        public void UpdateState(int id)
        {
            bool isSubscribed = RecipeSubscriptions.TryGetValue(id,out _);
            if(isSubscribed)
            {
                RecipeSubscriptions[id] = !RecipeSubscriptions[id];
            }
            else
            {
                RecipeSubscriptions.Add(id, true);
            }
            SubscriptionsHaveChanged = true;
        }

        public async Task<Dictionary<int, bool>> GetRecipeSubscriptions()
        {
            try
            {
                if (SubscriptionsHaveChanged)
                {
                    RecipeSubscriptions = await Http.GetFromJsonAsync<Dictionary<int, bool>>("https://localhost:4001/api/Subscription/Subscriptions");
                    SubscriptionsHaveChanged = false;
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
