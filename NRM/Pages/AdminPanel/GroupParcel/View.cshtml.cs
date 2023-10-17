using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NRM.Services;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NRM.Pages.AdminPanel.GroupParcel
{
    public class ViewModel : PageModel
    {
        private readonly GroupParcelService _groupParcelService;

        public ViewModel(GroupParcelService groupParcelService)
        {
            _groupParcelService = groupParcelService;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();
        public Models.GroupParcelModels.ViewModel GroupParcel { get; set; }

        public async Task OnGet(int id)
        {
            GroupParcel = await _groupParcelService.ViewGroupParcel(id);
            Input.StatusItems = await _groupParcelService.GetGroupParcelStatusSelect();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            bool b = await _groupParcelService.ChangeStatusGroupParcel(Input.StatusId, id, User.Identity.Name);
            if (b) { ModelState.AddModelError(String.Empty, "Статус успешно сменен"); }
            else ModelState.AddModelError(String.Empty, "Ошибка смены статуса");
            GroupParcel = await _groupParcelService.ViewGroupParcel(id);
            Input.StatusItems = await _groupParcelService.GetGroupParcelStatusSelect();
            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            await _groupParcelService.DeleteGroupParcel(id, User.Identity.Name);
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
