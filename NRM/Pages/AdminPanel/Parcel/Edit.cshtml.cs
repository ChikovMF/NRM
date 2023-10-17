using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Services;

namespace NRM.Pages.AdminPanel.Parcel
{
    public class EditModel : PageModel
    {
        private readonly ParcelService _parcelService;

        public EditModel(ParcelService parcelService)
        {
            _parcelService = parcelService;
        }

        [BindProperty]
        public Models.ParcelModels.EditModel Input { get; set; }

        public async Task OnGet(int id)
        {
            Input = await _parcelService.ViewEditParcel(id);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                bool b = await _parcelService.EditParcel(Input, id, User.Identity.Name);
                if (b) return RedirectToPage("View", new {id = id});
                else ModelState.AddModelError(String.Empty, "Ошибка изменения посылки");
            }
            return Page();
        }
    }
}
