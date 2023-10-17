using NRM.Models.DataModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace NRM.Models.PlaceModels
{
    public class EditModel
    {
        [Display(Name = "Название места"),
            Required(ErrorMessage = "Введите название места")]
        public string Name { get; set; }

        public void UpdatePlace(Place place)
        {
            place.Name = Name;
        }
    }
}
