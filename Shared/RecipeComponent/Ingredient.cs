using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.RecipeComponent
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public int RecipeId { get; set; }
        [MaxLength(90)]
        public string Description { get; set; }
        [MaxLength(90)]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
