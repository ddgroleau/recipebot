using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Recipebot.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Recipebot.Server.Models
{
    public class UserState : IUserState
    {

        private string _userName { get; set; }
        private string _userId { get; set; }    
        public string GetCurrentUserName() => _userName;
        public void SetCurrentUserName(string userName) =>_userName = userName;
        public string GetCurrentUserId() => _userId;
        public void SetCurrentUserId(string userId) =>_userId = userId;
        
    }
  
}

