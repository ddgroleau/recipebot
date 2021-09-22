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
        public ILazor Lazor { get; set; }

        private readonly HttpClient _http;
        private readonly ILogger<ISearchBarEvent> _logger;

        public SearchBarEvent(HttpClient http, ILogger<ISearchBarEvent> logger, ILazor lazor)
        {
            _http = http;
            _logger = logger;
            Lazor = lazor;
        }

        public string SearchText { get; set; }
        public IEnumerable<IRecipeDTO> SearchResults { get; set; } = new List<RecipeDTO>();

        public async Task<IEnumerable<IRecipeDTO>> HandleKeyPress()
        {
            await SearchRecipes();

            return SearchResults;
        }

        public async Task HandleClick()
        {
            await SearchRecipes();

            Lazor.Toggle();
        }

        private async Task SearchRecipes()
        {
            try
            {
                if (!String.IsNullOrEmpty(SearchText))
                {
                    SearchResults = await _http.GetFromJsonAsync<List<RecipeDTO>>($"/api/recipe/search/{SearchText}");
                }
            }
            catch (Exception)
            {
                _logger.LogError($"Failure to search recipes at SearchBarEvent. Search text: {SearchText}. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            }
        }
    }
}
