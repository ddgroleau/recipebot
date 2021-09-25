using PBC.Shared;
using PBC.Shared.ListComponent;
using PBC.Shared.RecipeComponent;
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
        public IListGeneratorDTO InvalidList {get;}
        public IListDTO ListDTO { get;  }
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
                            ListId = 1,
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
                            ListId = 2,
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
                            ListId = 3,
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
                            Breakfast = new Recipe
                            {
                                Title = "Day1Breakfast",
                                Description = "Day1BreakfastDescription",
                                RecipeType = "Breakfast",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = {
                                    new Ingredient
                                    {
                                        Description = "Salt",
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = "TestUsername"
                                    } 
                                },
                                Instructions = {
                                    new Instruction
                                    { 
                                        Description ="Combine and cook.",
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = "TestUsername"
                                    }
                                }
                            },
                            Lunch = new Recipe
                            {
                                Title = "Day1Lunch",
                                Description = "Day1LunchDescription",
                                RecipeType = "Lunch",
                                URL = "https://allrecipes.com/12345",
                               Ingredients = {
                                    new Ingredient
                                    {
                                        Description = "Salt",
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = "TestUsername"
                                    }
                                },
                                Instructions = {
                                    new Instruction
                                    {
                                        Description ="Combine and cook.",
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = "TestUsername"
                                    }
                                }
                            },
                            Dinner = new Recipe
                            {
                                Title = "Day1Dinner",
                                Description = "Day1DinnerDescription",
                                RecipeType = "Dinner",
                                URL = "https://allrecipes.com/12345",
                               Ingredients = {
                                    new Ingredient
                                    {
                                        Description = "Salt",
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = "TestUsername"
                                    }
                                },
                                Instructions = {
                                    new Instruction
                                    {
                                        Description ="Combine and cook.",
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = "TestUsername"
                                    }
                                }
                            }
                        }
                    },
                    {
                        new ListDay
                        {
                            SequenceNumber = 2,
                            Date = DateTime.Today.AddDays(3),
                                            Breakfast = new Recipe
                            {
                                Title = "Day2Breakfast",
                                Description = "Day2BreakfastDescription",
                                RecipeType = "Breakfast",
                                URL = "https://allrecipes.com/12345",
                               Ingredients = {
                                    new Ingredient
                                    {
                                        Description = "Salt",
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = "TestUsername"
                                    }
                                },
                                Instructions = {
                                    new Instruction
                                    {
                                        Description ="Combine and cook.",
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = "TestUsername",
                                         StepNumber = 1
                                    }
                                }
                            },
                            Lunch = new Recipe
                            {
                                Title = "Day2Lunch",
                                Description = "Day2LunchDescription",
                                RecipeType = "Lunch",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = {
                                    new Ingredient
                                    {
                                        Description = "Salt",
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = "TestUsername"
                                    }
                                },
                                Instructions = {
                                    new Instruction
                                    {
                                        Description ="Combine and cook.",
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = "TestUsername",
                                         StepNumber = 1
                                    }
                                }
                            },
                            Dinner = new Recipe
                            {
                                Title = "Day2Dinner",
                                Description = "Day2DinnerDescription",
                                RecipeType = "Dinner",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = {
                                    new Ingredient
                                    {
                                        Description = "Salt",
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = "TestUsername"
                                    }
                                },
                                Instructions = {
                                    new Instruction
                                    {
                                        Description ="Combine and cook.",
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = "TestUsername",
                                         StepNumber = 1
                                    }
                                }
                            }
                        }
                    },
                    {
                        new ListDay
                        {
                            SequenceNumber = 3,
                            Date = DateTime.Today.AddDays(4),
                                           Breakfast = new Recipe
                            {
                                Title = "Day3Breakfast",
                                Description = "Day3BreakfastDescription",
                                RecipeType = "Breakfast",
                                URL = "https://allrecipes.com/12345",
                               Ingredients = {
                                    new Ingredient
                                    {
                                        Description = "Salt",
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = "TestUsername"
                                    }
                                },
                                Instructions = {
                                    new Instruction
                                    {
                                        Description ="Combine and cook.",
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = "TestUsername",
                                        StepNumber = 1
                                    }
                                }
                            },
                            Lunch = new Recipe
                            {
                                Title = "Day3Lunch",
                                Description = "Day3LunchDescription",
                                RecipeType = "Lunch",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = {
                                    new Ingredient
                                    {
                                        Description = "Salt",
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = "TestUsername"
                                    }
                                },
                                Instructions = {
                                    new Instruction
                                    {
                                        Description ="Combine and cook.",
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = "TestUsername",
                                         StepNumber = 1
                                    }
                                }
                            },
                            Dinner = new Recipe
                            {
                                Title = "Day3Dinner",
                                Description = "Day3DinnerDescription",
                                RecipeType = "Dinner",
                                URL = "https://allrecipes.com/12345",
                                Ingredients = {
                                    new Ingredient
                                    {
                                        Description = "Salt",
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = "TestUsername"
                                    }
                                },
                                Instructions = {
                                    new Instruction
                                    {
                                        Description ="Combine and cook.",
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = "TestUsername",
                                        StepNumber = 1
                                    }
                                }
                            }
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
                            ListId = 1,
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
                        },
                 
                        new ListDayDTO
                        {
                            ListId = 2,
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
       
                    },
           
                        new ListDayDTO
                        {
                            ListId = 3,
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
            };
        }

        
    }
}
