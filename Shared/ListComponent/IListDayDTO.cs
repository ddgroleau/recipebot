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
        public RecipeDTO Breakfast { get; set; }
        public RecipeDTO Lunch { get; set; }
        public RecipeDTO Dinner { get; set; }
    }
}
