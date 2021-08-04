using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.Custom_Validation
{
    public class  ListMustContainElements : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
        {
            var list = value as List<string>;
            bool isNotEmpty = list.Where(x => !String.IsNullOrEmpty(x)).Any();
         
            if (isNotEmpty)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"You must include at least one ingredient and one instruction.", new[] { validationContext.MemberName });
        }
    }
}
