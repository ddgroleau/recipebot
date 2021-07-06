using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared
{
    public class ListGeneratorDTO
    {
        private int Id { get; set; }
        [Range(0, 7, ErrorMessage = "Days cannot exceed 7.")]
        public int Days { get; set; } = 0;
        public Dictionary<int, ListDayDTO> GeneratedDays { get; set; } = new Dictionary<int, ListDayDTO>();

    }
}
