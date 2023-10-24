using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using NRM.Models.DataModels;

namespace NRM.Models.UserModels
{
    public class CreateModel
    {
        [Display(Name = "Логин"),
            Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }
        [Display(Name = "Пароль"),
            Required(ErrorMessage = "Введите пароль"),
            DataType(DataType.Password),
            StringLength(15, MinimumLength = 8, ErrorMessage = "Пароль должен содержать более 8 и не менее 15 символов"),
            RegularExpression("(?=^.{8,}$)((?=.*\\d)|(?=.*\\W+))(?![.\\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Неверный формат пароля")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Подтвердите пароль"),
            Compare("Password", ErrorMessage = "Пароли не совпадают"),
            DataType(DataType.Password),
            Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
        [Display(Name = "Номер телефона"),
            RegularExpression("^((8|\\+7)[\\- ]?)?(\\(?\\d{3}\\)?[\\- ]?)?[\\d\\- ]{4,10}$", ErrorMessage = "Неверный формат номера телефона")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Электронная почта"), 
            EmailAddress()]
        public string? Email { get; set; }
        [Display(Name = "Имя"),
             Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия"),
             Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; }
        [Display(Name = "Отчество"),
             Required(ErrorMessage = "Введите отчество")]
        public string Patronymic { get; set; }
        [Required(ErrorMessage = "Введите место работы"), Display(Name = "Место работы")]
        public int PlaceId { get; set; }
        [Display(Name = "Роль пользователя"),
            Required(ErrorMessage = "Введите роль пользователя")]
        public int RoleId { get; set; }
        public List<SelectListItem>? RoleItems { get; set; }

        public User ToUser()
        {
            return new User
            {
                Login = Login,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password),
                IsDeleted = false,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Patronymic = Patronymic,
                PhoneNumber = PhoneNumber,
                RoleId = RoleId,
                PlaceId = PlaceId
            };
        }
    }
}
