using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PBC.Shared;
using PBC.Shared.RecipeComponent;
using PBC.Shared.WebScraper;
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
        private readonly IRecipeDTO _recipeDTO;
        private readonly IAllRecipesScraper _allRecipesScraper;

        public RecipeController(ILogger<RecipeController> logger, IRecipeDTO recipeDTO, IAllRecipesScraper allRecipesScraper)
        {
            _logger = logger;
            _recipeDTO = recipeDTO;
            _allRecipesScraper = allRecipesScraper;
        }

        [HttpPost("NewRecipe")]
        public IActionResult PostNewRecipe(RecipeDTO recipeDTO)
        {
            Console.WriteLine($"Recipe controller has received a Recipe! Recipe Title is {recipeDTO.Title}");
            return Ok(recipeDTO);
        }

        [HttpPost("RecipeURL")]
        public IRecipeDTO PostRecipeUrl(RecipeUrlDTO urlDTO)
        {
            Console.WriteLine($"Recipe controller has received a URL! Recipe URL is {urlDTO.URL}");
            try
            {
                return _allRecipesScraper.ScrapeRecipe(urlDTO.URL, _recipeDTO);
            }
            catch (Exception)
            {
                _logger.LogError($"Failed to scrape {urlDTO.URL} from AllRecipes.com");
            }
            return _recipeDTO;
        }
    }
}
