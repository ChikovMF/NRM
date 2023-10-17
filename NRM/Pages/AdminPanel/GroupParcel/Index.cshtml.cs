using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Models.GroupParcelModels;
using NRM.Services;

namespace NRM.Pages.AdminPanel.GroupParcel
{
    public class IndexModel : PageModel
    {
        private readonly GroupParcelService _groupParcelService;

        public IndexModel(GroupParcelService groupParcelService)
        {
            _groupParcelService = groupParcelService;
        }

        public List<TableModel> GroupParcels { get; set; }

        public async Task OnGet()
        {
            GroupParcels = await _groupParcelService.GetGroupParcels();
        }

        public async Task OnPostDelite(int id)
        {

        }
    }
}
