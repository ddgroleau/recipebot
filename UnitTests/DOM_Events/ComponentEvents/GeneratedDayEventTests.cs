using Microsoft.Extensions.Logging;
using PBC.Shared;
using PBC.Shared.DOM_Events.ComponentEvents;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.DOM_Events.ComponentEvents
{
    public class GeneratedDayEventTests : IDisposable
    {
        IGeneratedDayEvent GeneratedDayEvent;
        HttpClient Http;
        IRecipeDTO RecipeDTO;
        ILogger<GeneratedDayEvent> Logger;

        public GeneratedDayEventTests()
        {
            Logger = new LoggerFactory().CreateLogger<GeneratedDayEvent>();
            Http = new HttpClient();
            RecipeDTO = new RecipeDTO();
            GeneratedDayEvent = new GeneratedDayEvent(RecipeDTO,Http, Logger);
        }
        public void Dispose()
        {
            Logger = new LoggerFactory().CreateLogger<GeneratedDayEvent>();
            Http = new HttpClient();
            RecipeDTO = new RecipeDTO();
            GeneratedDayEvent = new GeneratedDayEvent(RecipeDTO, Http, Logger);
        }

        [Fact]
        public async Task RegenerateRecipe_WithValidRecipeType_ShouldReturnSameRecipe()
        {
            var result = await GeneratedDayEvent.RegenerateRecipe("Breakfast");

            Assert.IsAssignableFrom<IRecipeDTO>(result);
            Assert.Equal(RecipeDTO, result);
        }
    }
}
