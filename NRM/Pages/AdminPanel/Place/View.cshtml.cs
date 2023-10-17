using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    }
}
