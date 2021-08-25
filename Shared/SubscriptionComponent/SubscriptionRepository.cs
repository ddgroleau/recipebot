using PBC.Shared.Common;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.SubscriptionComponent
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly IFactory<RecipeSubscription> _subscriptionFactory;

        public SubscriptionRepository(IFactory<RecipeSubscription> subscriptionFactory)
        {
            _subscriptionFactory = subscriptionFactory;
        }
        public void CreateSubscription(int id)
        {
            RecipeSubscription subscription = _subscriptionFactory.Make();
            subscription.Recipe.RecipeId = id;
        }
        public void UpdateSubscription(int id)
        {

        }

        public IEnumerable<ISubscriptionServiceDTO> GetUserRecipes(int userId)
        {

            var recipes = new List<ISubscriptionServiceDTO>()
            {
                {
                    new SubscriptionServiceDTO
                    {
                        Recipe =  new RecipeServiceDTO
                                  {
                                        RecipeId = 1, Title = $"Recipe1",  Description = "Description1",  RecipeType="Breakfast", Ingredients ={ "Salt"}, Instructions={"Combine and cook."}
                                  }
                    }
                },
                   {
                    new SubscriptionServiceDTO
                    {
                        Recipe =  new RecipeServiceDTO
                                  {
                                        RecipeId = 1, Title = $"Recipe2",  Description = "Description2",  RecipeType="Breakfast", Ingredients ={ "Salt"}, Instructions={"Combine and cook."}
                                  }
                    }
                },
                      {
                    new SubscriptionServiceDTO
                    {
                        Recipe =  new RecipeServiceDTO
                                  {
                                        RecipeId = 1, Title = $"Recipe3",  Description = "Description3",  RecipeType="Breakfast", Ingredients ={ "Salt"}, Instructions={"Combine and cook."}
                                  }
                    }
                },
                         {
                    new SubscriptionServiceDTO
                    {
                        Recipe =  new RecipeServiceDTO
                                  {
                                        RecipeId = 1, Title = $"Recipe4",  Description = "Description4",  RecipeType="Dinner", Ingredients ={ "Salt"}, Instructions={"Combine and cook."}
                                  }
                    }
                },
                            {
                    new SubscriptionServiceDTO
                    {
                        Recipe =  new RecipeServiceDTO
                                  {
                                        RecipeId = 1, Title = $"Recipe5",  Description = "Description5",  RecipeType="Dinner", Ingredients ={ "Salt"}, Instructions={"Combine and cook."}
                                  }
                    }
                },
                               {
                    new SubscriptionServiceDTO
                    {
                        Recipe =  new RecipeServiceDTO
                                  {
                                        RecipeId = 1, Title = $"Recipe6",  Description = "Description6",  RecipeType="Lunch", Ingredients ={ "Salt"}, Instructions={"Combine and cook."}
                                  }
                    }
                },
                                  {
                    new SubscriptionServiceDTO
                    {
                        Recipe =  new RecipeServiceDTO
                                  {
                                        RecipeId = 1, Title = $"Recipe7",  Description = "Description7",  RecipeType="Lunch", Ingredients ={ "Salt"}, Instructions={"Combine and cook."}
                                  }
                    }
                },
                                     {
                    new SubscriptionServiceDTO
                    {
                        Recipe =  new RecipeServiceDTO
                                  {
                                        RecipeId = 1, Title = $"Recipe8",  Description = "Description8",  RecipeType="Lunch", Ingredients ={ "Salt"}, Instructions={"Combine and cook."}
                                  }
                    }
                },
                                        {
                    new SubscriptionServiceDTO
                    {
                        Recipe =  new RecipeServiceDTO
                                  {
                                        RecipeId = 1, Title = $"Recipe9",  Description = "Description9",  RecipeType="Dinner", Ingredients ={ "Salt"}, Instructions={"Combine and cook."}
                                  }
                    }
                }
            };
            
            return recipes;
        }
    
    
    }
}
