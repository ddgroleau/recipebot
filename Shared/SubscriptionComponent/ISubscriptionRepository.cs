using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.SubscriptionComponent
{
    public interface ISubscriptionRepository
    {
        public void CreateSubscription(int id);
        public Task UpdateSubscription(int id);
        public IEnumerable<ISubscriptionServiceDTO> GetUserRecipes(int userId);
    }
}
