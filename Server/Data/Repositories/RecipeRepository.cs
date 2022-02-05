using Microsoft.EntityFrameworkCore;
using Recipebot.Shared.Common;
using Recipebot.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Server.Data.Repositories
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

        public async Task CreateRecipe(IRecipeDTO RecipeDTO) 
        {
                Recipe recipe = BuildRecipe(RecipeDTO);
                await _dbContext.Recipes.AddAsync(recipe);
                await _dbContext.SaveChangesAsync();

                return;
        }

        public IEnumerable<IRecipeDTO> SearchRecipes(string searchText)
        {
            var searchResults = new List<IRecipeDTO>();

            var recipes = _dbContext.Recipes
                .Include(x => x.Ingredients)
                .Include(x => x.Instructions)
                .Where(x => 
                !string.IsNullOrEmpty(searchText.Trim()) && (
                   x.Title.ToLower().Contains(searchText.ToLower())
                || x.Description.ToLower().Contains(searchText.ToLower())
                || x.URL.ToLower().Contains(searchText.ToLower())
                || x.RecipeType.ToLower().Contains(searchText.ToLower())
                || x.Ingredients.Where(x=> x.Description.ToLower().Contains(searchText.ToLower())).Any()
                || x.Instructions.Where(x => x.Description.ToLower().Contains(searchText.ToLower())).Any()
                ));
            
            foreach(var recipe in recipes)
            {
                var RecipeDTO = BuildRecipeDTO(recipe);
                searchResults.Add(RecipeDTO);
            }

            return searchResults;
        }

        public Task<int> FindRecipe(IRecipeDTO RecipeDTO)
        {
            int recipeId = _dbContext.Recipes
                .AsNoTracking()
                .Where(x =>
                    x.Title.Equals(RecipeDTO.Title)
                    && x.Description.Equals(RecipeDTO.Description)
                    && x.URL.Equals(RecipeDTO.URL)
                    && x.RecipeType.Equals(RecipeDTO.RecipeType))
                .Select(x => x.RecipeId)
                .FirstOrDefault();

            return Task.FromResult(recipeId);
        }

        public async Task<IRecipeDTO> FindRecipeById(int id)
        {
            var recipe = await _dbContext.Recipes
                .Include(x => x.Ingredients)
                .Include(x => x.Instructions)
                .Where(x => x.RecipeId == id)
                .FirstOrDefaultAsync()
                ;

            var RecipeDTO = BuildRecipeDTO(recipe);

            return RecipeDTO;
        }

        public async Task UpdateRecipe(IRecipeDTO RecipeDTO)
        {
            var entity = await _dbContext.Recipes.FindAsync(RecipeDTO.RecipeId);
            
            if (entity == null) throw new InvalidOperationException("This recipe does not exist.");

            entity.Title = RecipeDTO.Title;
            entity.RecipeType = RecipeDTO.RecipeType;
            entity.Description = RecipeDTO.Description;
            entity.URL = RecipeDTO.URL;
            
            for(int i = 0; i < RecipeDTO.Ingredients.Count; i++)
            {
                var entityIngredient = entity.Ingredients.ElementAtOrDefault(i);

                if (entityIngredient == null)
                {
                    Ingredient ingredient = _ingredientFactory.Make();
                    ingredient.Description = RecipeDTO.Ingredients[i];
                    ingredient.CreatedBy = _userState.GetCurrentUserName();
                    ingredient.CreatedOn = DateTime.Now;
                    entity.Ingredients.Add(ingredient);
                }
                else
                {
                    entityIngredient.Description = RecipeDTO.Ingredients[i];
                }
            }
            
            for (int i = 0; i < RecipeDTO.Instructions.Count; i++)
            {
                var entityInstruction = entity.Instructions.ElementAtOrDefault(i);

                if (entityInstruction == null)
                {
                    Instruction instruction = _instructionFactory.Make();
                    instruction.Description = RecipeDTO.Instructions[i];
                    instruction.CreatedBy = _userState.GetCurrentUserName();
                    instruction.CreatedOn = DateTime.Now;
                    instruction.StepNumber = i + 1;
                    entity.Instructions.Add(instruction);
                }
                else
                {
                    entityInstruction.Description = RecipeDTO.Instructions[i];
                }
            }

            await _dbContext.SaveChangesAsync();

            return;
        }

        public async Task<IEnumerable<IRecipeDTO>> GetUserRecipes()
        {

            var userRecipes = new List<IRecipeDTO>();

            var userId = _userState.GetCurrentUserId();

            var recipes = await _dbContext.Recipes.AsNoTracking()
                .Include(recipe => recipe.Ingredients)
                .Include(recipe => recipe.Instructions)
                .Join(_dbContext.RecipeSubscriptions.Where(subscription => subscription.ApplicationUserId.Equals(userId)),
                recipe => recipe.RecipeId,
                subscription => subscription.RecipeId,
                (recipe, subscription) => recipe)
                .ToListAsync();
            
            foreach(var recipe in recipes)
            {
                var RecipeDTO = BuildRecipeDTO(recipe);

                userRecipes.Add(RecipeDTO);
            }

            return userRecipes;
        }

        private IRecipeDTO BuildRecipeDTO(Recipe recipe)
        {
            IRecipeDTO RecipeDTO = _recipeFactory.MakeRecipeDTO();

            if (recipe == null) return RecipeDTO;

            RecipeDTO.RecipeId = recipe.RecipeId;
            RecipeDTO.RecipeType = recipe.RecipeType;
            RecipeDTO.Title = recipe.Title;
            RecipeDTO.Description = recipe.Description;
            RecipeDTO.URL = recipe.URL;

            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                RecipeDTO.Ingredients.Add(ingredient.Description);
            }

            foreach (Instruction instruction in recipe.Instructions)
            {
                RecipeDTO.Instructions.Add(instruction.Description);
            }

            return RecipeDTO;
        }

        private Recipe BuildRecipe(IRecipeDTO RecipeDTO)
        {
            Recipe recipe = _recipeFactory.Make();
            string username = _userState.GetCurrentUserName();
            
            recipe.RecipeType = RecipeDTO.RecipeType;
            recipe.Title = RecipeDTO.Title;
            recipe.Description = RecipeDTO.Description;
            recipe.URL = RecipeDTO.URL;
            recipe.CreatedBy = username;
            recipe.CreatedOn = DateTime.Now;

            foreach (string ingredientDescription in RecipeDTO.Ingredients)
            {
                Ingredient ingredient = _ingredientFactory.Make();
                ingredient.Description = ingredientDescription;
                ingredient.CreatedBy = username;
                ingredient.CreatedOn = DateTime.Now;

                recipe.Ingredients.Add(ingredient);
            }

            for (int i = 0; i < RecipeDTO.Instructions.Count; i++)
            {
                Instruction instruction = _instructionFactory.Make();
                instruction.Description = RecipeDTO.Instructions[i];
                instruction.CreatedBy = username;
                instruction.CreatedOn = DateTime.Now;
                instruction.StepNumber = i + 1;

                recipe.Instructions.Add(instruction);
            }

            return recipe;
        }

    }
}
