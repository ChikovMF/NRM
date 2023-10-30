using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Services;

namespace NRM.Pages.AdminPanel.Parcel
{
    public class CreateModel : PageModel
    {
        private readonly ParcelService _parcelService;

        public CreateModel(ParcelService parcelService)
        {
            _parcelService = parcelService;
        }

        [BindProperty]
        public Models.ParcelModels.CreateModel Input { get; set; } = new Models.ParcelModels.CreateModel();

        public async Task OnGet()
        {
            Input.TypeItems = await _parcelService.GetParcelTypesSelect();
            Input.DepartureDate = DateOnly.FromDateTime(DateTime.Now);
            Input.DepartureTime = TimeOnly.FromDateTime(DateTime.Now);
            Input.TrackNumber = _parcelService.RandomTrackNumber();
            Input.StartItemsPlace = await _parcelService.GetStartItems(User.Identity?.Name);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                bool b = await _parcelService.CreateParcel(Input, User.Identity.Name);
                if (b) return RedirectToPage("Index");
                else ModelState.AddModelError(String.Empty, "Ошибка создания РПО");
            }
            Input.TypeItems = await _parcelService.GetParcelTypesSelect();
            Input.StartItemsPlace = await _parcelService.GetStartItems(User.Identity?.Name);
            return Page();
        }
    }
}
