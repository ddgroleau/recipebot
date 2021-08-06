using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using PBC.Shared.DOM_Events.ComponentEvents;
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
        ILogger<ISearchBarEvent> Logger;
        ISearchBarEvent SearchBarEvent;
        public SearchBarEventTests()
        {
            Logger = new LoggerFactory().CreateLogger<ISearchBarEvent>();
            Http = new HttpClient();
            SearchBarEvent = new SearchBarEvent(Http, Logger);
        }

        public void Dispose()
        {
            Logger = new LoggerFactory().CreateLogger<ISearchBarEvent>();
            Http = new HttpClient();
            SearchBarEvent = new SearchBarEvent(Http, Logger);
        }

        [Fact]
        public void HandleKeyPress_WithEmptySearchText_ShouldReturnFalse()
        {
            SearchBarEvent.SearchText = String.Empty;

            SearchBarEvent.HandleKeyPress();

            Assert.False(SearchBarEvent.RecipesFound.Any());
        }

        [Fact]
        public void HandleClick_WithEmptySearchText_ShouldReturnFalse()
        {
            SearchBarEvent.SearchText = String.Empty;

            SearchBarEvent.HandleClick();

            Assert.False(SearchBarEvent.RecipesFound.Any());
        }
    }
}
