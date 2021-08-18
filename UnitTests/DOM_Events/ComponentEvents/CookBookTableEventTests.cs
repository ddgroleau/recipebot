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
            RetrievedRecipes = new List<IRecipeDTO>();
            RecipeDTO = new RecipeDTO();
            Lazor = new Lazor();
            Logger = new LoggerFactory().CreateLogger<IRecipeDTO>();
            Http = new HttpClient();
            Cookbook = new CookbookTableEvent(Lazor, RecipeDTO, Logger, Http, RetrievedRecipes);
        }
        [Fact]
        public async void GetRecipesAsync_WithValidParameters_ShouldBeCorrectType()
        {
            var recipes = await Cookbook.GetRecipesAsync("TestUser");

            Assert.IsAssignableFrom<IEnumerable<IRecipeDTO>>(recipes);
        }

        [Fact]
        public void HandleUpdate_WithLazorObject_ShouldChangeIsToggled()
        {
            Cookbook.HandleUpdate();

            Assert.False(Cookbook.Lazor.IsToggled);
        }

        [Fact]
        public void HandleDelete_WithLazorObject_ShouldChangeIsShown()
        {
            Cookbook.HandleDelete();

            Assert.True(Cookbook.Lazor.IsShown);
            Assert.True(Cookbook.IsDeleteAction);
            Assert.Equal(Cookbook.Message, $"Are you sure you want to delete \"{RecipeDTO.Title}\"?");
        }

        [Fact]
        public void HandleDetails_WithLazorObject_ShouldChangeIsShown()
        {
            Cookbook.HandleDetails();

            Assert.True(Cookbook.Lazor.IsShown);
            Assert.False(Cookbook.IsDeleteAction);
            Assert.Equal(Cookbook.Message, $"{RecipeDTO.Title}");
        }

        [Fact]
        public void HandleSubscribe()
        {
            bool result = Cookbook.HandleSubscribe();

            Assert.IsType<bool>(result);
        }
    }
}
