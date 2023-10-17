using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using NRM.Models.ParcelModels;
using NRM.Services;
using NRM.Services.Queries;

namespace NRM.Pages.AdminPanel.Parcel
{
    public class IndexModel : PageModel
    {
        private readonly ParcelService _parcelService;

        public IndexModel(ParcelService parcelService)
        {
            _parcelService = parcelService;
        }

        public List<TableModel> Parcels { get; set; }
        [BindProperty(SupportsGet = true)]
        public SortFilterParcelPageOptions Options { get; set; }

        public async Task OnGet()
        {
            if (Options == null) Options = new SortFilterParcelPageOptions();
            Parcels = await _parcelService.GetParcels(Options);
        }

        public JsonResult OnGetFilterSearchContent([FromServices]ParcelFilterDropdownService filterService)
        {
            var traceIdent = HttpContext.TraceIdentifier;

            return new JsonResult(
                new TraceIndentGeneric<IEnumerable<DropdownTuple>>(
                    traceIdent,
                    filterService.GetFilterDropDownValues(
                        Options.FilterBy)));
        }
    }
}
