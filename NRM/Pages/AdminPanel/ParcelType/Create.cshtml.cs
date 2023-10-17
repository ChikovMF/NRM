using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Services;

namespace NRM.Pages.AdminPanel.ParcelType
{
    public class CreateModel : PageModel
    {
        private readonly ParcelTypeService _parcelTypeService;

        public CreateModel(ParcelTypeService parcelTypeService)
        {
            _parcelTypeService = parcelTypeService;
        }

        [BindProperty]
        public Models.ParcelTypeModels.CreateModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                bool b = await _parcelTypeService.CreateParcelType(Input);
                if (b) return RedirectToPage("Index");
                else ModelState.AddModelError(String.Empty, "Ошибка создания типа посылки");
            }
            return Page();
        }
    }
}
