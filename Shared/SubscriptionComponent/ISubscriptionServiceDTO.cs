using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.SubscriptionComponent
{
    public interface ISubscriptionServiceDTO
    {
        public int RecipeSubscriptionId { get; set; }

        public IRecipeServiceDTO Recipe { get; set; }
    }
}
