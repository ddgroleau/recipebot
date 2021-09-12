using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PBC.Shared.AccountComponent;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using System;

namespace PBC.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<IAccountLoginDTO> _logger;
        private readonly IEmailSender _emailSender;

        public AccountController(SignInManager<IdentityUser> signInManager,
            ILogger<IAccountLoginDTO> logger,
            UserManager<IdentityUser> userManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [HttpPost, Route("Register")]
        public async Task<IActionResult> Register(AccountRegisterDTO accountRegisterDTO)
        {
            try 
            {
                // string returnUrl = Url.Content("~/");
                    _logger.LogInformation($"Registration request received at AccountController. {accountRegisterDTO.Email} is attempting to register at {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                
                var user = new IdentityUser { UserName = accountRegisterDTO.Email, Email = accountRegisterDTO.Email };
                
                var result = await _userManager.CreateAsync(user, accountRegisterDTO.Password);
                    
                if (result.Succeeded)
                {
                    _logger.LogInformation($"Registration successful for email: {accountRegisterDTO.Email}. Timestamp: {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
    
                        // var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        // code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        
                        // var callbackUrl = Url.Page(
                        //     "/Account/Login",
                        //     pageHandler: null,
                        //     values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        //     protocol: Request.Scheme);
    
                        // await _emailSender.SendEmailAsync(accountRegisterDTO.Email, "Confirm your email",
                        //     $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
    
                        //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        //{
                        //    //return RedirectToPage("/Login");
                        //    return Ok();
                        //}
                        //else
                        //{
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return Ok();
                        //}
                   
                }
                else
                {
                    _logger.LogInformation($"Registration failed for Email: {accountRegisterDTO.Email} at {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                    return BadRequest(result);
                }
            } 
            catch(Exception e)
            {
                _logger.LogError($"Error occured when registering Email: {accountRegisterDTO.Email} at {DateTime.Now:MM/dd/yyyy HH:mm:ss}. Error: {e.Message}.");
                return BadRequest();
            }
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login(AccountLoginDTO accountLoginDTO)
        {
            try 
            {
                _logger.LogInformation($"Login request received at AccountController. {accountLoginDTO.Email} is attempting to login at {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                
                var result = await _signInManager.PasswordSignInAsync(accountLoginDTO.Email, accountLoginDTO.Password, accountLoginDTO.RememberMe, lockoutOnFailure: false);
    
                if (result.Succeeded)
                {
                    _logger.LogInformation($"Email: {accountLoginDTO.Email} successfully logged in at {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                    return Ok();
                }
                else
                {
                    _logger.LogInformation($"Email: {accountLoginDTO.Email} had a failed login attempt in at {DateTime.Now:MM/dd/yyyy HH:mm:ss}.");
                    return BadRequest();
                }
            } 
            catch (Exception e) 
            {
                _logger.LogError($"Login attempt error thrown for Email: {accountLoginDTO.Email} at {DateTime.Now:MM/dd/yyyy HH:mm:ss}. Error: {e.Message}.");
                return BadRequest();
            }
        }
    }
}