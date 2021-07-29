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

        public RecipeService(IRecipeBuilder recipeBuilder)
        {
            _recipeBuilder = recipeBuilder;
        }
        public bool RecipeIsValid(IRecipeDTO recipeDTO)
        {
            bool isValid;
            try
            {
                var validationContext = new ValidationContext(recipeDTO);

                isValid = Validator.TryValidateObject(recipeDTO, validationContext, new List<ValidationResult>());
            }
            catch (Exception)
            {
                throw;
            }
            return isValid;
        }

        public IRecipeModel CreateRecipeModel(IRecipeDTO recipeDTO)
        {
                IRecipeModel recipeModel = null;
            
                if (RecipeIsValid(recipeDTO))
                {
                    recipeModel = _recipeBuilder.Build(recipeDTO);
                }
         
            return recipeModel;
        }
    }
}
