using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public class IngredientEntity
    {
        public int IngredientEntityId { get; set; }
        public int RecipeEntityId { get; set; }
        public string IngredientDescription { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
