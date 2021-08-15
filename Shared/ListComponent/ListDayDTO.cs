using PBC.Shared.ListComponent;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared
{
    public class ListDayDTO : IListDayDTO
    {
        public int ListId { get; set; }
        public int SequenceNumber { get; set; }
        public DateTime Date { get; set; }
        // I had to use concrete implementation types here else I could not deserialize this object from JSON.
        public RecipeDTO Breakfast { get; set; }
        public RecipeDTO Lunch { get; set; }
        public RecipeDTO Dinner { get; set; }

    }
}
