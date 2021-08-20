using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.SubscriptionComponent
{
    public class RecipeSubscription
    {
        public int RecipeSubscriptionId { get; set; }
        public Recipe Recipe { get; set; }
        public int ApplicationUserId { get; set; }
        public bool IsSubscribed { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifed { get; set; }
    }
}
