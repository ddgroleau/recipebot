using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PBC.Shared;
using PBC.Shared.ListComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PBC.Server.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly ILogger<ListController> _logger;

        public ListController(ILogger<ListController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("Day")]
        public IListDayDTO GenerateRandomDay()
        {
            _logger.LogInformation($"Received request for Random Day at ListController, GenerateRandomDay method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");

            return new ListDayDTO
            {
                ListId = 1,
                Date = new DateTime().Date,
                Breakfast = new RecipeDTO
                {
                    Title = "breakfast recipe"
                },
                Lunch = new RecipeDTO
                {
                    Title = "lunch recipe"
                },
                Dinner = new RecipeDTO
                {
                    Title = "dinner recipe"
                },
            };
    }
}
}
