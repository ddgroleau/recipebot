﻿using Microsoft.AspNetCore.Http;
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
    [Route("/api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly ILogger<ListController> _logger;
        private readonly IListService _listService;

        public ListController(ILogger<ListController> logger, IListService listService)
        {
            _logger = logger;
            _listService = listService;
        }
        
        [HttpGet("Day")]
        public async Task<IListDayDTO> GenerateRandomDay()
        {
            _logger.LogInformation($"Received request for Random Day at ListController, GenerateRandomDay method. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");

            var listDay = await _listService.GenerateDayOfRecipes();

            return listDay;
    }

}
}