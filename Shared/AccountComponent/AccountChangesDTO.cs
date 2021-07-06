using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared
{
    public class AccountChangesDTO
    {
        private int Id { get; set; }
        
        [Required]
        public string Username { get; set; }

        [Required]
        public string CurrentEmail { get; set; }

        [Required]
        public string NewEmail { get; set; }

        [Required]
        public string ConfirmNewEmail { get; set; }

        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string ConfirmNewPassword { get; set; }
    }
}
