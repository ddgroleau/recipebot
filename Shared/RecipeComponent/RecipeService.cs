using Recipebot.Shared.Common;
using Recipebot.Shared.DOM_Events;
using Recipebot.Shared.SubscriptionComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.RecipeComponent
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ISubscriberState _subscriberState;

        public RecipeService(IRecipeRepository recipeRepository, ISubscriberState subscriberState)
        {
            _recipeRepository = recipeRepository;
            _subscriberState = subscriberState;
        }
  
        public async Task<int> CreateRecipe(IRecipeDTO recipeDTO)
        {
            int createdId;

            if (RecipeIsValid(recipeDTO))
            {
                createdId = await SaveRecipe(recipeDTO);
            }
            else
            {
                throw new InvalidOperationException();
            }
            return createdId;
        }

        public IEnumerable<IRecipeDTO> SearchRecipes(string searchText)
        {
            List<IRecipeDTO> recipeResults = new List<IRecipeDTO>();
            var searchResults = _recipeRepository.SearchRecipes(searchText);
            
            foreach(var recipe in searchResults)
            {
                recipeResults.Add(recipe);
            }
            return recipeResults;        
        }

        public async Task<IEnumerable<IRecipeDTO>> GetUserRecipes()
        {
            List<IRecipeDTO> userRecipes = new List<IRecipeDTO>();
            var RecipeDTOs = await _recipeRepository.GetUserRecipes();
            foreach (var recipeDTO in RecipeDTOs)
            {
                userRecipes.Add(recipeDTO);
            }
            return userRecipes;
        }

        private async Task<int> SaveRecipe(IRecipeDTO recipeModel)
        {
            int createdId;

            try
            {
                if (await RecipeExists(recipeModel.RecipeId))
                {
                   await _recipeRepository.UpdateRecipe(recipeModel);
                }
                else
                {
                    await _recipeRepository.CreateRecipe(recipeModel);
                }

                createdId = await _recipeRepository.FindRecipe(recipeModel);

                _subscriberState.UpdateState();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to Add Recipe to Database.", e);
            }
            
            return createdId;
        }

        private static bool RecipeIsValid(IRecipeDTO RecipeDTO)
        {
            bool isValid;
            try
            {
                var validationContext = new ValidationContext(RecipeDTO);

                isValid = Validator.TryValidateObject(RecipeDTO, validationContext, new List<ValidationResult>(), true);
            }
            catch (Exception)
            {
                throw;
            }
            return isValid;
        }

        private async Task<bool> RecipeExists(int recipeId)
        {
            var recipe = await _recipeRepository.FindRecipeById(recipeId);
            return recipe.RecipeId > 0;
        }

    }
}
