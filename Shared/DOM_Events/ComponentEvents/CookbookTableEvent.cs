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
        public bool IsDeleteAction { get; set; }

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
            try
            {
                if (isUserCookbook)
                {
                    RetrievedRecipes = await _http.GetFromJsonAsync<List<RecipeDTO>>($"/api/Recipe/UserRecipes/{userName}");
                }
                else
                {
                    RetrievedRecipes = await _http.GetFromJsonAsync<List<RecipeDTO>>("/api/Recipe/AllRecipes");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Could not get recipes from recipeController. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}. Error: {e.Message}", e);
            }

            return RetrievedRecipes;
        }


        public void HandleClick()
        {
            Lazor.Toggle();
        }

        public void HandleDelete()
        {
            IsDeleteAction = true;
            Message = $"Are you sure you want to delete \"{RecipeDTO.Title}\"?";
            Lazor.Show();
        }

        public void HandleDetails()
        {
            IsDeleteAction = false;
            Message = $"{RecipeDTO.Title}";
            Lazor.Show();
        }

    }
}
