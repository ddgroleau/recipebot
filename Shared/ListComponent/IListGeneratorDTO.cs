using Recipebot.Shared.DOM_Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.ListComponent
{
    public interface IListGeneratorDTO
    {
        public int ListEntityId { get; set; }
        public int Days { get; set; }
        public List<ListDayDTO> GeneratedDays { get; set; }
    }
}
