using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Models.ParcelTypeModels;
using NRM.Services;

namespace NRM.Pages.AdminPanel.ParcelType
{
    public class IndexModel : PageModel
    {
        private readonly ParcelTypeService _parcelTypeService;

        public IndexModel(ParcelTypeService parcelTypeService)
        {
            _parcelTypeService = parcelTypeService;
        }

        public List<TableModel> ParcelTypes { get; set; }

        public async Task OnGet()
        {
            ParcelTypes = await _parcelTypeService.GetTableParcelTypes();
        }
    }
}
