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
    public class SearchBarEvent : ISearchBarEvent
    {

        private readonly HttpClient _http;
        private readonly ILogger<ISearchBarEvent> _logger;

        public SearchBarEvent(HttpClient http, ILogger<ISearchBarEvent> logger)
        {
            _http = http;
            _logger = logger;
        }

        public string SearchText { get; set; }
        public IEnumerable<IRecipeDTO> RecipesFound { get; set; } = new List<RecipeDTO>();

        public async Task<IEnumerable<IRecipeDTO>> HandleKeyPress()
        {
            try
            {
                if (!String.IsNullOrEmpty(SearchText))
                {
                   RecipesFound = await _http.GetFromJsonAsync<List<RecipeDTO>>($"/api/Recipe/SearchRecipes/{SearchText}");
                }
            }
            catch (Exception)
            {
                _logger.LogError($"Failure to search recipes on key up at SearchBarEvent. Search text: {SearchText}. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            }

            return RecipesFound;
        }

        public async Task HandleClick()
        {
            try
            {
                if (!String.IsNullOrEmpty(SearchText))
                {
                    _logger.LogInformation($"Submitting a search for \"{SearchText}\" to RecipeController. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                    RecipesFound = await _http.GetFromJsonAsync<List<RecipeDTO>>($"/api/Recipe/SearchRecipes/{SearchText}");
                }
            }
            catch (Exception)
            {
                _logger.LogError($"Failure to search recipes on key up at SearchBarEvent. Search text: {SearchText}. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            }
        }
    }
}
