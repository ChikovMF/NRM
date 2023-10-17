using NRM.Models.DataModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NRM.Models.ParcelTypeModels
{
    public class CreateModel
    {
        [Display(Name = "Тип посылки"),
            Required(ErrorMessage = "Введите тип посылки")]
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
