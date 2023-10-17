using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NRM.Services;
using System.ComponentModel.DataAnnotations;

namespace NRM.Pages.AdminPanel.Parcel
{
    public class ViewModel : PageModel
    {
        private readonly ParcelService _parcelService;

        public ViewModel(ParcelService parcelService)
        {
            _parcelService = parcelService;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();
        public Models.ParcelModels.ViewModel Parcel { get; set; }

        public async Task OnGet(int id)
        {
            Parcel = await _parcelService.ViewParcel(id);
            Input.StatusItems = await _parcelService.GetParcelStatusSelect();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            bool b = await _parcelService.ChangeStatusParcel(Input.StatusId, id, User.Identity.Name);
            if (b) { ModelState.AddModelError(String.Empty, "Статус успешно сменен"); }
            else ModelState.AddModelError(String.Empty, "Ошибка смены статуса");
            Parcel = await _parcelService.ViewParcel(id);
            Input.StatusItems = await _parcelService.GetParcelStatusSelect();
            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            await _parcelService.DeleteParcel(id, User.Identity.Name);
            return RedirectToPage("Index");
        }

        public class InputModel
        {
            [Display(Name = "Статус посылки"), Required(ErrorMessage = "Введите статус посылки")]
            public int StatusId { get; set; }
            public List<SelectListItem>? StatusItems { get; set; }
        }
    }
}
