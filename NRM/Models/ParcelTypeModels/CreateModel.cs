using NRM.Models.DataModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NRM.Models.ParcelTypeModels
{
    public class CreateModel
    {
        [Display(Name = "Тип РПО"),
            Required(ErrorMessage = "Введите тип РПО")]
        public string Name { get; set; }

        public ParcelType ToParcelType()
        {
            return new ParcelType
            {
                Name = Name,
                IsDeleted = false
            };
        }
    }
}
