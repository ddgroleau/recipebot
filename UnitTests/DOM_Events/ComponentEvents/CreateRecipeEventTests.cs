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
    public class CreateRecipeEventTests : IDisposable
    {
        IRecipeDTO RecipeDTO;
        ILogger<IRecipeUrlDTO> Logger;
        ILazor Lazor;
        HttpClient Http;
        ICreateRecipeEvent RecipeEvent;
        IRecipeUrlDTO RecipeUrlDTO;
        public CreateRecipeEventTests()
        {
            RecipeDTO = new RecipeDTO();
            Lazor = new Lazor();
            Logger = new LoggerFactory().CreateLogger<IRecipeUrlDTO>();
            Http = new HttpClient();
            RecipeUrlDTO = new RecipeUrlDTO();
            RecipeEvent = new CreateRecipeEvent(Lazor,RecipeUrlDTO,Logger,Http,RecipeDTO);
        }

        public void Dispose()
        {
            Http.Dispose();
            GC.SuppressFinalize(this); ;
        }

        [Fact]
        public async Task HandleSubmit_WithValidParameters_ShouldReturnRecipeDTO()
        {
            var actual = await RecipeEvent.HandleSubmit();

            Assert.Equal(RecipeDTO, actual);
        }

        [Fact]
        public void ResetView_WithValidParameters_ShouldResetObjects()
        {
            Lazor.SetLoadingStatus(true);
            Lazor.SetSuccessStatus(true);
            RecipeUrlDTO.URL = "TestURL";

            RecipeEvent.ResetView();

            Assert.False(Lazor.Loading);
            Assert.False(Lazor.IsSuccess);
            Assert.Null(RecipeUrlDTO.URL);
        }
    }
}
