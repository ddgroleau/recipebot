using Microsoft.Extensions.Logging;
using Recipebot.Shared.Custom_Validation;
using Recipebot.Shared.DOM_Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Recipebot.Shared.RecipeComponent
{
    public class RecipeUrlDTO : IRecipeUrlDTO
    {
        [AcceptableURL]
        public string URL { get; set; }
    }
}
