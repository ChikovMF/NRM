using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NRM.Services;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Xml.Linq;

namespace NRM.Pages.AdminPanel.User
{
    public class ViewModel : PageModel
    {
        private readonly AuthorizationService _authorizationService;

        public ViewModel(AuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public Models.UserModels.ViewModel User { get; set; }

        public async Task OnGet(int id)
        {
            User = await _authorizationService.ViewUser(id);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                bool b = await _authorizationService.ChangePassword(id, Input.Password);
                if (b) ModelState.AddModelError(String.Empty, "������ ������������ ������� ������");
                else ModelState.AddModelError(String.Empty, "������ ����� ������������");
            }
            User = await _authorizationService.ViewUser(id);
            return Page();
        }

        public class InputModel
        {
            [Display(Name = "������"),
                Required(ErrorMessage = "������� ������"),
                DataType(DataType.Password),
                StringLength(15, MinimumLength = 8, ErrorMessage = "������ ������ ��������� ����� 8 �������� � �� ����� 15"),
                RegularExpression("(?=^.{8,}$)((?=.*\\d)|(?=.*\\W+))(?![.\\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "�������� ������ ������")]
            public string Password { get; set; }
            [Required(ErrorMessage = "����������� ������"),
                Compare("Password", ErrorMessage = "������ �� ���������"),
                DataType(DataType.Password),
                Display(Name = "����������� ������")]
            public string PasswordConfirm { get; set; }
        }
    }
}
