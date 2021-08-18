using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.SubscriptionComponent
{
    public interface ISubscriberMemento
    {
        public void UpdateState(int id);
        public Dictionary<int, bool> GetRecipeSubscriptions();
    }
}
