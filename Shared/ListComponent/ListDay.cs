using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.ListComponent
{
    public class ListDay
    {
        public int ListDayId { get; set; }
        public int ListEntityId { get; set; }
        [Range(1, 7)]
        public int SequenceNumber { get; set; }
        public DateTime Date { get; set; }

        // I had to use concrete implementation types here else I could not deserialize this object from JSON.
        public Recipe Breakfast { get; set; }
        public Recipe Lunch { get; set; }
        public Recipe Dinner { get; set; }
    }
}
