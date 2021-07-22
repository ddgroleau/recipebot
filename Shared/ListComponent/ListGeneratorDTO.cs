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
        public void AddDay(ILazor e)
        {
            e.ErrorMessage = "";
            if (Days >= 7)
            {
                e.ErrorMessage = "Max 7 Days";
            }
            else
            {
                Days += 1;
                // To do: add functionality to fill in day object.
                ListDayDTO day = new ListDayDTO();
                GeneratedDays.Add(Days, day);
            }

        }
        public void RemoveDay(ILazor e)
        {
            e.ErrorMessage = "";
            if (Days <= 0)
            {
                e.ErrorMessage = "Min 0 Days";
            }
            else
            {
                GeneratedDays.Remove(Days);
                Days -= 1;
            }
        }

    }
}
