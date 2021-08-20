using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        [HttpGet("Subscriptions")]
        public async Task<Dictionary<int, bool>> GetSubscriptions()
        {
            await Task.Delay(100); // Remove this once implemented
            return new Dictionary<int, bool>()
            {
                {  1,true },
                {  2,true },
                {  3,true },
                {  4,true },
                {  5,true },
                {  6,true },
                {  7,true },
                {  8,true },
                {  9,true },
                { 10,true },
                { 11,true },
                { 12,true },
            };
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
