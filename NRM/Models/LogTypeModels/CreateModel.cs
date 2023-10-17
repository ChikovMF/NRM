using NRM.Models.DataModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NRM.Models.LogTypeModels
{
    public class CreateModel
    {
        [Display(Name = "Тип логов"),
            Required(ErrorMessage = "Введите тип логов")]
        public string Name { get; set; }

        public LogType ToLogType()
        {
            return new LogType
            {
                Name = Name,
                IsDeleted = false
            };
        }
    }
}
