using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.ListComponent
{
    public interface IListDayDTO
    {
        public int ListId { get; set; }
        public DateTime Date { get; set; }
        public IRecipeDTO Breakfast { get; set; }
        public IRecipeDTO Lunch { get; set; }
        public IRecipeDTO Dinner { get; set; }
    }
}
