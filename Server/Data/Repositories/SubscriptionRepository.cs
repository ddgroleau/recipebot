using Microsoft.EntityFrameworkCore;
using PBC.Shared.Common;
using PBC.Shared.RecipeComponent;
using PBC.Shared.SubscriptionComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Server.Data.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly IFactory<RecipeSubscription> _subscriptionFactory;
        private readonly ApplicationDbContext _context;

        public SubscriptionRepository(IFactory<RecipeSubscription> subscriptionFactory, ApplicationDbContext context)
        {
            _subscriptionFactory = subscriptionFactory;
            _context = context;
        }
        public void CreateSubscription(int id)
        {
            RecipeSubscription subscription = _subscriptionFactory.Make();
            subscription.Recipe.RecipeId = id;
        }
        public async Task UpdateSubscription(int id)
        {
            var entity = await _context.RecipeSubscriptions.FindAsync(id);
            if(entity != null)
            {
                entity.IsSubscribed = false;
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public IEnumerable<ISubscriptionServiceDTO> GetUserRecipes(int userId)
        {

            var recipes = new List<ISubscriptionServiceDTO>
            {

                new SubscriptionServiceDTO
                {
                    Recipe = new RecipeServiceDTO
                    {
                        RecipeId = 11,
                        Title = $"Recipe11",
                        Description = "Description11",
                        RecipeType = "Breakfast",
                        Ingredients = { "Salt" },
                        Instructions = { "Combine and cook." }
                    }
                },
                    new SubscriptionServiceDTO
                    {
                        Recipe = new RecipeServiceDTO
                        {
                            RecipeId = 12,
                            Title = $"Recipe12",
                            Description = "Description12",
                            RecipeType = "Breakfast",
                            Ingredients = { "Salt" },
                            Instructions = { "Combine and cook." }
                        }
                    },
            new SubscriptionServiceDTO
            {
                Recipe = new RecipeServiceDTO
                {
                    RecipeId = 13,
                    Title = $"Recipe13",
                    Description = "Description13",
                    RecipeType = "Breakfast",
                    Ingredients = { "Salt" },
                    Instructions = { "Combine and cook." }
                }
            },
            new SubscriptionServiceDTO
            {
                Recipe = new RecipeServiceDTO
                {
                    RecipeId = 14,
                    Title = $"Recipe14",
                    Description = "Description14",
                    RecipeType = "Breakfast",
                    Ingredients = { "Salt" },
                    Instructions = { "Combine and cook." }
                }
            },
            new SubscriptionServiceDTO
            {
                Recipe = new RecipeServiceDTO
                {
                    RecipeId = 15,
                    Title = $"Recipe15",
                    Description = "Description15",
                    RecipeType = "Breakfast",
                    Ingredients = { "Salt" },
                    Instructions = { "Combine and cook." }
                }
            },
            new SubscriptionServiceDTO
            {
                Recipe = new RecipeServiceDTO
                {
                    RecipeId = 16,
                    Title = $"Recipe16",
                    Description = "Description16",
                    RecipeType = "Lunch",
                    Ingredients = { "Salt" },
                    Instructions = { "Combine and cook." }
                }
            },

            new SubscriptionServiceDTO
            {
                Recipe = new RecipeServiceDTO
                {
                    RecipeId = 17,
                    Title = $"Recipe17",
                    Description = "Description17",
                    RecipeType = "Lunch",
                    Ingredients = { "Salt" },
                    Instructions = { "Combine and cook." }
                }
            },

            new SubscriptionServiceDTO
            {
                Recipe = new RecipeServiceDTO
                {
                    RecipeId = 18,
                    Title = $"Recipe18",
                    Description = "Description18",
                    RecipeType = "Lunch",
                    Ingredients = { "Salt" },
                    Instructions = { "Combine and cook." }
                }
            },

            new SubscriptionServiceDTO
            {
                Recipe = new RecipeServiceDTO
                {
                    RecipeId = 19,
                    Title = $"Recipe19",
                    Description = "Description19",
                    RecipeType = "Dinner",
                    Ingredients = { "Salt" },
                    Instructions = { "Combine and cook." }
                }
            },

            new SubscriptionServiceDTO
            {
                Recipe = new RecipeServiceDTO
                {
                    RecipeId = 20,
                    Title = $"Recipe20",
                    Description = "Description20",
                    RecipeType = "Dinner",
                    Ingredients = { "Salt" },
                    Instructions = { "Combine and cook." }
                }
            },

            new SubscriptionServiceDTO
            {
                Recipe = new RecipeServiceDTO
                {
                    RecipeId = 21,
                    Title = $"Recipe21",
                    Description = "Description21",
                    RecipeType = "Dinner",
                    Ingredients = { "Salt" },
                    Instructions = { "Combine and cook." }
                }
            },

            new SubscriptionServiceDTO
            {
                Recipe = new RecipeServiceDTO
                {
                    RecipeId = 22,
                    Title = $"Recipe22",
                    Description = "Description22",
                    RecipeType = "Dinner",
                    Ingredients = { "Salt" },
                    Instructions = { "Combine and cook." }
                }
            },

            new SubscriptionServiceDTO
            {
                Recipe = new RecipeServiceDTO
                {
                    RecipeId = 23,
                    Title = $"Recipe23",
                    Description = "Description23",
                    RecipeType = "Dinner",
                    Ingredients = { "Salt" },
                    Instructions = { "Combine and cook." }
                }
            } };


            return recipes;
        }


    }
}
