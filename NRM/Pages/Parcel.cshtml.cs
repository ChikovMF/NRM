using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Services;

namespace NRM.Pages
{
    public class ParcelModel : PageModel
    {
        private readonly ParcelService _parcelService;

        public ParcelModel(ParcelService parcelService)
        {
            _parcelService = parcelService;
        }

        public Models.ParcelModels.PublicViewModel Parcel { get; set; }

        public async Task OnGet(int id)
        {
            Parcel = await _parcelService.ViewPublicParcel(id);
        }
    }
}
