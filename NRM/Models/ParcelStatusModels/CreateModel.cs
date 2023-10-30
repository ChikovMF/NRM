using NRM.Models.DataModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NRM.Models.ParcelStatusModels
{
    public class CreateModel
    {
        [Display(Name = "Статус РПО"),
            Required(ErrorMessage = "Введите статус РПО")]
        public string Name { get; set; }

        public ParcelStatus ToParcelStatus()
        {
            return new ParcelStatus
            {
                Name = Name,
                IsDeleted = false
            };
        }
    }
}
