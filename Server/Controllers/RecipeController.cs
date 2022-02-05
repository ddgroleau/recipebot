using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Recipebot.Shared;
using Recipebot.Shared.RecipeComponent;
using Recipebot.Shared.WebScraper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Recipebot.Server.Controllers
{
    [Authorize]
    [Route("/api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly ILogger<RecipeController> _logger;
        private readonly IRecipeDTO _RecipeDTO;
        private readonly IRecipeScraper _allRecipesScraper;
        private readonly IRecipeService _recipeService;
        private readonly HttpClient _http;

        public RecipeController(
            ILogger<RecipeController> logger, 
            IRecipeDTO RecipeDTO, 
            IRecipeScraper 
            allRecipesScraper, 
            IRecipeService recipeService, 
            HttpClient http)
        {
            _logger = logger;
            _RecipeDTO = RecipeDTO;
            _allRecipesScraper = allRecipesScraper;
            _recipeService = recipeService;
            _http = http;
        }

        [HttpPost("recipe-url")]
        public IRecipeDTO ProcessRecipeUrl(RecipeUrlDTO urlDTO)
        {
            try
            {
                _logger.LogInformation($"New URL {urlDTO.URL} recieved by RecipeController, PostRecipeUrl method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                return _allRecipesScraper.ScrapeRecipe(urlDTO.URL, _RecipeDTO);
            }
            catch (Exception)
            {
                _logger.LogError($"Failed to scrape {urlDTO.URL} from AllRecipes.com; RecipeController, PostRecipeUrl method.");
            }
            return _RecipeDTO;
        }

        [HttpPost("recipe")]
        public async Task<IActionResult> CreateOrUpdateRecipe(RecipeDTO RecipeDTO)
        {
            try
            {
                _logger.LogInformation($"Processing RecipeDTO: \"{RecipeDTO.Title}\" at RecipeController, PostRecipe method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                int createdId = await _recipeService.CreateRecipe(RecipeDTO);

                var subscription = await NotifySubscriptionComponent(createdId);

                return subscription;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to process RecipeDTO \"{RecipeDTO.Title}\" at RecipeController, CreateOrUpdateRecipe method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.", e.Message);
            }
            return BadRequest();
        }  

        [HttpGet("search/{searchText}")]
        public IEnumerable<IRecipeDTO> SearchRecipes(string searchText)
        {
            IEnumerable<IRecipeDTO> recipes = new List<IRecipeDTO>();
            _logger.LogInformation($"Search request received by RecipeController, SearchRecipes method. Search text: {searchText}. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            try
            {
                recipes =  _recipeService.SearchRecipes(searchText);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to retrieve recipes at RecipeController, SearchRecipes method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.", e.Message);
            }
            return recipes;
        }

        [HttpGet("user-recipes")]
        public async Task<IEnumerable<IRecipeDTO>> GetUserRecipes()
        {
            _logger.LogInformation($"Request for user recipes recieved by RecipeController, GetUserRecipes method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");

            try
            {
                return await _recipeService.GetUserRecipes();
            }
            catch (Exception)
            {
                _logger.LogError($"Could retrieve user recipe list at RecipeController, GetUserRecipes method,. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            };
            return new List<IRecipeDTO>();
        }
    
        private async Task<IActionResult> NotifySubscriptionComponent(int createdId)
        {
            try
            {
                var subscribeAction = await _http.PostAsJsonAsync("https://localhost:4001/api/subscription/subscribe", createdId);
                bool subscriptionCreated = subscribeAction.IsSuccessStatusCode;
                if (subscriptionCreated)
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to notify subscription component of new recipe, ID: {createdId} at RecipeController, NotifySubscriptionComponent method. " +
                    $"Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.", e.Message);
            }
            return UnprocessableEntity();
        }
    
    }
}
