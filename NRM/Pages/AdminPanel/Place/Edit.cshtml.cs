using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Services;

namespace NRM.Pages.AdminPanel.Place
{
    public class EditModel : PageModel
    {
        private readonly PlaceService _placeService;

        public EditModel(PlaceService placeService)
        {
            _placeService = placeService;
        }

        [BindProperty]
        public Models.PlaceModels.EditModel Input { get; set; }

        public async Task OnGet(int id)
        {
            Input = await _placeService.ViewEditPlace(id);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                bool b = await _placeService.EditPlace(Input, id, User.Identity.Name);
                if (b) return RedirectToPage("View", new { id = id });
                else ModelState.AddModelError(String.Empty, "Ошибка изменения места");
            }
            return Page();
        }
    }
}
