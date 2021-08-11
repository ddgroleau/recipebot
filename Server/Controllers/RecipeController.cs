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
                if (recipeDTO.RecipeId.Equals(0))
                {
                    _recipeService.CreateRecipe(recipeDTO);
                    _logger.LogInformation($"Processing RecipeDTO: \"{recipeDTO.Title}\" at RecipeController, PostRecipe method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                }
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to process RecipeDTO \"{recipeDTO.Title}\" at RecipeController, PostRecipe method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.", e.Message);
            }
            return BadRequest();
        }

        [HttpGet("UserRecipes/{username}")]
        public IEnumerable<IRecipeDTO> GetUserRecipes(string username)
        {
            _logger.LogInformation($"Request for user recipes recieved by RecipeController, GetUserRecipes method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");

            var recipes = new List<IRecipeDTO>
            {
               new RecipeDTO { Title = $"Recipe1", Description = "Description1", RecipeType="Breakfast" },
               new RecipeDTO { Title = $"Recipe2", Description = "Description2", RecipeType="Breakfast" },
               new RecipeDTO { Title = $"Recipe3", Description = "Description3", RecipeType="Breakfast" },
               new RecipeDTO { Title = $"Recipe4", Description = "Description4", RecipeType="Breakfast" },
                new RecipeDTO { Title = $"Recipe5", Description = "Description5", RecipeType="Lunch"},
                new RecipeDTO { Title = $"Recipe6", Description = "Description6", RecipeType="Lunch"},
                new RecipeDTO { Title = $"Recipe7", Description = "Description7", RecipeType="Lunch"},
                new RecipeDTO { Title = $"Recipe8", Description = "Description8", RecipeType="Lunch"},
                new RecipeDTO { Title = $"Recipe9", Description = "Description9", RecipeType="Dinner"},
                new RecipeDTO { Title = $"Recipe10", Description = "Description10", RecipeType="Dinner"},
                new RecipeDTO { Title = $"Recipe11", Description = "Description11", RecipeType="Dinner"},
                new RecipeDTO { Title = $"Recipe12", Description = "Description12", RecipeType="Dinner"},
            };
            return recipes;
        }

        [HttpDelete("DeleteRecipe/{recipe}")]
        public IActionResult DeleteRecipe(IRecipeDTO recipe)
        {
            _logger.LogInformation($"Request to delete recipe recieved by RecipeController, DeleteRecipe method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");

            var recipes = new List<IRecipeDTO>
            {
                new RecipeDTO { Title = $"{recipe.Title}", Description = "Description1", RecipeType="Breakfast" },
                new RecipeDTO { Title = $"{recipe.Title}", Description = "Description2", RecipeType="Lunch"},
                new RecipeDTO { Title = $"{recipe.Title}", Description = "Description3", RecipeType="Dinner"}
            };
            recipes.Add(recipe);

            recipes.Remove(recipe);

            if (recipes.Contains(recipe))
            {
                return BadRequest();
            }

            return Ok();
        }   

        [HttpGet("SearchRecipes/{searchText}")]
        public IEnumerable<IRecipeDTO> SearchRecipes(string searchText)
        {
            _logger.LogInformation($"Search request received by RecipeController, SearchRecipes method. Search text: {searchText}. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
            return new List<RecipeDTO>();
        }
    }
}
