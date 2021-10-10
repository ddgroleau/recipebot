﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PBC.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PBC.Server.Models
{
    public class UserState : IUserState
    {

        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserState(IHttpContextAccessor accessor, UserManager<ApplicationUser> userManager)
        {
            _accessor = accessor;
            _userManager = userManager;
        }

        public async Task<string> CurrentUsernameAsync()
        {
            var username = _accessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);
            return user.UserName;

        }

        public async Task<string> CurrentUserIdAsync()
        {
            var username = _accessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);
            return user.Id;
        }
    }
}