using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public async Task<IEnumerable<int>> GetSubscriptions()
        {
            return new List<int>()
            {
                1,2,3,4,5,6,7,8,9,10,11,12
            };
        }

        [HttpPost("NewSubscription")]
        public IActionResult CreateSubscription()
        {
            _logger.LogInformation($"Recieved new subscription at SubscriptionController, CreateSubscription method,. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            return Ok();
        }
    }
}
