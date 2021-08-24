using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public class RecipeServiceDTO : IRecipeServiceDTO
    {
        public int RecipeId { get; set; }
        public string RecipeType { get; set; }
        public string URL { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; } = new List<string>();
        public List<string> Instructions { get; set; } = new List<string>();
    }
}
