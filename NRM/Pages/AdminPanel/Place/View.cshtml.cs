using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Models.DataModels;
using NRM.Services;

namespace NRM.Pages.AdminPanel.Place
{
    public class ViewModel : PageModel
    {
        private readonly PlaceService _placeService;

        public ViewModel(PlaceService placeService)
        {
            _placeService = placeService;
        }

        public Models.PlaceModels.ViewModel Place { get; set; }

        public async Task OnGet(int id)
        {
            Place = await _placeService.ViewPlace(id);
        }

        public async Task<IActionResult> OnPostCreateMilitaryUnit([FromForm] MilitaryUnit militaryUnit, int id)
        {
            if (!string.IsNullOrWhiteSpace(militaryUnit.Name) && militaryUnit.PlaceId != 0)
            {
                var error = await _placeService.CreateMilitaryUnit(militaryUnit);

                if (error != null)
                {
                    ModelState.AddModelError(String.Empty, error.ErrorMessage);
                }

                Place = await _placeService.ViewPlace(id);
                return Page();
            }
            return RedirectToPage("View", new { id = militaryUnit.PlaceId });
        }

        public async Task<IActionResult> OnPostDeleteMilitaryUnit(int idMilitaryUnit, int idPlace)
        {
            await _placeService.DeleteMilitaryUnit(idMilitaryUnit, User.Identity.Name);

            return RedirectToPage("View", new { id = idPlace });
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            await _placeService.DeletePlace(id, User.Identity.Name);
            return RedirectToPage("Index");
        }
    }
}
