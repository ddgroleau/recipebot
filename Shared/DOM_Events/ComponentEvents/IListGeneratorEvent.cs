using PBC.Shared.ListComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.DOM_Events.ComponentEvents
{
    public interface IListGeneratorEvent
    {
        public ILazor Lazor { get; set; }
        public IListGeneratorDTO ListGeneratorDTO { get; set; }
        public Task AddDay();
        public void RemoveDay();
    }
}
