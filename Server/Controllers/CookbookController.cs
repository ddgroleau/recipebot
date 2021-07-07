using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PBC.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PBC.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CookbookController : ControllerBase
    {
   

        private readonly ILogger<CookbookController> _logger;

        public CookbookController(ILogger<CookbookController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Cookbook> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Cookbook
            {
                id = rng.Next(1, 11)
            }) 
            .ToArray();
        }
    }
}
