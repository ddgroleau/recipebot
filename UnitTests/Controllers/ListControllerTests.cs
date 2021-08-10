using Microsoft.Extensions.Logging;
using PBC.Server.Controllers;
using PBC.Shared;
using PBC.Shared.ListComponent;
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
        IListService ListService;
        IListDayDTO ListDayDTO;
        IListBuilder ListBuilder;
        HttpClient Http;
        ILogger<ListController> Logger;
        ListController ListController;

        public ListControllerTests()
        {
            Http = new HttpClient();
            ListDayDTO = new ListDayDTO();
            ListBuilder = new ListBuilder(ListDayDTO);
            ListService = new ListService(ListBuilder, Http);
            Logger = new LoggerFactory().CreateLogger<ListController>();
            ListController = new ListController(Logger, ListService);
        }

        public void Dispose()
        {
            Http = new HttpClient();
            ListDayDTO = new ListDayDTO();
            ListBuilder = new ListBuilder(ListDayDTO);
            ListService = new ListService(ListBuilder, Http);
            Logger = new LoggerFactory().CreateLogger<ListController>();
            ListController = new ListController(Logger, ListService);
        }

        [Fact]
        public void GenerateRandomDay_NoParameters_ShouldReturnDayDTO()
        {
            var result = ListController.GenerateRandomDay();

            Assert.IsAssignableFrom<IListDayDTO>(result);
        }

    }
}
