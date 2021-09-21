using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Shared.Common
{
    public class UserState : IUserState
    {

        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<ClaimsPrincipal> _userManager;

        public UserState(IHttpContextAccessor accessor, UserManager<ClaimsPrincipal> userManager)
        {
            _accessor = accessor;
            _userManager = userManager;
        }

        public async Task<string> CurrentUsernameAsync()
        {
             ClaimsPrincipal claimsPrincipal = _accessor.HttpContext.User;
             return await _userManager.GetUserNameAsync(claimsPrincipal);
        }

        public async Task<string> CurrentUserIdAsync()
        {
            ClaimsPrincipal claimsPrincipal = _accessor.HttpContext.User;
            return await _userManager.GetUserIdAsync(claimsPrincipal);
        }
    }
}
