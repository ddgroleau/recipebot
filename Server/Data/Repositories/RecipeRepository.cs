using Microsoft.EntityFrameworkCore;
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
                Recipe recipe = BuildRecipe(recipeServiceDTO);
                await _dbContext.Recipes.AddAsync(recipe);
                await _dbContext.SaveChangesAsync();

                return;
        }

        public IEnumerable<IRecipeServiceDTO> SearchRecipes(string searchText)
        {
            var searchResults = new List<IRecipeServiceDTO>();

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
                var recipeServiceDTO = BuildRecipeServiceDTO(recipe);
                searchResults.Add(recipeServiceDTO);
            }

            return searchResults;
        }

        public Task<int> FindRecipe(IRecipeServiceDTO recipeServiceDTO)
        {
            int recipeId = _dbContext.Recipes
                .AsNoTracking()
                .Where(x =>
                    x.Title.Equals(recipeServiceDTO.Title)
                    && x.Description.Equals(recipeServiceDTO.Description)
                    && x.URL.Equals(recipeServiceDTO.URL)
                    && x.RecipeType.Equals(recipeServiceDTO.RecipeType))
                .Select(x => x.RecipeId)
                .FirstOrDefault();

            return Task.FromResult(recipeId);
        }

        public async Task<IRecipeServiceDTO> FindRecipeById(int id)
        {
            var recipe = await _dbContext.Recipes
                .Include(x => x.Ingredients)
                .Include(x => x.Instructions)
                .Where(x => x.RecipeId == id)
                .FirstOrDefaultAsync()
                ;

            var recipeServiceDTO = BuildRecipeServiceDTO(recipe);

            return recipeServiceDTO;
        }

        public async Task UpdateRecipe(IRecipeServiceDTO recipeServiceDTO)
        {
            var entity = await _dbContext.Recipes.FindAsync(recipeServiceDTO.RecipeId);
            
            if (entity == null) throw new InvalidOperationException("This recipe does not exist.");

            entity.Title = recipeServiceDTO.Title;
            entity.RecipeType = recipeServiceDTO.RecipeType;
            entity.Description = recipeServiceDTO.Description;
            entity.URL = recipeServiceDTO.URL;
            
            for(int i = 0; i < recipeServiceDTO.Ingredients.Count; i++)
            {
                var entityIngredient = entity.Ingredients.ElementAtOrDefault(i);

                if (entityIngredient == null)
                {
                    Ingredient ingredient = _ingredientFactory.Make();
                    ingredient.Description = recipeServiceDTO.Ingredients[i];
                    ingredient.CreatedBy = _userState.GetCurrentUserName();
                    ingredient.CreatedOn = DateTime.Now;
                    entity.Ingredients.Add(ingredient);
                }
                else
                {
                    entityIngredient.Description = recipeServiceDTO.Ingredients[i];
                }
            }
            
            for (int i = 0; i < recipeServiceDTO.Instructions.Count; i++)
            {
                var entityInstruction = entity.Instructions.ElementAtOrDefault(i);

                if (entityInstruction == null)
                {
                    Instruction instruction = _instructionFactory.Make();
                    instruction.Description = recipeServiceDTO.Instructions[i];
                    instruction.CreatedBy = _userState.GetCurrentUserName();
                    instruction.CreatedOn = DateTime.Now;
                    instruction.StepNumber = i + 1;
                    entity.Instructions.Add(instruction);
                }
                else
                {
                    entityInstruction.Description = recipeServiceDTO.Instructions[i];
                }
            }

            await _dbContext.SaveChangesAsync();

            return;
        }

        public async Task<IEnumerable<IRecipeServiceDTO>> GetUserRecipes()
        {

            var userRecipes = new List<IRecipeServiceDTO>();

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
                var recipeServiceDTO = BuildRecipeServiceDTO(recipe);

                userRecipes.Add(recipeServiceDTO);
            }

            return userRecipes;
        }

        private IRecipeServiceDTO BuildRecipeServiceDTO(Recipe recipe)
        {
            IRecipeServiceDTO recipeServiceDTO = _recipeFactory.MakeRecipeServiceDTO();

            if (recipe == null) return recipeServiceDTO;

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

        private Recipe BuildRecipe(IRecipeServiceDTO recipeServiceDTO)
        {
            Recipe recipe = _recipeFactory.Make();
            string username = _userState.GetCurrentUserName();
            
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
