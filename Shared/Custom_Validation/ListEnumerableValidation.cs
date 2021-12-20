using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Shared.Custom_Validation
{
    public class ListMustContainDays : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
        {
            var list = value as List<ListDayDTO>;
            bool isNotEmpty = list.Any();

            if (isNotEmpty)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"You must include at least one day of recipes in your list.", new[] { validationContext.MemberName });
        }
    }
}
