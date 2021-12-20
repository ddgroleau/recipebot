using Recipebot.Shared.RecipeComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.Custom_Validation
{
    public class MustBeValidType : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
        {
            try
            {
                var recipeTypes = new RecipeTypes();
                string type = recipeTypes.GetRecipeType(value.ToString());

                if (!type.Equals("Invalid"))
                {
                    return ValidationResult.Success;
                }
            }
            catch (NullReferenceException)
            {
                return new ValidationResult($"Invalid recipe type.", new[] { validationContext.MemberName });
            }
            return new ValidationResult($"Invalid recipe type.", new[] { validationContext.MemberName });
        }
    }
}
