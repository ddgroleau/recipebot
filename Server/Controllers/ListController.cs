using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PBC.Shared;
using PBC.Shared.ListComponent;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PBC.Server.Controllers
{
    [Authorize]
    [Route("/api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly ILogger<ListController> _logger;
        private readonly IListService _listService;
        private readonly IListDayDTO _listDayDTO;

        public ListController(ILogger<ListController> logger, IListService listService, IListDayDTO listDayDTO)
        {
            _logger = logger;
            _listService = listService;
            _listDayDTO = listDayDTO;
        }

        [HttpGet("day")]
        public async Task<IListDayDTO> GenerateRandomDay()
        {
            _logger.LogInformation($"Received request for Random Day at ListController, GenerateRandomDay method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");

            try
            {
                var listDay = await _listService.GenerateDayOfRecipes();
                return listDay;
            }
            catch (Exception)
            {
                _logger.LogInformation($"Failed to process request for Random Day at ListController, GenerateRandomDay method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                return _listDayDTO;
            }
        }

        [HttpPost("new-list")]
        public async Task<IActionResult> CreateList(ListGeneratorDTO listDays)
        {
            try
            {
                await _listService.CreateList(listDays);
                _logger.LogInformation($"Received new List at ListController, CreateList method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error when receiving new List at ListController, CreateList method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}. {e.Message}.");
            }
            return BadRequest();
        }

        [HttpGet("random-recipe/{recipeType}")]
        public async Task<IRecipeDTO> GetRandomRecipeByType(string recipeType)
        {
            return await _listService.GenerateRandomRecipeByType(recipeType);
        }
    }
}
