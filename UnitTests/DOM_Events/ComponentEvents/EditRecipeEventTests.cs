using Microsoft.Extensions.Logging;
using PBC.Shared;
using PBC.Shared.DOM_Events;
using PBC.Shared.DOM_Events.ComponentEvents;
using PBC.Shared.Lazor;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static PBC.Client.Components.EditRecipeModal;

namespace UnitTests.DOM_Events.ComponentEvents
{
    public class EditRecipeEventTests : IDisposable
    {
        IRecipeDTO RecipeDTO;
        ILogger<IRecipeDTO> Logger;
        ILazor Lazor;
        HttpClient Http;
        IEditRecipeEvent RecipeEvent;

        public EditRecipeEventTests()
        {
            RecipeDTO = new RecipeDTO();
            Logger = new LoggerFactory().CreateLogger<IRecipeDTO>();
            Lazor = new Lazor();
            Http = new HttpClient();
            RecipeEvent = new EditRecipeEvent(Logger, Http);
        }
        public void Dispose()
        {
            RecipeDTO = new RecipeDTO();
            Logger = new LoggerFactory().CreateLogger<IRecipeDTO>();
            Lazor = new Lazor();
            Http = new HttpClient();
            RecipeEvent = new EditRecipeEvent(Logger, Http);
        }

        [Fact]
        public async void HandleValidSubmit_WithValidParameters_ShouldReturnRecipeDTO()
        {
            var actual = await RecipeEvent.HandleValidSubmit(Lazor, RecipeDTO);

            Assert.Equal(RecipeDTO, actual);
        }
    }
}
