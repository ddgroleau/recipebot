using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBC.Shared.DOM_Events;
using PBC.Shared.ListComponent;

namespace PBC.Shared
{
    public class ListGeneratorDTO : IListGeneratorDTO
    {
        public int Id { get; set; }
        [Range(0, 7)]
        public int Days { get; set; } = 0;
        public Dictionary<int, ListDayDTO> GeneratedDays { get; set; } = new Dictionary<int, ListDayDTO>();
    }
}
