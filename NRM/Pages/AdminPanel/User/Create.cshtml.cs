using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Services;
using System.Net;

namespace NRM.Pages.AdminPanel.User
{
    public class CreateModel : PageModel
    {
        private readonly AuthorizationService _authorizationService;

        public CreateModel(AuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public Models.UserModels.CreateModel Input { get; set; } = new Models.UserModels.CreateModel();

        public async Task OnGet()
        {
            Input.RoleItems = await _authorizationService.GetRolesSelect();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                string error = await _authorizationService.CreateUser(Input, User.Identity.Name);
                if (error == string.Empty) return RedirectToPage("Index");
                else ModelState.AddModelError(String.Empty, error);
            }
            Input.RoleItems = await _authorizationService.GetRolesSelect();
            return Page();
        }
    }
}
