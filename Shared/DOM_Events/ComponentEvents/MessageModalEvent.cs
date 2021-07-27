using Microsoft.Extensions.Logging;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.DOM_Events.ComponentEvents
{
    public class MessageModalEvent : IMessageModalEvent
    {
        private readonly HttpClient _http;
        private readonly ILogger<IRecipeDTO> _logger;

        public MessageModalEvent(HttpClient http, ILogger<IRecipeDTO> logger)
        {
            _http = http;
            _logger = logger;
        }
        public void HandleClick(ILazor lazor)
        {
            lazor.Hide();
        }

        public async void DeleteRecipe(IRecipeDTO recipeDTO, ILazor lazor)
        {
            try
            {
                var response = await _http.DeleteAsync($"/api/Recipe/DeleteRecipe/{recipeDTO}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to delete RecipeDTO. ID: {recipeDTO.RecipeDtoId} at MessageModalEvent, DeleteRecipe method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.", e.Message);
            }

            lazor.Hide();
        }
    }
}
