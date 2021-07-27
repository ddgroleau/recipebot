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
            _logger.LogInformation($"New URL {urlDTO.URL} recieved by RecipeController, PostRecipeUrl method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}. ID: {urlDTO.RecipeUrlDtoId}.");
            try
            {
                return _allRecipesScraper.ScrapeRecipe(urlDTO.URL, _recipeDTO);
            }
            catch (Exception)
            {
                _logger.LogError($"Failed to scrape {urlDTO.URL} from AllRecipes.com; RecipeController, PostRecipeUrl method. ID: {urlDTO.RecipeUrlDtoId}");
            }
            return _recipeDTO;
        }

        [HttpPost("Recipe")]
        public IActionResult PostRecipe(RecipeDTO recipeDTO)
        {
            _logger.LogInformation($"RecipeDTO: \"{recipeDTO.Title}\" recieved by RecipeController, PostNewRecipe method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}. ID: {recipeDTO.RecipeDtoId}.");
            try
            {
                _logger.LogInformation($"Processing RecipeDTO: \"{recipeDTO.Title}\" at RecipeController, PostRecipe method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}. ID: {recipeDTO.RecipeDtoId}.");
                // RecipeService (which will perform validation (method), build entity (class), and save entity to repository (class)) (if not built already). Build this deliberately.
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to process RecipeDTO \"{recipeDTO.Title}\" at RecipeController, PostRecipe method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}. ID: {recipeDTO.RecipeDtoId}.", e.Message);
            }
            return Ok(recipeDTO);
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

        [HttpGet("UserRecipes/{UserName}")]
        public IEnumerable<IRecipeDTO> GetUserRecipes(string UserName)
        {
            _logger.LogInformation($"Request for user recipes recieved by RecipeController, GetUserRecipes method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");

            var recipes = new List<IRecipeDTO>
            {
                new RecipeDTO { Title = $"{UserName} Title1", Description = "UserDescription1" },
                new RecipeDTO { Title = $"{UserName} Title2", Description = "UserDescription2" },
                new RecipeDTO { Title = $"{UserName} Title3", Description = "UserDescription3" }
            };

            return recipes;
        }

        [HttpDelete("DeleteRecipe/{recipe}")]
        public IActionResult DeleteRecipe(IRecipeDTO recipe)
        {
            _logger.LogInformation($"Request to delete recipe, ID: {recipe.RecipeDtoId} recieved by RecipeController, DeleteRecipe method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");

            var recipes = new List<IRecipeDTO>
            {
                new RecipeDTO { Title = "Title1", Description = "Description1" },
                new RecipeDTO { Title = "Title2", Description = "Description2" },
                new RecipeDTO { Title = "Title3", Description = "Description3" }
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
