using Microsoft.Extensions.Logging;
using Recipebot.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.DOM_Events.ComponentEvents
{
    public class GeneratedDayEvent : IGeneratedDayEvent
    {
        private readonly IRecipeDTO _RecipeDTO;
        private readonly HttpClient _http;
        private readonly ILogger<GeneratedDayEvent> _logger;
        public GeneratedDayEvent(IRecipeDTO RecipeDTO, HttpClient http, ILogger<GeneratedDayEvent> logger)
        {
            _http = http;
            _RecipeDTO = RecipeDTO;
            _logger = logger;
        }
        public Dictionary<string, bool> Loading { get; set; } = new Dictionary<string, bool>() {
                { "Breakfast", false },
                { "Lunch", false },
                { "Dinner", false },
                { "AllThree", false }
            };
            
        // "↻" is a refresh icon.
        public Dictionary<string, string> RefreshSymbol { get; set; } = new Dictionary<string, string>() {
                { "Breakfast", "↻" },
                { "Lunch", "↻" },
                { "Dinner", "↻" },
                { "AllThree", "↻" }
            };

        public async Task<IRecipeDTO> RegenerateRecipe(string recipeType)
        {
            var newRecipe = _RecipeDTO;
            Loading[recipeType] = true;
            RefreshSymbol[recipeType] = null;
            try
            {
                newRecipe = await _http.GetFromJsonAsync<RecipeDTO>($"/api/list/random-recipe/{recipeType}");
            }
            catch (Exception)
            {
                _logger.LogError($"Could not regenerate recipe at GeneratedDayEvent, RegenerateRecipe method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}");
            }
            Loading[recipeType] = false;
            RefreshSymbol[recipeType] = "↻";
            return newRecipe;
        }
    }
}
