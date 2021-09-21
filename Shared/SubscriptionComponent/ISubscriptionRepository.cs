using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.SubscriptionComponent
{
    public interface ISubscriptionRepository
    {
        public void Subscribe(int id);
        public Task Unsubscribe(int id);
        public IEnumerable<ISubscriptionServiceDTO> GetUserRecipes(int userId);
    }
}
