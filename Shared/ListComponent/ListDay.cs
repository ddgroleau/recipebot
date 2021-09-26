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
        public int BreakfastRecipeId { get; set; }
        public int LunchRecipeId { get; set; }
        public int DinnerRecipeId { get; set; }
    }
}
