using PBC.Shared.DOM_Events;
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
        private readonly IRecipeBuilder _recipeBuilder;
        private readonly IRecipeRepository _recipeRepository;
        public RecipeService(IRecipeBuilder recipeBuilder, IRecipeRepository recipeRepository)
        {
            _recipeBuilder = recipeBuilder;
            _recipeRepository = recipeRepository;
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

        private IRecipeServiceDTO SaveRecipe(IRecipeServiceDTO recipeModel)
        {
            try
            {
                _recipeRepository.InsertOne(recipeModel);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to Add Recipe to Database.",e);
            }
            return recipeModel;
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
            var recipeResults = new List<IRecipeDTO>();
            var searchResults = _recipeRepository.FindMany(searchText);
            
            foreach(var recipe in searchResults)
            {
                var recipeResult =_recipeBuilder.Build(recipe);
                recipeResults.Add(recipeResult);
            }

            return recipeResults;        
        }

    }
}
