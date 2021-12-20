using Recipebot.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.SubscriptionComponent
{
    public class RecipeSubscription
    {
        public int RecipeSubscriptionId { get; set; }
        public int RecipeId { get; set; }
        public string ApplicationUserId { get; set; }
        public bool IsSubscribed { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifed { get; set; }
    }
}
