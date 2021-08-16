using PBC.Shared;
using PBC.Shared.ListComponent;
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

        public MockListObject()
        {
            GeneratedList = new ListGeneratorDTO
            {
                Days = 3,
                GeneratedDays =
                {
                    {
                        new ListDayDTO
                        {
                            ListId = 1,
                            SequenceNumber = 1,
                            Date = DateTime.Today.AddDays(2),
                            Breakfast = new RecipeDTO
                            {
                                Title = "Day1Breakfast",
                                Description = "Day1BreakfastDescription",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            },
                            Lunch = new RecipeDTO
                            {
                                Title = "Day1Lunch",
                                Description = "Day1LunchDescription",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            },
                            Dinner = new RecipeDTO
                            {
                                Title = "Day1Dinner",
                                Description = "Day1DinnerDescription",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            }
                        }
                    },
                    {
                        new ListDayDTO
                        {
                            ListId = 2,
                            SequenceNumber = 2,
                            Date = DateTime.Today.AddDays(3),
                                            Breakfast = new RecipeDTO
                            {
                                Title = "Day2Breakfast",
                                Description = "Day2BreakfastDescription",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            },
                            Lunch = new RecipeDTO
                            {
                                Title = "Day2Lunch",
                                Description = "Day2LunchDescription",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            },
                            Dinner = new RecipeDTO
                            {
                                Title = "Day2Dinner",
                                Description = "Day2DinnerDescription",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            }
                        }
                    },
                    {
                        new ListDayDTO
                        {
                            ListId = 3,
                            SequenceNumber = 3,
                            Date = DateTime.Today.AddDays(4),
                                           Breakfast = new RecipeDTO
                            {
                                Title = "Day3Breakfast",
                                Description = "Day3BreakfastDescription",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            },
                            Lunch = new RecipeDTO
                            {
                                Title = "Day3Lunch",
                                Description = "Day3LunchDescription",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            },
                            Dinner = new RecipeDTO
                            {
                                Title = "Day3Dinner",
                                Description = "Day3DinnerDescription",
                                Ingredients = { { "Salt" } },
                                Instructions = {{ "Combine and cook." }}
                            }
                        }
                    }
                }
            };
        }
    }
}
