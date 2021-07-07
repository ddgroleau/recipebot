using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.AccountComponent
{
    public interface IAccountLoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Loading { get; set; }
    }
}
