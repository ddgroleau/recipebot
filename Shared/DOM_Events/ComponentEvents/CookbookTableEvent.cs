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
        private readonly ILogger<IRecipeDTO> _logger;
        private readonly HttpClient _http;

        
        public CookbookTableEvent(ILazor lazor, IRecipeDTO recipeDTO, ILogger<IRecipeDTO> logger, HttpClient http, IEnumerable<IRecipeDTO> retrievedRecipes)
        {
            Lazor = lazor;
            _logger = logger;
            _http = http;
            RecipeDTO = recipeDTO;
            RetrievedRecipes = retrievedRecipes;
        }

        public async Task<IEnumerable<IRecipeDTO>> GetRecipesAsync(bool isUserCookbook, string userName)
        {
            IEnumerable<IRecipeDTO> retrievedRecipes = new List<IRecipeDTO>();

            try
            {
                if (isUserCookbook)
                {
                    retrievedRecipes = await _http.GetFromJsonAsync<IEnumerable<RecipeDTO>>($"/api/Recipe/UserRecipes/{userName}");
                }
                else
                {
                    retrievedRecipes = await _http.GetFromJsonAsync<IEnumerable<RecipeDTO>>("/api/Recipe/AllRecipes");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Could not get recipes from recipeController. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}. Error: {e.Message}", e);
            }

            return retrievedRecipes;
        }


        public void HandleClick()
        {
            Lazor.Toggle();
        }

        public void HandleDelete()
        {
            Lazor.Show();
        }

        public void HandleDetails()
        {
            Lazor.Show();
        }

    }
}
