using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBC.Shared.RecipeComponent;

namespace PBC.Shared
{
    public class RecipeDTO : IRecipeDTO
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; } = new List<string>();
        public List<string> Instructions { get; set; } = new List<string>();
        public string NewIngredient { get; set; }
        public string NewInstruction { get; set; }
        public void addIngredient()
        {
            Ingredients.Add(NewIngredient);
        }
        public void addInstruction()
        {
            Instructions.Add(NewInstruction);
        }
    }
}
