using PBC.Shared.ListComponent;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared
{
    public class ListDayDTO : IListDayDTO
    {
        public int ListDayId { get; set; }
        public int ListId { get; set; }
        [Range(1,7)]
        public int SequenceNumber { get; set; }
        public DateTime Date { get; set; }
        // I had to use concrete implementation types here else I could not deserialize this object from JSON.
        public RecipeDTO Breakfast { get; set; }
        public RecipeDTO Lunch { get; set; }
        public RecipeDTO Dinner { get; set; }

    }
}
