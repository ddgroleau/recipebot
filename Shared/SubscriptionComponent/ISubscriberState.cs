using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.SubscriptionComponent
{
    public interface ISubscriberState
    {
        public void UpdateState(int id);
        public Task<Dictionary<int, bool>> GetRecipeSubscriptions();
    }
}
