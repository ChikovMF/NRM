using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Services;

namespace NRM.Pages.AdminPanel.ParcelStatus
{
    public class ViewModel : PageModel
    {
        private readonly ParcelStatusService _parcelStatusService;

        public ViewModel(ParcelStatusService parcelStatusService)
        {
            _parcelStatusService = parcelStatusService;
        }

        public Models.ParcelStatusModels.ViewModel ParcelStatus { get; set; }

        public async Task OnGet(int id)
        {
            ParcelStatus = await _parcelStatusService.ViewParcelStatus(id);
        }
    }
}
