using Microsoft.Extensions.Logging;
using Recipebot.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.DOM_Events.ComponentEvents
{
    public class MessageModalEvent : IMessageModalEvent
    {
        private readonly HttpClient _http;
        private readonly ILogger<IRecipeDTO> _logger;

        public MessageModalEvent(HttpClient http, ILogger<IRecipeDTO> logger)
        {
            _http = http;
            _logger = logger;
        }
        public void HandleClick(ILazor lazor)
        {
            lazor.Hide();
        }
    }
}
