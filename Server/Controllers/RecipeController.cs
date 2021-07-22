using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PBC.Shared;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PBC.Server.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly ILogger<RecipeController> _logger;

        public RecipeController(ILogger<RecipeController> logger)
        {
            _logger = logger;
        }

        [HttpPost("NewRecipe")]
        public IActionResult PostNewRecipe(RecipeDTO recipeDTO)
        {
            Console.WriteLine($"Recipe controller has received a Recipe! Recipe Title is {recipeDTO.Title}");
            return Ok(recipeDTO);
        }

        [HttpPost("RecipeURL")]
        public IActionResult PostRecipeUrl(RecipeUrlDTO urlDTO)
        {
            Console.WriteLine($"Recipe controller has received a URL! Recipe URL is {urlDTO.URL}");
            return Ok(urlDTO.URL);
        }

    }
}
