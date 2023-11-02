using Microsoft.AspNetCore.Mvc.Rendering;
using NRM.Models.DataModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NRM.Models.UserModels
{
    public class EditModel
    {
        public string? Login { get; set; }
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
        public Item? StartPlace { get; set; }

        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public void UpdateUser(User user)
        {
            user.Email = Email;
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Patronymic = Patronymic;
            user.RoleId = RoleId;
            user.PhoneNumber = PhoneNumber;
            user.PlaceId = PlaceId;
        }
    }
}
