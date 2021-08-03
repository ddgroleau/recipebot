using PBC.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.Custom_Validation
{
    public class  MustBeValidType : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
        {
            var recipeTypes = new RecipeTypes();

            try
            {
                string type = recipeTypes.GetRecipeType(value.ToString());
                return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult($"Invalid recipe type.", new[] { validationContext.MemberName });
            }

        }
    }
}
