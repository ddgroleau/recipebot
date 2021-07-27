using Microsoft.Extensions.Logging;
using PBC.Shared;
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
    public class MessageModalEventTests
    {
        [Fact]
        public void HandleClick_WithLazorObject_ShouldMakeIsHiddenTrue()
        {
            var http = new HttpClient();
            var lazor = new Lazor();
            var logger = new LoggerFactory().CreateLogger<IRecipeDTO>();
            var messageModal = new MessageModalEvent(http, logger);

            messageModal.HandleClick(lazor);

            Assert.True(lazor.isHidden);
        }

        [Fact]
        public void DeleteRecipe_WithValidRecipeDTO_ShouldHide()
        {
            var http = new HttpClient();
            var lazor = new Lazor();
            var logger = new LoggerFactory().CreateLogger<IRecipeDTO>();
            var messageModal = new MessageModalEvent(http, logger);
            var recipeDTO = new RecipeDTO();

            messageModal.DeleteRecipe(recipeDTO, lazor);

            Assert.True(lazor.isHidden);
        }
    }
}
