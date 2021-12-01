using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.Common
{
    public interface IUserState
    {
        public string GetCurrentUserName();
        public void SetCurrentUserName(string userName);
        public string GetCurrentUserId();
        public void SetCurrentUserId(string userId);

    }
}
