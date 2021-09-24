using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PBC.Shared;
using PBC.Shared.RecipeComponent;
using PBC.Shared.WebScraper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PBC.Server.Controllers
{
    [Authorize]
    [Route("/api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly ILogger<RecipeController> _logger;
        private readonly IRecipeDTO _recipeDTO;
        private readonly IAllRecipesScraper _allRecipesScraper;
        private readonly IRecipeService _recipeService;
        private readonly HttpClient _http;

        public RecipeController(
            ILogger<RecipeController> logger, 
            IRecipeDTO recipeDTO, 
            IAllRecipesScraper 
            allRecipesScraper, 
            IRecipeService recipeService, 
            HttpClient http)
        {
            _logger = logger;
            _recipeDTO = recipeDTO;
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
                return _allRecipesScraper.ScrapeRecipe(urlDTO.URL, _recipeDTO);
            }
            catch (Exception)
            {
                _logger.LogError($"Failed to scrape {urlDTO.URL} from AllRecipes.com; RecipeController, PostRecipeUrl method.");
            }
            return _recipeDTO;
        }

        [HttpPost("recipe")]
        public async Task<IActionResult> CreateOrUpdateRecipe(RecipeDTO recipeDTO)
        {
            try
            {
                _logger.LogInformation($"Processing RecipeDTO: \"{recipeDTO.Title}\" at RecipeController, PostRecipe method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                int createdId = await _recipeService.CreateRecipe(recipeDTO);

                var subscription = await NotifySubscriptionComponent(createdId);
                bool isSubscribed = subscription.Equals(Ok());

                return isSubscribed ? Ok() : UnprocessableEntity();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to process RecipeDTO \"{recipeDTO.Title}\" at RecipeController, PostRecipe method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.", e.Message);
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
                var subscribeAction = await _http.PostAsJsonAsync("/api/subscriptions/subscripe", createdId);
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
