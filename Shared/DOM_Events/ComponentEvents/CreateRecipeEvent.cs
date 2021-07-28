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
                Lazor.Loading = true;
                Lazor.ErrorMessage = null;

                bool urlIsValid = Lazor.IsPropertyValid(RecipeUrlDTO, "URL", RecipeUrlDTO.URL);

                if (urlIsValid)
                {
                    if (string.IsNullOrEmpty(RecipeUrlDTO.URL))
                    {
                        Lazor.Toggle();
                    }
                    else
                    { 
                    var response = await _http.PostAsJsonAsync("/api/Recipe/RecipeURL", RecipeUrlDTO);
                    var scrapedRecipe = await response.Content.ReadFromJsonAsync<RecipeDTO>();
                    RecipeDTO = scrapedRecipe;
                    Lazor.Toggle();
                    _logger.LogInformation($"URL: {RecipeUrlDTO.URL} sucessfully submitted to RecipeController. New RecipeDTO \"{RecipeDTO.Title}\" was scraped and received. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}. ID: {RecipeUrlDTO.RecipeUrlDtoId}.");
                    }
                }
            }
            catch (NullReferenceException)
            {
                Lazor.Toggle();
            }
            catch (Exception err)
            {
                _logger.LogError($"Failed to post RecipeUrlDTO to RecipeController. URL: {RecipeUrlDTO.URL}. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}. ID: {RecipeUrlDTO.RecipeUrlDtoId}.", err);
            }
        
            Lazor.Loading = false;
            return RecipeDTO;
        }

    public void ResetView()
    {
        Lazor.Loading = false;
        Lazor.isSuccess = false;
        Lazor.Toggle();
        RecipeDTO.ResetRecipe();
        RecipeUrlDTO.ResetURL();
    }
}
}
