using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PBC.Shared;
using PBC.Shared.RecipeComponent;
using PBC.Shared.SubscriptionComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PBC.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ILogger<SubscriptionController> _logger;
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ILogger<SubscriptionController> logger, ISubscriptionService subscriptionService)
        {
            _logger = logger;
            _subscriptionService = subscriptionService;
        }

        [HttpGet("Subscriptions/{userId}")]
        public IEnumerable<IRecipeDTO> GetUserRecipes(int userId)
        {
            _logger.LogInformation($"Request for user recipes recieved by RecipeController, GetUserRecipes method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");

            //return _subscriptionService.GetUserRecipes(userId);
            var recipes = new List<IRecipeDTO> //STUB
            {
                new RecipeDTO { RecipeId = 11, Title =  $"Recipe11", Description = "Description11", RecipeType="Breakfast", Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 12, Title =  $"Recipe12", Description = "Description12", RecipeType="Breakfast", Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 13, Title = $"Recipe13", Description = "Description13",  RecipeType="Breakfast", Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 14, Title = $"Recipe14", Description = "Description14",  RecipeType="Breakfast", Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 15, Title = $"Recipe15", Description = "Description15",  RecipeType="Breakfast", Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 16, Title = $"Recipe16", Description = "Description16",  RecipeType="Lunch",     Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 17, Title = $"Recipe17", Description = "Description17",  RecipeType="Lunch",     Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 18, Title = $"Recipe18", Description = "Description18",  RecipeType="Lunch",     Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 19, Title = $"Recipe19", Description = "Description19",  RecipeType="Dinner",    Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 20, Title = $"Recipe20", Description = "Description20",  RecipeType="Dinner",    Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 21, Title = $"Recipe21", Description = "Description21",  RecipeType="Dinner",    Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 22, Title = $"Recipe22", Description = "Description22",  RecipeType="Dinner",    Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
            };
            return recipes;
        }

        [HttpPost("Subscribe")]
        public IActionResult Subscribe(int recipeId)
        {
            try
            {
                _logger.LogInformation($"Recieved new subscription at SubscriptionController, Subscribe method,. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                _subscriptionService.CreateSubscription(recipeId);
                return Ok();
            }
            catch (Exception)
            {
                _logger.LogError($"Could not subscribe to recipe id: {recipeId} at SubscriptionController, Subscribe method,. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            }
            return BadRequest();
        }

        [HttpPost("Unsubscribe")]
        public IActionResult Unsubscribe(int recipeId)
        {
            try
            {
            _logger.LogInformation($"Recieved unsubscribe request at SubscriptionController, Unsubscribe method,. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            _subscriptionService.UpdateSubscription(recipeId);
            return Ok();
            }
            catch (Exception)
            {
                _logger.LogError($"Could not unsubscribe from recipe id: {recipeId} at SubscriptionController, Unsubscribe method,. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            }
            return BadRequest();
        }
    }
}
