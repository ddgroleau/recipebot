using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public class RecipeEntity
    {
        public int RecipeEntityId { get; set; }
        public string RecipeType { get; set; }
        public string URL { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<IngredientEntity> Ingredients { get; set; }
        public ICollection<InstructionEntity> Instructions { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
