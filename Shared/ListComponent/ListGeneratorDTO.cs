using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recipebot.Shared.Custom_Validation;
using Recipebot.Shared.DOM_Events;
using Recipebot.Shared.ListComponent;

namespace Recipebot.Shared
{
    public class ListGeneratorDTO : IListGeneratorDTO
    {
        public int ListEntityId { get; set; }
        [Range(1,7)]
        public int Days { get; set; }
        [ListMustContainDays]
        public List<ListDayDTO> GeneratedDays { get; set; } = new List<ListDayDTO>();
    }
}
