using PBC.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MockObjects
{
    public class MockUserState : IUserState
    {
        public Task<string> CurrentUsernameAsync()
        {
            return Task<string>.Factory.StartNew(() => "TestUsername");
        }

        public Task<string> CurrentUserIdAsync()
        {
            return Task<string>.Factory.StartNew(() => "TestUserId");

        }
        public string GetCurrentUserName()
        { return "test"; }
        public void SetUserName(string userName)
        { }
        public string GetCurrentUserId()
        { return "test"; }
        public void SetUserId(string userId)
        { }

    }
}
