using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Services;

namespace NRM.Pages.AdminPanel.GroupParcel
{
    public class EditModel : PageModel
    {
        private readonly GroupParcelService _groupParcelService;

        public EditModel(GroupParcelService groupParcelService)
        {
            _groupParcelService = groupParcelService;
        }

        [BindProperty]
        public Models.GroupParcelModels.EditModel Input { get; set; }

        public async Task OnGet(int id)
        {
            Input = await _groupParcelService.ViewEditParcel(id, User.Identity.Name);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                bool b = await _groupParcelService.EditGroupParcel(Input, id, User.Identity.Name);
                if (b) return RedirectToPage("View", new { id = id });
                else ModelState.AddModelError(String.Empty, "Ошибка изменения группы РПО");
            }
            return Page();
        }

    }
}
