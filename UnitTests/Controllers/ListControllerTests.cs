﻿using Microsoft.Extensions.Logging;
using PBC.Server.Controllers;
using PBC.Shared;
using PBC.Shared.ListComponent;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Controllers
{
    public class ListControllerTests : IDisposable
    {
        IRecipeDTO RecipeDTO;
        IListService ListService;
        IListDayDTO ListDayDTO;
        IListBuilder ListBuilder;
        HttpClient Http;
        ILogger<ListController> Logger;
        ListController ListController;

        public ListControllerTests()
        {
            RecipeDTO = new RecipeDTO();
            Http = new HttpClient();
            ListDayDTO = new ListDayDTO();
            ListBuilder = new ListBuilder(ListDayDTO, RecipeDTO);
            ListService = new ListService(ListBuilder, Http, ListDayDTO);
            Logger = new LoggerFactory().CreateLogger<ListController>();
            ListController = new ListController(Logger, ListService, ListDayDTO);
        }

        public void Dispose()
        {
            RecipeDTO = new RecipeDTO();
            Http = new HttpClient();
            ListDayDTO = new ListDayDTO();
            ListBuilder = new ListBuilder(ListDayDTO, RecipeDTO);
            ListService = new ListService(ListBuilder, Http, ListDayDTO);
            Logger = new LoggerFactory().CreateLogger<ListController>();
            ListController = new ListController(Logger, ListService, ListDayDTO);
        }

        [Fact]
        public async void GenerateRandomDay_NoParameters_ShouldReturnDayDTO()
        {
            var result = await ListController.GenerateRandomDay();

            Assert.IsAssignableFrom<IListDayDTO>(result);
        }

    }
}
