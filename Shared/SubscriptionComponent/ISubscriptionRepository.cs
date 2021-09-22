using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.SubscriptionComponent
{
    public interface ISubscriptionRepository
    {
        public Task Subscribe(int recipeId);
        public Task Unsubscribe(int recipeId);
    }
}
