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
using PBC.Shared.Custom_Validation;
using PBC.Shared.RecipeComponent;

namespace PBC.Shared
{
    public class RecipeDTO : IRecipeDTO
    {
        [AcceptableURL]
        public string URL { get; set; }
        [Required]
        public string Title { get; set; }
        [MustBeValidType]
        public string RecipeType { get; set; }
        public string Description { get; set; }
        [ListMustContainElements]
        public List<string> Ingredients { get; set; } = new List<string>();
        [ListMustContainElements]
        public List<string> Instructions { get; set; } = new List<string>();
    }
}
