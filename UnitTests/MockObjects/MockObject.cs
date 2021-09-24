using PBC.Shared.Custom_Validation;
using PBC.Shared.RecipeComponent;
using PBC.Shared.SubscriptionComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MockObjects
{
    public class MockObject
    {
        [AcceptableURL]
        public string URL { get; set; }
        
        [ListMustContainElements]
        public List<string> ListString { get; set; } = new List<string>();

        public RecipeSubscription RecipeSubscription = new RecipeSubscription
        {
            ApplicationUserId = "TestUserId",
            CreationDate = DateTime.Now,
            IsSubscribed = true,
            RecipeSubscriptionId = 1,
            LastModifed = DateTime.Now,
            RecipeId = 1
        };


        public Recipe Recipe = new Recipe
        {
            RecipeId = 1,
            Title = "Recipe1",
            Description = "Description1",
            URL = "https://allrecipes.com/1234",
            RecipeType = "Breakfast",
            CreatedBy = "Test",
            CreatedOn = DateTime.Now,
            Ingredients = new List<Ingredient>
                        {
                            new Ingredient
                            {
                                IngredientId = 1,
                                RecipeId = 1,
                                CreatedBy = "Test",
                                CreatedOn = DateTime.Now,
                                Description = "Salt"
                            }
                        },
            Instructions = new List<Instruction>
                        {
                            new Instruction
                            {
                                InstructionId = 1,
                                RecipeId = 1,
                                CreatedBy = "Test",
                                CreatedOn = DateTime.Now,
                                StepNumber = 1,
                                Description = "Combine and cook."
                            }
                          }
        };

        public List<IRecipeServiceDTO> RecipeServiceDTOs = new List<IRecipeServiceDTO>
            {
                new RecipeServiceDTO
                    {
                        RecipeId = 11,
                        Title = $"Recipe11",
                        Description = "Description11",
                        RecipeType = "Breakfast",
                        Ingredients = { "Salt" },
                        Instructions = { "Combine and cook." }
                    },
                new RecipeServiceDTO
                    {
                        RecipeId = 12,
                        Title = $"Recipe12",
                        Description = "Description12",
                        RecipeType = "Breakfast",
                        Ingredients = { "Salt" },
                        Instructions = { "Combine and cook." }
                    },
                 new RecipeServiceDTO
                    {
                        RecipeId = 13,
                        Title = $"Recipe13",
                        Description = "Description13",
                        RecipeType = "Breakfast",
                        Ingredients = { "Salt" },
                        Instructions = { "Combine and cook." }
                    },
                  new RecipeServiceDTO
                    {
                        RecipeId = 14,
                        Title = $"Recipe14",
                        Description = "Description14",
                        RecipeType = "Breakfast",
                        Ingredients = { "Salt" },
                        Instructions = { "Combine and cook." }
                    },
                  new RecipeServiceDTO
                    {
                        RecipeId = 15,
                        Title = $"Recipe15",
                        Description = "Description15",
                        RecipeType = "Breakfast",
                        Ingredients = { "Salt" },
                        Instructions = { "Combine and cook." }
                    },
               new RecipeServiceDTO
                    {
                        RecipeId = 16,
                        Title = $"Recipe16",
                        Description = "Description16",
                        RecipeType = "Lunch",
                        Ingredients = { "Salt" },
                        Instructions = { "Combine and cook." }
                    },
                new RecipeServiceDTO
                    {
                        RecipeId = 17,
                        Title = $"Recipe17",
                        Description = "Description17",
                        RecipeType = "Lunch",
                        Ingredients = { "Salt" },
                        Instructions = { "Combine and cook." }
                    },
               new RecipeServiceDTO
                   {
                       RecipeId = 18,
                       Title = $"Recipe18",
                       Description = "Description18",
                       RecipeType = "Lunch",
                       Ingredients = { "Salt" },
                       Instructions = { "Combine and cook." }
                   },
                new RecipeServiceDTO
                    {
                        RecipeId = 19,
                        Title = $"Recipe19",
                        Description = "Description19",
                        RecipeType = "Dinner",
                        Ingredients = { "Salt" },
                        Instructions = { "Combine and cook." }
                    },
                new RecipeServiceDTO
                    {
                        RecipeId = 20,
                        Title = $"Recipe20",
                        Description = "Description20",
                        RecipeType = "Dinner",
                        Ingredients = { "Salt" },
                        Instructions = { "Combine and cook." }
                    },
                new RecipeServiceDTO
                    {
                        RecipeId = 21,
                        Title = $"Recipe21",
                        Description = "Description21",
                        RecipeType = "Dinner",
                        Ingredients = { "Salt" },
                        Instructions = { "Combine and cook." }
                    },
                new RecipeServiceDTO
                    {
                        RecipeId = 22,
                        Title = $"Recipe22",
                        Description = "Description22",
                        RecipeType = "Dinner",
                        Ingredients = { "Salt" },
                        Instructions = { "Combine and cook." }
                    },
                 new RecipeServiceDTO
                     {
                         RecipeId = 23,
                         Title = $"Recipe23",
                         Description = "Description23",
                         RecipeType = "Dinner",
                         Ingredients = { "Salt" },
                         Instructions = { "Combine and cook." }
                     }
            };

        public RecipeServiceDTO RecipeServiceDTO = new RecipeServiceDTO
        {
            Title = "Recipe2",
            Description = "Description2",
            URL = "https://allrecipes.com/12345",
            RecipeType = "Dinner",
            Ingredients = { "Pepper" },
            Instructions = { "Shake and bake." }
        };

        public Recipe CreateRecipeExpected = new Recipe
        {
            Title = "Recipe2",
            Description = "Description2",
            URL = "https://allrecipes.com/12345",
            RecipeType = "Dinner",
            Ingredients = new List<Ingredient>
                        {
                            new Ingredient
                            {
                                IngredientId = 1,
                                CreatedBy = "TestUsername",
                                CreatedOn = DateTime.Now,
                                Description = "Pepper"
                            }
                        },
            Instructions = new List<Instruction>
                        {
                            new Instruction
                            {
                                InstructionId = 1,
                                CreatedBy = "TestUsername",
                                CreatedOn = DateTime.Now,
                                StepNumber = 1,
                                Description = "Shake and bake."
                            }
                          },
            CreatedBy = "TestUsername",
            CreatedOn = DateTime.Now
        };

        public RecipeSubscription ExpectedSubscription =
            new RecipeSubscription
            {
                ApplicationUserId = "TestUsername",
                CreationDate = DateTime.Now,
                LastModifed = DateTime.Now,
                IsSubscribed = true
            };
    }
}

