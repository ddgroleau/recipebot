using Microsoft.Extensions.Logging;
using PBC.Server.Controllers;
using PBC.Shared.ListComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Controllers
{
    public class ListControllerTests : IDisposable
    {
        ILogger<ListController> Logger;
        ListController ListController;

        public ListControllerTests()
        {
            Logger = new LoggerFactory().CreateLogger<ListController>();
            ListController = new ListController(Logger);
        }

        public void Dispose()
        {
            Logger = new LoggerFactory().CreateLogger<ListController>();
            ListController = new ListController(Logger);
        }

        [Fact]
        public void GenerateRandomDay_NoParameters_ShouldReturnDayDTO()
        {
            var result = ListController.GenerateRandomDay();

            Assert.IsAssignableFrom<IListDayDTO>(result);
        }

    }
}
