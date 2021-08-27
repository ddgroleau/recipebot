using PBC.Shared.Common;
using PBC.Shared.DOM_Events;
using PBC.Shared.SubscriptionComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.RecipeComponent
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
  
        public IRecipeServiceDTO CreateRecipe(IRecipeDTO recipeDTO)
        {
            IRecipeServiceDTO recipeModel;

            if (RecipeIsValid(recipeDTO))
            {
                recipeModel = _recipeBuilder.Build(recipeDTO);
                SaveRecipe(recipeModel);
            }
            else
            {
                throw new InvalidOperationException();
            }
            return recipeModel;
        }

        private void SaveRecipe(IRecipeServiceDTO recipeModel)
        {
            try
            {
                if(RecipeExists(recipeModel))
                {
                    _recipeRepository.UpdateRecipe(recipeModel);
                }
                else
                {
                    _recipeRepository.CreateRecipe(recipeModel);
                }
                _subscriberState.UpdateState();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to Add Recipe to Database.",e);
            }
        }

        private bool RecipeIsValid(IRecipeDTO recipeDTO)
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

        public IEnumerable<IRecipeDTO> SearchRecipes(string searchText)
        {
            List<IRecipeDTO> recipeResults = new List<IRecipeDTO>();
            List<IRecipeServiceDTO> searchResults = _recipeRepository.SearchRecipes(searchText).ToList();
            
            foreach(var recipe in searchResults)
            {
                var recipeResult =_recipeBuilder.Build(recipe);
                recipeResults.Add(recipeResult);
            }
            return recipeResults;        
        }

        private bool RecipeExists(IRecipeServiceDTO recipeServiceDTO)
        {
            return _recipeRepository.FindOne(recipeServiceDTO.RecipeId) != null;
        }

    }
}
