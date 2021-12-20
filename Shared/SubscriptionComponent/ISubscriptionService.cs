using Recipebot.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.SubscriptionComponent
{
    public interface ISubscriptionService
    {
        public void Subscribe(int id);
        public void Unsubscribe(int id);
    }
}
