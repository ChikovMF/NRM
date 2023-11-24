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
                if (b) ModelState.AddModelError(String.Empty, "Пароль пользователя успешно сменен");
                else ModelState.AddModelError(String.Empty, "Ошибка смены пароля");
            }
            User = await _authorizationService.ViewUser(id);
            return Page();
        }

        public class InputModel
        {
            [Display(Name = "Новый пароль"),
                Required(ErrorMessage = "Введите пароль"),
                DataType(DataType.Password),
                StringLength(15, MinimumLength = 8, ErrorMessage = "Пароль должен быть более 8 и менее 15 символов"),
                RegularExpression("(?=^.{8,}$)((?=.*\\d)|(?=.*\\W+))(?![.\\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Введите корректный пароль")]
            public string Password { get; set; }
            [Required(ErrorMessage = "Подтвердите пароль"),
                Compare("Password", ErrorMessage = "Пароли не совпадают"),
                DataType(DataType.Password),
                Display(Name = "Повторите пароль")]
            public string PasswordConfirm { get; set; }
        }
    }
}
