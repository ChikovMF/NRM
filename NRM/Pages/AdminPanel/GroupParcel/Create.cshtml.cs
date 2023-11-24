using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Models.GroupParcelModels;
using NRM.Services;

namespace NRM.Pages.AdminPanel.GroupParcel
{
    public class CreateModel : PageModel
    {
        private readonly GroupParcelService _groupParcelService;

        public CreateModel(GroupParcelService groupParcelService)
        {
            _groupParcelService = groupParcelService;
        }

        [BindProperty]
        public Models.GroupParcelModels.CreateModel Input { get; set; } = new Models.GroupParcelModels.CreateModel();

        public async Task OnGet()
        {
            Input.StartUser = await _groupParcelService.GetStartUser(User.Identity.Name);
            Input.TrackNumber = _groupParcelService.RandomTrackNumber();
            Input.DepartureDate = DateOnly.FromDateTime(DateTime.Now);
            Input.DepartureTime = TimeOnly.FromDateTime(DateTime.Now);
        }

        public async Task<IActionResult> OnPost()
        {  
            if (ModelState.IsValid)
            {
                string error = await _groupParcelService.CreateGroupParcel(Input, User.Identity.Name);
                if (string.IsNullOrEmpty(error)) return RedirectToPage("Index");
                else ModelState.AddModelError(String.Empty, error);
            }
            return Page();
        }


    }
}
