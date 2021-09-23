﻿using Microsoft.EntityFrameworkCore;
using PBC.Shared.Common;
using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Server.Data.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly AbstractRecipeFactory _recipeFactory;
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserState _userState;
        private readonly IFactory<Ingredient> _ingredientFactory;
        private readonly IFactory<Instruction> _instructionFactory;


        public RecipeRepository
            (
            AbstractRecipeFactory recipeFactory, 
            ApplicationDbContext context, 
            IUserState userState, 
            IFactory<Ingredient> ingredientFactory, 
            IFactory<Instruction> instructionFactory
            )
        {
            _recipeFactory = recipeFactory;
            _dbContext = context;
            _userState = userState;
            _ingredientFactory = ingredientFactory;
            _instructionFactory = instructionFactory;
        }

        public async Task CreateRecipe(IRecipeServiceDTO recipeServiceDTO) 
        {
                Recipe recipe = await BuildRecipe(recipeServiceDTO);
                await _dbContext.Recipes.AddAsync(recipe);
                await _dbContext.SaveChangesAsync();

                return;
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

        public async Task<int> FindRecipe(IRecipeServiceDTO recipeServiceDTO)
        {
            return 0;
        }

        public IRecipeServiceDTO FindRecipeById(int id)
        {
            return new RecipeServiceDTO();
        }

        public void UpdateRecipe(IRecipeServiceDTO recipe)
        {
            // Update a recipe in RecipeSubscriptions
        }

        public async Task<IEnumerable<IRecipeServiceDTO>> GetUserRecipes()
        {

            var userRecipes = new List<IRecipeServiceDTO>();

            var userId = await _userState.CurrentUserIdAsync();
            
            var recipes = _dbContext.Recipes.AsNoTracking()
                .Include(recipe => recipe.Ingredients)
                .Include(recipe => recipe.Instructions)
                .Join(_dbContext.RecipeSubscriptions.Where(subscription => subscription.ApplicationUserId.Equals(userId)),
                recipe => recipe.RecipeId,
                subscription => subscription.RecipeId,
                (recipe, subscription) => recipe);
            
            foreach(var recipe in recipes)
            {
                var recipeServiceDTO = BuildRecipeServiceDTO(recipe);

                userRecipes.Add(recipeServiceDTO);
            }

            return userRecipes;
        }

        private IRecipeServiceDTO BuildRecipeServiceDTO(Recipe recipe)
        {
            IRecipeServiceDTO recipeServiceDTO = _recipeFactory.MakeRecipeServiceDTO();

            recipeServiceDTO.RecipeId = recipe.RecipeId;
            recipeServiceDTO.RecipeType = recipe.RecipeType;
            recipeServiceDTO.Title = recipe.Title;
            recipeServiceDTO.Description = recipe.Description;
            recipeServiceDTO.URL = recipe.URL;

            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                recipeServiceDTO.Ingredients.Add(ingredient.Description);
            }

            foreach (Instruction instruction in recipe.Instructions)
            {
                recipeServiceDTO.Instructions.Add(instruction.Description);
            }

            return recipeServiceDTO;
        }

        private async Task<Recipe> BuildRecipe(IRecipeServiceDTO recipeServiceDTO)
        {
            Recipe recipe = _recipeFactory.Make();
            string username = await _userState.CurrentUsernameAsync();
            
            recipe.RecipeType = recipeServiceDTO.RecipeType;
            recipe.Title = recipeServiceDTO.Title;
            recipe.Description = recipeServiceDTO.Description;
            recipe.URL = recipeServiceDTO.URL;
            recipe.CreatedBy = username;
            recipe.CreatedOn = DateTime.Now;

            foreach (string ingredientDescription in recipeServiceDTO.Ingredients)
            {
                Ingredient ingredient = _ingredientFactory.Make();
                ingredient.Description = ingredientDescription;
                ingredient.CreatedBy = username;
                ingredient.CreatedOn = DateTime.Now;

                recipe.Ingredients.Add(ingredient);
            }

            for (int i = 0; i < recipeServiceDTO.Instructions.Count; i++)
            {
                Instruction instruction = _instructionFactory.Make();
                instruction.Description = recipeServiceDTO.Instructions[i];
                instruction.CreatedBy = username;
                instruction.CreatedOn = DateTime.Now;
                instruction.StepNumber = i + 1;

                recipe.Instructions.Add(instruction);
            }

            return recipe;
        }

    }
}
