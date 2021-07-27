using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.DOM_Events.ComponentEvents
{
    public class MessageModalEvent : IMessageModalEvent
    {
        public void HandleClick(ILazor e)
        {
            e.Hide();
        }
    }
}
