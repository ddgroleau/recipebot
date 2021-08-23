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
        public async Task<IEnumerable<IRecipeDTO>> GetSubscriptions(int userId)
        {
            _logger.LogInformation($"Request for user recipes recieved by RecipeController, GetUserRecipes method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");

            var recipes = new List<IRecipeDTO>
            {
                new RecipeDTO { RecipeId = 1, Title = $"Recipe1",  Description = "Description1",  RecipeType="Breakfast", Ingredients ={ "Salt"}, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 2, Title = $"Recipe2",  Description = "Description2",  RecipeType="Breakfast", Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 3, Title = $"Recipe3",  Description = "Description3",  RecipeType="Breakfast", Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 4, Title = $"Recipe4",  Description = "Description4",  RecipeType="Breakfast", Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 5, Title = $"Recipe5",  Description = "Description5",  RecipeType="Lunch",     Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 6, Title = $"Recipe6",  Description = "Description6",  RecipeType="Lunch",     Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 7, Title = $"Recipe7",  Description = "Description7",  RecipeType="Lunch",     Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 8, Title = $"Recipe8",  Description = "Description8",  RecipeType="Lunch",     Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 9, Title = $"Recipe9",  Description = "Description9",  RecipeType="Dinner",    Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 10,Title = $"Recipe10", Description = "Description10", RecipeType="Dinner",    Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 11,Title = $"Recipe11", Description = "Description11", RecipeType="Dinner",    Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeDTO { RecipeId = 12,Title = $"Recipe12", Description = "Description12", RecipeType="Dinner",    Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
            };
            return recipes;
        }

        [HttpPost("Subscribe")]
        public IActionResult Subscribe(int id)
        {
            try
            {
                _logger.LogInformation($"Recieved new subscription at SubscriptionController, Subscribe method,. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                _subscriptionService.CreateSubscription(id);
                return Ok();
            }
            catch (Exception)
            {
                _logger.LogError($"Could not subscribe to recipe id: {id} at SubscriptionController, Subscribe method,. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            }
            return BadRequest();
        }

        [HttpPost("Unsubscribe")]
        public IActionResult Unsubscribe(int id)
        {
            try
            {
            _logger.LogInformation($"Recieved unsubscribe request at SubscriptionController, Unsubscribe method,. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            _subscriptionService.UpdateSubscription(id);
            return Ok();
            }
            catch (Exception)
            {
                _logger.LogError($"Could not unsubscribe from recipe id: {id} at SubscriptionController, Unsubscribe method,. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            }
            return BadRequest();
        }
    }
}
