using Microsoft.Extensions.Logging;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.DOM_Events.ComponentEvents
{
    public class CookbookTableEvent : ICookBookTableEvent
    {
        public IRecipeDTO RecipeDTO { get; set; }
        public IEnumerable<IRecipeDTO> RetrievedRecipes { get; set; }
        public ILazor Lazor { get; set; }
        public string Message { get; set; }
        public Dictionary<int, bool> Loading { get; set; } = new();

        private readonly ILogger<IRecipeDTO> _logger;
        private readonly HttpClient _http;

        public CookbookTableEvent(ILazor lazor, IRecipeDTO recipeDTO, ILogger<IRecipeDTO> logger, HttpClient http, IEnumerable<IRecipeDTO> userRecipes)
        {
            Lazor = lazor;
            _logger = logger;
            _http = http;
            RecipeDTO = recipeDTO;
            RetrievedRecipes = userRecipes;
        }

        public async Task<IEnumerable<IRecipeDTO>> GetUserRecipesAsync()
        {
            try
            {
                RetrievedRecipes = await _http.GetFromJsonAsync<List<RecipeDTO>>($"https://localhost:4001/api/recipe/user-recipes");
            }
            catch (Exception e)
            {
                _logger.LogError($"Could not get recipes from recipeController. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}. Error: {e.Message}", e);
            }

            return RetrievedRecipes;
        }


        public void HandleUpdate()
        {
            Lazor.Toggle();
        }

        public void HandleDetails()
        {
            Message = $"{RecipeDTO.Title}";
            Lazor.Show();
        }
       
        public async Task<bool> HandleSubscribe()
        {
            try
            {
                var response = await _http.PostAsJsonAsync("/api/subscription/subscribe", RecipeDTO.RecipeId);
                Lazor.SetSuccessStatus(response.IsSuccessStatusCode);
            }
            catch (Exception e)
            {
                _logger.LogError($"Could not post new subscription SubscriptionController at CookbookTableEvent, HandleSubscribe method,. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}. Error: {e.Message}", e);
            }
            Loading.Remove(RecipeDTO.RecipeId);
            return Lazor.IsSuccess;
        }

        public async Task<bool> HandleUnsubscribe()
        {
            try
            {
                var response = await _http.PostAsJsonAsync("/api/subscription/unsubscribe", RecipeDTO.RecipeId);
                Lazor.SetSuccessStatus(response.IsSuccessStatusCode);
            }
            catch (Exception e)
            {
                _logger.LogError($"Could not post new subscription SubscriptionController at CookbookTableEvent, HandleSubscribe method,. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}. Error: {e.Message}", e);
            }
            Loading.Remove(RecipeDTO.RecipeId);
            return Lazor.IsSuccess;
        }
    }
}
