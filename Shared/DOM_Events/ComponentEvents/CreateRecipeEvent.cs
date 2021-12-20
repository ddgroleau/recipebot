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
    public class CreateRecipeEvent : ICreateRecipeEvent
    {
        public IRecipeDTO RecipeDTO { get; set; }
        public IRecipeUrlDTO RecipeUrlDTO { get; set; }
        public ILazor Lazor { get; set; }

        private readonly ILogger<IRecipeUrlDTO> _logger;
        private readonly HttpClient _http;

        public CreateRecipeEvent(ILazor e, IRecipeUrlDTO recipeUrlDTO, IRecipeDTO recipeDTO, ILogger<IRecipeUrlDTO> logger, HttpClient http)
        {
            Lazor = e;
            RecipeUrlDTO = recipeUrlDTO;
            _logger = logger;
            _http = http;
            RecipeDTO = recipeDTO;
        }

        public async Task<IRecipeDTO> HandleSubmit()
        {
            try
            {
                Lazor.SetLoadingStatus(true);
                Lazor.SetErrorMessage(null);

                bool urlIsValid = Lazor.IsPropertyValid(RecipeUrlDTO, "URL", RecipeUrlDTO.URL);

                if (urlIsValid)
                {
                    if (string.IsNullOrEmpty(RecipeUrlDTO.URL))
                    {
                        Lazor.Toggle();
                    }
                    else
                    {
                        var response = await _http.PostAsJsonAsync("/api/recipe/recipe-url", RecipeUrlDTO);
                        var scrapedRecipe = await response.Content.ReadFromJsonAsync<RecipeDTO>();
                        RecipeDTO = scrapedRecipe;
                        Lazor.Toggle();
                        _logger.LogInformation($"URL: {RecipeUrlDTO.URL} sucessfully submitted to RecipeController. New RecipeDTO \"{RecipeDTO.Title}\" was scraped and received. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                    }
                }
            }
            catch (NullReferenceException)
            {
                Lazor.Toggle();
            }
            catch (Exception err)
            {
                _logger.LogError($"Failed to post RecipeUrlDTO to RecipeController. URL: {RecipeUrlDTO.URL}. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.", err);
            }

            Lazor.SetLoadingStatus(false);
            return RecipeDTO;
        }

        public void ResetView()
        {
            Lazor.SetLoadingStatus(false);
            Lazor.SetSuccessStatus(false);
            Lazor.Toggle();
            RecipeUrlDTO.URL = null;
        }
    }
}
