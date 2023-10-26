using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using NRM.Models.DataModels;

namespace NRM.Models.GroupParcelModels
{
    public class CreateModel
    {
        [Required(ErrorMessage = "Введите трек-номер"), DisplayName("Трек-номер"), 
            StringLength(13, MinimumLength = 13, ErrorMessage = "Длина строки должна быть 13 символов")]
        public string TrackNumber { get; set; }
        [DataType(DataType.Date), 
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), 
            Required(ErrorMessage = "Введите дату отправки"), Display(Name = "Дата отправления")]
        public DateOnly DepartureDate { get; set; }
        [DataType(DataType.Time),
            DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true),
            Display(Name = "Время отправления"),
            Required(ErrorMessage = "Введите время отправки")]
        public TimeOnly DepartureTime { get; set; }
        [Required(ErrorMessage = "Выберите место отправки"), 
            DisplayName("Место отправки")]
        public int PlaceOfDepartureId { get; set; }
        [Required(ErrorMessage = "Выберите место доставки"), 
            DisplayName("Место доставки")]
        public int PlaceOfDeliveryId { get; set; }
        [Required(ErrorMessage = "Введите ответственного"), 
            DisplayName("Ответственный")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Выберите посылки"), DisplayName("Посылки")]
        public List<int> ParcelsId { get; set; }
        public StartUserItem? StartUser { get; set; }

        public class StartUserItem
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string? Place { get; set; }
            public int? PlaceId { get; set; }
        }

        public GroupParcel ToGroupParcel()
        {
            return new GroupParcel
            {
                DepartureDate = DepartureDate,
                TrackNumber = TrackNumber,
                PlaceOfDepartureId = PlaceOfDepartureId,
                PlaceOfDeliveryId = PlaceOfDeliveryId,
                UserId = UserId,
                StatusId = 1,
                DepartureTime = DepartureTime,
            };
        }
    }
}
