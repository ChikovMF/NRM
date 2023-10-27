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

        public async Task<IActionResult> OnPostCreateMilitaryUnit([FromForm] MilitaryUnit militaryUnit)
        {
            if (!string.IsNullOrWhiteSpace(militaryUnit.Name) && militaryUnit.PlaceId != 0)
            {
                await _placeService.CreateMilitaryUnit(militaryUnit);
            }
            return RedirectToPage("View", new { id = militaryUnit.PlaceId });
        }

        public async Task<IActionResult> OnPostDeleteMilitaryUnit(int idMilitaryUnit, int idPlace)
        {
            await _placeService.DeleteMilitaryUnit(idMilitaryUnit);

            return RedirectToPage("View", new { id = idPlace });
        }
    }
}
