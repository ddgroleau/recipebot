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
        private readonly IRepository<IRecipeEntity> _recipeRepository;
        public RecipeService(IRecipeBuilder recipeBuilder, IRepository<IRecipeEntity> recipeRepository)
        {
            _recipeBuilder = recipeBuilder;
            _recipeRepository = recipeRepository;
        }
        public bool RecipeIsValid(IRecipeDTO recipeDTO)
        {
            bool isValid;

            var validationContext = new ValidationContext(recipeDTO);

            isValid = Validator.TryValidateObject(recipeDTO, validationContext, new List<ValidationResult>());

            return isValid;
        }
        public IRecipeModel CreateRecipeModel(IRecipeDTO recipeDTO)
        {
            IRecipeModel recipeModel;

            if (RecipeIsValid(recipeDTO))
            {
                recipeModel = _recipeBuilder.Build(recipeDTO);
            }
            else
            {
                throw new InvalidOperationException();
            }

            return recipeModel;
        }

        public IRecipeModel SaveRecipe(IRecipeModel recipeModel)
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
    }
}
