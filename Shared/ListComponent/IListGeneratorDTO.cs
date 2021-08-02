using PBC.Shared.DOM_Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.ListComponent
{
    public interface IListGeneratorDTO
    {
        public int Id { get; set; }
        public int Days { get; set; }
        public Dictionary<int, ListDayDTO> GeneratedDays { get; set; }
        public void AddDay(ILazor lazor);
        public void RemoveDay(ILazor lazor);
    }
}
