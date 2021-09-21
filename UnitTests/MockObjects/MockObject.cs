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
            Title = $"Recipe1",
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
    }
}

