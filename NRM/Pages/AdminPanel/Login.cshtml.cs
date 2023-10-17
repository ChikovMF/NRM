using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Services;
using System.Net;
using System.Security.Claims;

namespace NRM.Pages.AdminPanel
{
    public class LoginModel : PageModel
    {
        private readonly AuthorizationService _authorizationService;

        public LoginModel(AuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public Models.UserModels.LoginModel Input { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = await _authorizationService.Login(Input);
                if (user != null)
                {
                    var claims = new List<Claim>
                    { 
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToPage("Index");
                }
                else ModelState.AddModelError(String.Empty, "Неправильный логин или пароль");
            }
            return Page();
        }
    }
}
