using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PBC.Shared.Custom_Validation
{
    public class AcceptableURLAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
        {
            //To Do: Find a cleaner way to perform validation.
            bool isValidBlankURL = String.IsNullOrEmpty(value.ToString());
            bool isValidAllRecipesURL = value.ToString().StartsWith("https://www.allrecipes.com/recipe/");

            if (isValidBlankURL || isValidAllRecipesURL)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"Sorry! We haven't added \"{value}\" to our app yet. Try another site.", new[] { validationContext.MemberName });
        }
    }
}