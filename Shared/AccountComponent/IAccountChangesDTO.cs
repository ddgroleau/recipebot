using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.AccountComponent
{
    public interface IAccountChangesDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string CurrentEmail { get; set; }
        public string NewEmail { get; set; }
        public string ConfirmNewEmail { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
