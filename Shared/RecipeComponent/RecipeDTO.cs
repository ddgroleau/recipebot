using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Recipebot.Shared.Custom_Validation;
using Recipebot.Shared.RecipeComponent;

namespace Recipebot.Shared
{
    public class RecipeDTO : IRecipeDTO
    {
        public int RecipeId { get; set; }
        [AcceptableURL]
        public string URL { get; set; }
        [Required]
        public string Title { get; set; }
        [MustBeValidType]
        public string RecipeType { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [ListMustContainElements]
        public List<string> Ingredients { get; set; } = new List<string>();
        [ListMustContainElements]
        public List<string> Instructions { get; set; } = new List<string>();
    }
}
