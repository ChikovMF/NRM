using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Models.PlaceModels;
using NRM.Services;

namespace NRM.Pages.AdminPanel.Place
{
    public class IndexModel : PageModel
    {
        private readonly PlaceService _placeService;

        public IndexModel(PlaceService placeService)
        {
            _placeService = placeService;
        }

        public List<TableModel> Places { get; set; }

        public async Task OnGet()
        {
            Places = await _placeService.GetTablePlaces();
        }
    }
}
