using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ExternalAuthorization.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExternalAuthorization.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ExternalAuthData.ExternalAuthContext Context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> SignInManager, UserManager<ApplicationUser> UserManager, ExternalAuthData.ExternalAuthContext DbContext)
        {
            _signInManager = SignInManager;
            _userManager = UserManager;
            Context = DbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FacebookLogin()
        {
            // Request a redirect to the external login provider.
            var redirectUrl = "./Account/FacebookLoginCallback";
            var properties = new AuthenticationProperties();
            properties.RedirectUri = redirectUrl;
            properties.Items.Add("LoginProvider", "Facebook");
            var cr = new ChallengeResult("Facebook", properties);
            return cr;
        }

        public IActionResult FacebookLoginCallback()
        {
            return View("Register");
        }

        public async Task<JsonResult> RegisterUser(string FirstName, string LastName, string Email)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            string providerkey = "";
            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
            {
                providerkey = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            string authprovider = "";
            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
            {
                authprovider = info.Principal.FindFirst(ClaimTypes.NameIdentifier).Issuer;
            }

            ApplicationUser user = new ApplicationUser()
            {
                Id = providerkey,
                UserName = Email,
                Email = Email,
                GivenName = FirstName,
                SurName = LastName,
                AuthenticationProvider = authprovider
            };

            var result = await _userManager.CreateAsync(user);

            return Json(result);
        }

        public async Task<JsonResult> RetrieveExternalAuthClaims()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            Dictionary<string, string> claims = new Dictionary<string, string>();
            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                claims.Add("Email", info.Principal.FindFirstValue(ClaimTypes.Email));
            }
            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.GivenName))
            {
                claims.Add("FirstName", info.Principal.FindFirstValue(ClaimTypes.GivenName));
            }
            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Surname))
            {
                claims.Add("LastName", info.Principal.FindFirstValue(ClaimTypes.Surname));
            }

            return Json(claims);
        }
    }
}