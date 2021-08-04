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
    public class EditRecipeEvent : IEditRecipeEvent
    {
        private readonly ILogger<IRecipeDTO> _logger;
        private readonly HttpClient _http;

        public EditRecipeEvent(ILogger<IRecipeDTO> logger, HttpClient http)
        {
            _logger = logger;
            _http = http;
        }
        
        public async Task<IRecipeDTO> HandleValidSubmit(ILazor lazor, IRecipeDTO recipeDTO)
        {
            try
            {
                bool recipeDTOIsValid = lazor.IsObjectValid(recipeDTO);

                if (recipeDTOIsValid)
                {
                    lazor.SetLoadingStatus(true);

                    var response = await _http.PostAsJsonAsync("/api/Recipe/Recipe", recipeDTO);
                    if (response.IsSuccessStatusCode)
                    {
                        lazor.SetSuccessStatus(true);
                        _logger.LogInformation($"Successfully posted recipe \"{recipeDTO.Title}\" to RecipeController. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                        recipeDTO.ResetRecipe();
                        return recipeDTO;
                    }
                    _logger.LogError($"Failed to post recipe \"{recipeDTO.Title}\" to RecipeController. Server responded with {response.StatusCode}. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                    lazor.SetErrorMessage($"Sorry, something went wrong. Server responded with {response.StatusCode}.");
                    return recipeDTO;
                }
            }
            catch (Exception err)
            {
                lazor.SetErrorMessage($"Sorry, something went wrong. Error {lazor.ErrorMessage}.");
                _logger.LogError($"Exception occured when posting recipe \"{recipeDTO.Title}\" to RecipeController. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.", err.Message);
            }
            lazor.SetLoadingStatus(false);
            return recipeDTO;
        }
    }
}
