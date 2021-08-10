using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
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
    public class SearchBarEventTests : IDisposable
    {
        HttpClient Http;
        ILazor Lazor;
        ILogger<ISearchBarEvent> Logger;
        ISearchBarEvent SearchBarEvent;
        public SearchBarEventTests()
        {
            Lazor = new Lazor();
            Logger = new LoggerFactory().CreateLogger<ISearchBarEvent>();
            Http = new HttpClient();
            SearchBarEvent = new SearchBarEvent(Http, Logger, Lazor);
        }

        public void Dispose()
        {
            Lazor = new Lazor();
            Logger = new LoggerFactory().CreateLogger<ISearchBarEvent>();
            Http = new HttpClient();
            SearchBarEvent = new SearchBarEvent(Http, Logger, Lazor);
        }

        [Fact]
        public void HandleKeyPress_WithEmptySearchText_ShouldReturnFalse()
        {
            SearchBarEvent.SearchText = String.Empty;

            SearchBarEvent.HandleKeyPress();

            Assert.False(SearchBarEvent.SearchResults.Any());
        }

        [Fact]
        public void HandleClick_WithEmptySearchText_ShouldReturnFalse()
        {
            SearchBarEvent.SearchText = String.Empty;

            SearchBarEvent.HandleClick();

            Assert.False(SearchBarEvent.SearchResults.Any());
        }

        [Fact]
        public void HandleClick_WithValidSearchText_ShouldReturnRecipeDTOList()
        {
            SearchBarEvent.SearchText = "test";

            SearchBarEvent.HandleClick();

            Assert.IsAssignableFrom<IEnumerable<IRecipeDTO>>(SearchBarEvent.SearchResults);
            Assert.False(SearchBarEvent.SearchResults.Any());
        }
    }
}
