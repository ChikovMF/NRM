using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Services;

namespace NRM.Pages.AdminPanel.ParcelType
{
    public class ViewModel : PageModel
    {
        private readonly ParcelTypeService _parcelTypeService;

        public ViewModel(ParcelTypeService parcelTypeService)
        {
            _parcelTypeService = parcelTypeService;
        }

        public Models.ParcelTypeModels.ViewModel ParcelType { get; set; }

        public async Task OnGet(int id)
        {
            ParcelType = await _parcelTypeService.ViewParcelType(id);
        }
    }
}
