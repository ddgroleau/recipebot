using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PBC.Shared.Custom_Validation
{
    public class RegisterPassword : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
        {
                string input = value.ToString();
                Regex specialChars = new Regex("[^A-Za-z0-9]");
                
                bool containsSpecialCharacter = specialChars.IsMatch(input);
                bool containsUpperLowerDigit = input.Any(char.IsLower) &&
                                               input.Any(char.IsUpper) &&
                                               input.Any(char.IsDigit) &&
                                               input.Length > 7;

                if (containsSpecialCharacter && containsUpperLowerDigit)
                {
                    return ValidationResult.Success;
                }
  
            return new ValidationResult("Password must have at least one lowercase, " +
                                                                    "one uppercase, " + 
                                                                    "one digit, " + 
                                                                    "one special character " +
                                                                    "and be at least eight characters.", 
                                            new[] { validationContext.MemberName 
                                        });
        }
    }
}