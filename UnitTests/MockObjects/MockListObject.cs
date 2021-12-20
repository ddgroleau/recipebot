using Recipebot.Shared;
using Recipebot.Shared.ListComponent;
using Recipebot.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MockObjects
{
    public class MockListObject
    {
        public IListGeneratorDTO GeneratedList { get; }
        public IListGeneratorDTO InvalidList { get; }
        public IListDTO ListDTO { get; }
        public ListEntity ListEntity { get; }

        public MockListObject()
        {
            InvalidList = new ListGeneratorDTO();
            GeneratedList = new ListGeneratorDTO
            {
                Days = 3,
                GeneratedDays =
                {
                    {
                        new ListDayDTO
                        {
                            ListEntityId = 1,
                            SequenceNumber = 1,
                            Date = DateTime.Today.AddDays(2),
                            Breakfast = new RecipeDTO
                            {
                                Title = "Day1Breakfast",
                                Description = "Day1BreakfastDescription",
                                RecipeType = "Breakfast",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            },
                            Lunch = new RecipeDTO
                            {
                                Title = "Day1Lunch",
                                Description = "Day1LunchDescription",
                                RecipeType = "Lunch",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            },
                            Dinner = new RecipeDTO
                            {
                                Title = "Day1Dinner",
                                Description = "Day1DinnerDescription",
                                RecipeType = "Dinner",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            }
                        }
                    },
                    {
                        new ListDayDTO
                        {
                            ListEntityId = 2,
                            SequenceNumber = 2,
                            Date = DateTime.Today.AddDays(3),
                                            Breakfast = new RecipeDTO
                            {
                                Title = "Day2Breakfast",
                                Description = "Day2BreakfastDescription",
                                RecipeType = "Breakfast",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            },
                            Lunch = new RecipeDTO
                            {
                                Title = "Day2Lunch",
                                Description = "Day2LunchDescription",
                                RecipeType = "Lunch",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            },
                            Dinner = new RecipeDTO
                            {
                                Title = "Day2Dinner",
                                Description = "Day2DinnerDescription",
                                RecipeType = "Dinner",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            }
                        }
                    },
                    {
                        new ListDayDTO
                        {
                            ListEntityId = 3,
                            SequenceNumber = 3,
                            Date = DateTime.Today.AddDays(4),
                                           Breakfast = new RecipeDTO
                            {
                                Title = "Day3Breakfast",
                                Description = "Day3BreakfastDescription",
                                RecipeType = "Breakfast",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            },
                            Lunch = new RecipeDTO
                            {
                                Title = "Day3Lunch",
                                Description = "Day3LunchDescription",
                                RecipeType = "Lunch",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            },
                            Dinner = new RecipeDTO
                            {
                                Title = "Day3Dinner",
                                Description = "Day3DinnerDescription",
                                RecipeType = "Dinner",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            }
                        }
                    }
                }
            };

            ListEntity = new ListEntity
            {
                Days = 3,
                CreatedBy = "TestUsername",
                CreatedOn = DateTime.Now,
                ListDays =
                {
                    {
                        new ListDay
                        {
                            SequenceNumber = 1,
                            Date = DateTime.Today.AddDays(2),
                            BreakfastRecipeId = 1,
                            LunchRecipeId = 2,
                            DinnerRecipeId = 3

                        }
                    },
                    {
                        new ListDay
                        {
                            SequenceNumber = 2,
                            Date = DateTime.Today.AddDays(3),
                            BreakfastRecipeId = 4,
                            LunchRecipeId = 5,
                            DinnerRecipeId = 6
                        }
                    },
                    {
                        new ListDay
                        {
                            SequenceNumber = 3,
                            Date = DateTime.Today.AddDays(4),
                             BreakfastRecipeId = 7,
                            LunchRecipeId = 8,
                            DinnerRecipeId = 9
                        }
                    }
                }
            };

            ListDTO = new ListDTO
            {
                Days = 3,
                ListDays = new List<IListDayDTO>
                {
                        new ListDayDTO
                        {
                            SequenceNumber = 1,
                            Date = DateTime.Today.AddDays(2),
                            Breakfast = new RecipeDTO
                            {
                                RecipeId = 1,
                                Title = "Day1Breakfast",
                                Description = "Day1BreakfastDescription",
                                RecipeType = "Breakfast",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            },
                            Lunch = new RecipeDTO
                            {
                                RecipeId = 2,
                                Title = "Day1Lunch",
                                Description = "Day1LunchDescription",
                                RecipeType = "Lunch",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            },
                            Dinner = new RecipeDTO
                            {
                                RecipeId = 3,
                                Title = "Day1Dinner",
                                Description = "Day1DinnerDescription",
                                RecipeType = "Dinner",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            }
                        },
                        new ListDayDTO
                        {
                            SequenceNumber = 2,
                            Date = DateTime.Today.AddDays(3),
                            Breakfast = new RecipeDTO
                            {
                                RecipeId = 4,
                                Title = "Day2Breakfast",
                                Description = "Day2BreakfastDescription",
                                RecipeType = "Breakfast",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            },
                            Lunch = new RecipeDTO
                            {
                                RecipeId = 5,
                                Title = "Day2Lunch",
                                Description = "Day2LunchDescription",
                                RecipeType = "Lunch",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            },
                            Dinner = new RecipeDTO
                            {
                                RecipeId = 6,
                                Title = "Day2Dinner",
                                Description = "Day2DinnerDescription",
                                RecipeType = "Dinner",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            }
                        },
                        new ListDayDTO
                        {
                            SequenceNumber = 3,
                            Date = DateTime.Today.AddDays(4),
                                           Breakfast = new RecipeDTO
                            {
                                RecipeId = 7,
                                Title = "Day3Breakfast",
                                Description = "Day3BreakfastDescription",
                                RecipeType = "Breakfast",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            },
                            Lunch = new RecipeDTO
                            {
                                RecipeId = 8,
                                Title = "Day3Lunch",
                                Description = "Day3LunchDescription",
                                RecipeType = "Lunch",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            },
                            Dinner = new RecipeDTO
                            {
                                RecipeId = 9,
                                Title = "Day3Dinner",
                                Description = "Day3DinnerDescription",
                                RecipeType = "Dinner",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            }

                    }
                }
            };

        }

    }
}
