using Microsoft.Extensions.Logging;
using PBC.Shared;
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

namespace UnitTests.Views
{
    public class EditRecipeTests
    {
        [Fact]
        public async void HandleValidSubmit_WithValidParameters_ShouldReturnRecipeDTO()
        {
            var lazor = new Lazor();
            var recipeDTO = new RecipeDTO();
            var logger = new LoggerFactory().CreateLogger<IRecipeDTO>();
            var Http = new HttpClient();

            var actual = await EditRecipeView.HandleValidSubmit(lazor, recipeDTO, Http, logger);

            Assert.Equal(recipeDTO, actual);
        }
    }
}
