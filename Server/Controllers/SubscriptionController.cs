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

        public SubscriptionController(ILogger<SubscriptionController> logger)
        {
            _logger = logger;
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
        public IActionResult CreateSubscription(int id)
        {
            _logger.LogInformation($"Recieved new subscription at SubscriptionController, CreateSubscription method,. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            // SubscriptionService -
                // _state.UpdateState(id);
                // _repository.InsertOne?
            return Ok();
        }

        [HttpPost("Unsubscribe")]
        public IActionResult Unsubscribe(int id)
        {
            _logger.LogInformation($"Recieved unsubscribe request at SubscriptionController, Unsubscribe method,. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            // SubscriptionService -
            // _state.UpdateState(id);
            // _repository.InsertOne?
            return Ok();
        }
    }
}
