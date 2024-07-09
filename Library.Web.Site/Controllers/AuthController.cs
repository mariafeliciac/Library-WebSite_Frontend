using Library.Web.Site.Models.Model;
using Library.Web.Site.Models.ModelDto.UserDto;
using Library.Web.Site.Models.ShareModels;
using Library.Web.Site.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Net;
using System.Security.Claims;
using Library.Web.Site.Models;
using Microsoft.AspNetCore.Authorization;

namespace Library.Web.Site.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

      
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        
        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginModel() { ReturnUrl=returnUrl});  
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                var user = _userService.Login(model.Username, model.Password);

                if (user == null)
                {
                    return View("Login", null);
                }

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,user.Role.ToString())
            };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                              principal);

                return LocalRedirect(model.ReturnUrl);

            }
            catch (Exception ex)
            {
                return View("Error",new ErrorViewModel() { RequestId = ex.Message });

            }
          
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/Auth/Login");
        }
    }
}
