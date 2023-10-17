using NRM.Models.DataModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NRM.Models.PlaceModels
{
    public class CreateModel
    {
        [Display(Name = "Название места"),
            Required(ErrorMessage = "Введите название места")]
        public string Name { get; set; }

        public Place ToPlace()
        {
            return new Place
            {
                Name = Name,
                IsDeleted = false
            };
        }
    }
}
