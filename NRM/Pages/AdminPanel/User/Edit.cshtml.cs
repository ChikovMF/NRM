using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Services;
using System.Net;

namespace NRM.Pages.AdminPanel.User
{
    public class EditModel : PageModel
    {
        private readonly AuthorizationService _authorizationService;

        public EditModel(AuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public Models.UserModels.EditModel Input { get; set; }

        public async Task OnGet(int id)
        {
            Input = await _authorizationService.ViewEditUser(id);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                bool b = await _authorizationService.EditUser(Input, id);
                if (b) return RedirectToPage("View", new { id = id });
                else ModelState.AddModelError(String.Empty, "Ошибка изменения пользователя");
            }
            return Page();
        }
    }
}
