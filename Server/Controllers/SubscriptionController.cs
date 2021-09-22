using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
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

        [HttpPost("subscribe")]
        public IActionResult Subscribe(int recipeId)
        {
            try
            {
                _logger.LogInformation($"Recieved new subscription at SubscriptionController, Subscribe method,. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                _subscriptionService.Subscribe(recipeId);
                return Ok();
            }
            catch (Exception)
            {
                _logger.LogError($"Could not subscribe to recipe id: {recipeId} at SubscriptionController, Subscribe method,. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            }
            return BadRequest();
        }

        [HttpPost("unsubscribe")]
        public IActionResult Unsubscribe(int recipeId)
        {
            try
            {
            _logger.LogInformation($"Recieved unsubscribe request at SubscriptionController, Unsubscribe method,. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            _subscriptionService.Unsubscribe(recipeId);
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
