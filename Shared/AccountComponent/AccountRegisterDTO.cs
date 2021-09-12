using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PBC.Shared.Custom_Validation;

namespace PBC.Shared.AccountComponent
{
    public class AccountRegisterDTO : IAccountRegisterDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, DataType(DataType.Password), RegisterPassword]
        public string Password { get; set; }
        [Required, Compare("Password", ErrorMessage = "Password and Confirmation Password must match."), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
