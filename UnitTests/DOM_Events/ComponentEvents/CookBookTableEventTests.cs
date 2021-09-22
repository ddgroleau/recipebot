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
    public class CookBookTableEventTests : IDisposable
    {
        IList<IRecipeDTO> RetrievedRecipes;
        IRecipeDTO RecipeDTO;
        ILogger<IRecipeDTO> Logger;
        ILazor Lazor;
        HttpClient Http;
        ICookBookTableEvent Cookbook;
        public CookBookTableEventTests()
        {
            RetrievedRecipes = new List<IRecipeDTO>();
            RecipeDTO = new RecipeDTO();
            Lazor = new Lazor();
            Logger = new LoggerFactory().CreateLogger<IRecipeDTO>();
            Http = new HttpClient();
            Cookbook = new CookbookTableEvent(Lazor, RecipeDTO, Logger, Http, RetrievedRecipes);
        }

        public void Dispose()
        {
            Http.Dispose();
            GC.SuppressFinalize(this);
        }
        [Fact]
        public async void GetUserRecipesAsync_WithValidParameters_ShouldBeCorrectType()
        {
            var recipes = await Cookbook.GetUserRecipesAsync();

            Assert.IsAssignableFrom<IEnumerable<IRecipeDTO>>(recipes);
        }

        [Fact]
        public void HandleUpdate_WithLazorObject_ShouldChangeIsToggled()
        {
            Cookbook.HandleUpdate();

            Assert.False(Cookbook.Lazor.IsToggled);
        }

        [Fact]
        public void HandleDetails_WithLazorObject_ShouldChangeIsShown()
        {
            Cookbook.HandleDetails();

            Assert.True(Cookbook.Lazor.IsShown);
            Assert.Equal(Cookbook.Message, $"{RecipeDTO.Title}");
        }

        [Fact]
        public async Task HandleSubscribe_WithValidRecipeDTO_ShouldReturnBool()
        {
            bool result = await Cookbook.HandleSubscribe();

            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task HandleUnsubscribee_WithValidRecipeDTO_ShouldReturnBool()
        {
            bool result = await Cookbook.HandleUnsubscribe();

            Assert.IsType<bool>(result);
        }
    }
}
