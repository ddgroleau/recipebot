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
using static PBC.Client.Components.EditRecipeModal;

namespace UnitTests.DOM_Events.ComponentEvents
{
    public class EditRecipeEventTests
    {
        [Fact]
        public async void HandleValidSubmit_WithValidParameters_ShouldReturnRecipeDTO()
        {
            var lazor = new Lazor();
            var recipeDTO = new RecipeDTO();
            var logger = new LoggerFactory().CreateLogger<IRecipeDTO>();
            var http = new HttpClient();

            var editRecipe = new EditRecipeEvent(logger,http);

            var actual = await editRecipe.HandleValidSubmit(lazor, recipeDTO);

            Assert.Equal(recipeDTO, actual);
        }
    }
}
