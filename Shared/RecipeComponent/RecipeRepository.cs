using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
{
    public class RecipeRepository : IRecipeRepository
    {
        public void CreateRecipe(IRecipeServiceDTO recipe) 
        {
            // Add a recipe to recipe table
            // Add a recipe to RecipeSubscriptions
        }
        public IEnumerable<IRecipeServiceDTO> SearchRecipes(string text)
        {
            var recipes = new List<IRecipeServiceDTO> //STUB
            {
                new RecipeServiceDTO { RecipeId = 11,Title =  $"Recipe11", Description = "Description11",  RecipeType="Dinner",    Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeServiceDTO { RecipeId = 12,Title =  $"Recipe12", Description = "Description12",  RecipeType="Dinner",    Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeServiceDTO { RecipeId = 13, Title = $"Recipe13", Description = "Description13",  RecipeType="Breakfast", Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeServiceDTO { RecipeId = 14, Title = $"Recipe14", Description = "Description14",  RecipeType="Breakfast", Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeServiceDTO { RecipeId = 15, Title = $"Recipe15", Description = "Description15",  RecipeType="Lunch",     Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeServiceDTO { RecipeId = 16, Title = $"Recipe16", Description = "Description16",  RecipeType="Lunch",     Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeServiceDTO { RecipeId = 17, Title = $"Recipe17", Description = "Description17",  RecipeType="Lunch",     Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeServiceDTO { RecipeId = 18, Title = $"Recipe18", Description = "Description18",  RecipeType="Lunch",     Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeServiceDTO { RecipeId = 19, Title = $"Recipe19", Description = "Description19",  RecipeType="Dinner",    Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeServiceDTO { RecipeId = 20, Title = $"Recipe20", Description = "Description20",  RecipeType="Dinner",    Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeServiceDTO { RecipeId = 21, Title = $"Recipe21", Description = "Description21",  RecipeType="Dinner",    Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
                new RecipeServiceDTO { RecipeId = 22, Title = $"Recipe22", Description = "Description22",  RecipeType="Dinner",    Ingredients={ "Salt" }, Instructions={"Combine and cook."} },
            };
            return recipes;
        }
        public IRecipeServiceDTO FindOne(int id)
        {
            return new RecipeServiceDTO();
        }
        public void UpdateRecipe(IRecipeServiceDTO recipe)
        {
            // Update a recipe in RecipeSubscriptions
        }


    }
}
