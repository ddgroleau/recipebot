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
        public string GetCurrentUserName() => "TestUsername";
        public void SetCurrentUserName(string userName) { }
        public string GetCurrentUserId() => "TestUserId";
        public void SetCurrentUserId(string userId) { }

    }
}
