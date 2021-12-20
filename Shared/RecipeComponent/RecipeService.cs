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
        private readonly IBuilder<IRecipeServiceDTO, IRecipeDTO> _recipeBuilder;
        private readonly IRecipeRepository _recipeRepository;
        private readonly ISubscriberState _subscriberState;

        public RecipeService(IBuilder<IRecipeServiceDTO, IRecipeDTO> recipeBuilder, IRecipeRepository recipeRepository, ISubscriberState subscriberState)
        {
            _recipeBuilder = recipeBuilder;
            _recipeRepository = recipeRepository;
            _subscriberState = subscriberState;
        }
  
        public async Task<int> CreateRecipe(IRecipeDTO recipeDTO)
        {
            int createdId;

            if (RecipeIsValid(recipeDTO))
            {
                var recipeModel = _recipeBuilder.Build(recipeDTO);
                createdId = await SaveRecipe(recipeModel);
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
                var recipeResult =_recipeBuilder.Build(recipe);
                recipeResults.Add(recipeResult);
            }
            return recipeResults;        
        }

        public async Task<IEnumerable<IRecipeDTO>> GetUserRecipes()
        {
            List<IRecipeDTO> userRecipes = new List<IRecipeDTO>();
            var recipeServiceDTOs = await _recipeRepository.GetUserRecipes();
            foreach (var recipeDTO in recipeServiceDTOs)
            {
                var userRecipe = _recipeBuilder.Build(recipeDTO);
                userRecipes.Add(userRecipe);
            }
            return userRecipes;
        }

        private async Task<int> SaveRecipe(IRecipeServiceDTO recipeModel)
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

        private static bool RecipeIsValid(IRecipeDTO recipeDTO)
        {
            bool isValid;
            try
            {
                var validationContext = new ValidationContext(recipeDTO);

                isValid = Validator.TryValidateObject(recipeDTO, validationContext, new List<ValidationResult>(), true);
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
