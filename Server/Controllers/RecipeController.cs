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

        [HttpPost("RecipeURL")]
        public IRecipeDTO PostRecipeUrl(RecipeUrlDTO urlDTO)
        {
            _logger.LogInformation($"New URL {urlDTO.URL} was submitted to recipe controller. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
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

        [HttpPost("NewRecipe")]
        public IActionResult PostNewRecipe(RecipeDTO recipeDTO)
        {
            _logger.LogInformation($"New RecipeDTO: \"{recipeDTO.Title}\" was submitted to recipe controller. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            try
            {
                _logger.LogInformation($"Processing RecipeDTO: \"{recipeDTO.Title}\". Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                // RecipeService (which will perform validation (method), build entity (class), and save entity to repository (class)). Build this deliberately.
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to process RecipeDTO \"{recipeDTO.Title}\". Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.", e.Message);
            }
            return Ok(recipeDTO);
        }
    }
}
