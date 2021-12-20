using Microsoft.Extensions.Logging;
using Recipebot.Shared;
using Recipebot.Shared.DOM_Events;
using Recipebot.Shared.DOM_Events.ComponentEvents;
using Recipebot.Shared.Lazor;
using Recipebot.Shared.RecipeComponent;
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
            Http.Dispose();
            GC.SuppressFinalize(this);
        }
        [Fact]
        public void HandleClick_WithLazorObject_ShouldMakeIsHiddenTrue()
        {
            MessageEvent.HandleClick(Lazor);

            Assert.True(Lazor.IsHidden);
        }

    }
}
