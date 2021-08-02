using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.DOM_Events.ComponentEvents
{
    public class SearchBarEvent : ISearchBarEvent
    {
        private readonly HttpClient _http;

        public SearchBarEvent(HttpClient http)
        {
            _http = http;
        }

        public string SearchText { get; set; }

        public List<IRecipeDTO> RecipesFound { get; set; } = new List<IRecipeDTO>();

        public async Task<List<IRecipeDTO>> HandleKeyPress()
        {
            //var response = await _http.PostAsJsonAsync("/api/Recipe/RecipeURL", SearchText);
            //var recipeTitles = await response.Content.ReadFromJsonAsync<RecipeDTO>();
            var recipe = new RecipeDTO
            {
                Title = "Test Description"
            };

            var results = new List<IRecipeDTO>();
            results.Add(recipe);
            results.Add(recipe);
            results.Add(recipe);

            RecipesFound = results;
            return results;
        }

        public async Task HandleClick()
        {
            // To Do
        }
    }
}
