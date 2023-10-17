using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Services;

namespace NRM.Pages.AdminPanel.ParcelStatus
{
    public class CreateModel : PageModel
    {
        private readonly ParcelStatusService _parcelStatusService;

        public CreateModel(ParcelStatusService parcelStatusService)
        {
            _parcelStatusService = parcelStatusService;
        }

        [BindProperty]
        public Models.ParcelStatusModels.CreateModel Input { get; set; }

        public IActionResult OnGet()
        {
            return NotFound(); //!
        }

        public async Task<IActionResult> OnPost()
        {
            return NotFound(); //!
            if (ModelState.IsValid)
            {
                bool b = await _parcelStatusService.CreateParcelStatus(Input);
                if (b) return RedirectToPage("Index");
                else ModelState.AddModelError(String.Empty, "Ошибка создания статуса посылки");
            }
            return Page();
        }
    }
}
