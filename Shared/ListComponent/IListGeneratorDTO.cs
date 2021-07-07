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
        public bool Loading { get; set; }
        public string ErrorMessage { get; set; }
        public void AddDay();
        public void RemoveDay();
    }
}
