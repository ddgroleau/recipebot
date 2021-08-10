using PBC.Shared.ListComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.DOM_Events.ComponentEvents
{
    public class ListGeneratorEvent : IListGeneratorEvent
    {
        public ILazor Lazor { get; set; }
        public IListGeneratorDTO ListGeneratorDTO { get; set; }
        public ListGeneratorEvent(ILazor lazor, IListGeneratorDTO listGeneratorDTO)
        {
            Lazor = lazor;
            ListGeneratorDTO = listGeneratorDTO;
        }
        public void AddDay()
        {
            Lazor.SetErrorMessage(null);
            if (ListGeneratorDTO.Days >= 7)
            {
                Lazor.SetErrorMessage("Max 7 Days");
            }
            else
            {
                ListGeneratorDTO.Days += 1;
                // To do: add functionality to fill in day object.
                ListDayDTO day = new ListDayDTO();
                ListGeneratorDTO.GeneratedDays.Add(ListGeneratorDTO.Days, day);
            }

        }
        public void RemoveDay()
        {
            Lazor.SetErrorMessage(null);
            if (ListGeneratorDTO.Days <= 0)
            {
                Lazor.SetErrorMessage("Min 0 Days");
            }
            else
            {
                ListGeneratorDTO.GeneratedDays.Remove(ListGeneratorDTO.Days);
                ListGeneratorDTO.Days -= 1;
            }
        }
    }
}
