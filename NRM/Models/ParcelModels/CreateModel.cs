using NRM.Models.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NRM.Models.ParcelModels
{
    public class CreateModel
    {
        [Required(ErrorMessage = "Введите трек-номер"), DisplayName("Трек-номер"), 
            StringLength(14, MinimumLength = 14, ErrorMessage = "Длина строки должна быть 14 символов")]
        public string TrackNumber { get; set; }
        [Required(ErrorMessage = "Введите отправителя"), DisplayName("Отправитель")]
        public string Sender { get; set; }
        [Required(ErrorMessage = "Введите получателя"), DisplayName("Получатель")]
        public string Recipient { get; set; }
        [Required(ErrorMessage = "Выберите тип посылки"), DisplayName("Тип посылки")]
        public int TypeId { get; set; }
        [DisplayName("Статус посылки")]
        public int? StatusId { get; set; }
        [DataType(DataType.Date),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true),
            Display(Name = "Дата отправления"),
            Required(ErrorMessage = "Введите дату отправки")]
        public DateOnly DepartureDate { get; set; }
        [Required(ErrorMessage = "Выберите место отправки"),
            DisplayName("Место отправки")]
        public int? PlaceOfDepartureId { get; set; }
        [Required(ErrorMessage = "Выберите место доставки"),
            DisplayName("Место доставки")]
        public int? PlaceOfDeliveryId { get; set; }
        [DataType(DataType.Time),
            DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true),
            Display(Name = "Время отправления"),
            Required(ErrorMessage = "Введите время отправки")]
        public TimeOnly DepartureTime { get; set; }
        public StartItems? StartItemsPlace { get; set; }
        [DisplayName("В/ч доставки")]
        public int? MilitaryUnitId { get; set; }

        public List<SelectListItem>? TypeItems { get; set; }

        public Parcel ToParcel()
        {
            return new Parcel
            {
                Sender = Sender,
                Recipient = Recipient,
                TypeId = TypeId,
                DepartureDate = DepartureDate,
                DepartureTime = DepartureTime,
                TrackNumber = TrackNumber,
                StatusId = 1,
                IsDeleted = false,
                PlaceOfDeliveryId = PlaceOfDeliveryId,
                PlaceOfDepartureId = PlaceOfDepartureId,
                MilitaryUnitId = MilitaryUnitId,
            };
        }

        public class StartItems
        {
            public string? Place { get; set; }
            public int? PlaceId { get; set; }
        }
    }
}
