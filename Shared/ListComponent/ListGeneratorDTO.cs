using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBC.Shared.ListComponent;

namespace PBC.Shared
{
    public class ListGeneratorDTO : IListGeneratorDTO
    {
        public int Id { get; set; }
        [Range(0, 7, ErrorMessage = "Days cannot exceed 7.")]
        public int Days { get; set; } = 0;
        public Dictionary<int, ListDayDTO> GeneratedDays { get; set; } = new Dictionary<int, ListDayDTO>();
        public bool Loading { get; set; } = false;
        public string ErrorMessage { get; set; }
        public void AddDay()
        {
            ErrorMessage = "";
            if (Days >= 7)
            {
                ErrorMessage = "Max 7 Days";
            }
            else
            {
                Days += 1;
                ListDayDTO day = new ListDayDTO();
                GeneratedDays.Add(Days, day);
            }

        }
        public void RemoveDay()
        {
            ErrorMessage = "";
            if (Days <= 0)
            {
                ErrorMessage = "Min 0 Days";
            }
            else
            {
                GeneratedDays.Remove(Days);
                Days -= 1;
            }
        }

    }
}
