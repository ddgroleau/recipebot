using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.AccountComponent
{
    public interface IAccountRegisterDTO
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public bool Loading { get; set; }
    }
}
