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
    public class CatalogController : ControllerBase
    {
   

        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ILogger<CatalogController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Catalog> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Catalog
            {
                id = rng.Next(1, 11)
            }) 
            .ToArray();
        }
    }
}
