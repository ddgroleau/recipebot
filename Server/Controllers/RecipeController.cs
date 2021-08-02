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
        private readonly IRecipeService _recipeService;

        public RecipeController(ILogger<RecipeController> logger, IRecipeDTO recipeDTO, IAllRecipesScraper allRecipesScraper, IRecipeService recipeService)
        {
            _logger = logger;
            _recipeDTO = recipeDTO;
            _allRecipesScraper = allRecipesScraper;
            _recipeService = recipeService;
        }

        [HttpPost("RecipeURL")]
        public IRecipeDTO ProcessRecipeUrl(RecipeUrlDTO urlDTO)
        {
            try
            {
                _logger.LogInformation($"New URL {urlDTO.URL} recieved by RecipeController, PostRecipeUrl method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                return _allRecipesScraper.ScrapeRecipe(urlDTO.URL, _recipeDTO);
            }
            catch (Exception)
            {
                _logger.LogError($"Failed to scrape {urlDTO.URL} from AllRecipes.com; RecipeController, PostRecipeUrl method.");
            }
            return _recipeDTO;
        }

        [HttpPost("Recipe")]
        public IActionResult CreateOrUpdateRecipe(RecipeDTO recipeDTO)
        {
            try
            {
                _recipeService.CreateRecipe(recipeDTO);
                
                _logger.LogInformation($"Processing RecipeDTO: \"{recipeDTO.Title}\" at RecipeController, PostRecipe method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}. ID: {recipeDTO.RecipeDtoId}.");
                
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to process RecipeDTO \"{recipeDTO.Title}\" at RecipeController, PostRecipe method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}. ID: {recipeDTO.RecipeDtoId}.", e.Message);
            }
            return BadRequest();
        }

        [HttpGet("AllRecipes")]
        public IEnumerable<IRecipeDTO> GetAllRecipes()
        {
            _logger.LogInformation($"Request for all recipes recieved by RecipeController, GetAllRecipes method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");

            var recipes = new List<IRecipeDTO>
            {
                new RecipeDTO { Title = "GlobalTitle1", Description = "GlobalDescription1" },
                new RecipeDTO { Title = "GlobalTitle2", Description = "GlobalDescription2" },
                new RecipeDTO { Title = "GlobalTitle3", Description = "GlobalDescription3" },
            };

            return recipes;
        }

        [HttpGet("UserRecipes/{username}")]
        public IEnumerable<IRecipeDTO> GetUserRecipes(string username)
        {
            _logger.LogInformation($"Request for user recipes recieved by RecipeController, GetUserRecipes method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");

            var recipes = new List<IRecipeDTO>
            {
                new RecipeDTO { Title = $"{username} Title1", Description = "UserDescription1" },
                new RecipeDTO { Title = $"{username} Title2", Description = "UserDescription2" },
                new RecipeDTO { Title = $"{username} Title3", Description = "UserDescription3" }
            };

            return recipes;
        }

        [HttpDelete("DeleteRecipe/{recipe}")]
        public IActionResult DeleteRecipe(IRecipeDTO recipe)
        {
            _logger.LogInformation($"Request to delete recipe, ID: {recipe.RecipeDtoId} recieved by RecipeController, DeleteRecipe method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");

            var recipes = new List<IRecipeDTO>
            {
                new RecipeDTO { Title = $"{recipe.Title}", Description = "Description1" },
                new RecipeDTO { Title = $"{recipe.Title}", Description = "Description2" },
                new RecipeDTO { Title = $"{recipe.Title}", Description = "Description3" }
            };
            recipes.Add(recipe);

            recipes.Remove(recipe);

            if (recipes.Contains(recipe))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
