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

namespace UnitTests.DOM_Events.ComponentEvents
{
    public class MessageModalEventTests : IDisposable
    {
        IRecipeDTO RecipeDTO;
        ILogger<IRecipeDTO> Logger;
        ILazor Lazor;
        HttpClient Http;
        IMessageModalEvent MessageEvent;

        public MessageModalEventTests()
        {
            RecipeDTO = new RecipeDTO();
            Logger = new LoggerFactory().CreateLogger<IRecipeDTO>();
            Lazor = new Lazor();
            Http = new HttpClient();
            MessageEvent = new MessageModalEvent(Http, Logger);
        }
        public void Dispose()
        {
            RecipeDTO = new RecipeDTO();
            Logger = new LoggerFactory().CreateLogger<IRecipeDTO>();
            Lazor = new Lazor();
            Http = new HttpClient();
            MessageEvent = new MessageModalEvent(Http, Logger);
        }
        [Fact]
        public void HandleClick_WithLazorObject_ShouldMakeIsHiddenTrue()
        {
            MessageEvent.HandleClick(Lazor);

            Assert.True(Lazor.IsHidden);
        }

    }
}
