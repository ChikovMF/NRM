using Microsoft.AspNetCore.Mvc.Rendering;
using NRM.Models.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace NRM.Models.ParcelModels
{
    public class EditModel
    {
        [DisplayName("Трек-номер")]
        public string? TrackNumber { get; set; }
        [Required(ErrorMessage = "Введите отправителя"), DisplayName("Отправитель")]
        public string Sender { get; set; }
        [Required(ErrorMessage = "Введите получателя"), DisplayName("Получатель")]
        public string Recipient { get; set; }
        [Required(ErrorMessage = "Выберите тип РПО"), DisplayName("Тип РПО")]
        public int TypeId { get; set; }
        [DisplayName("Статус РПО")]
        public int StatusId { get; set; }
        [DataType(DataType.Date),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true),
            Display(Name = "Дата отправления"),
            Required(ErrorMessage = "Введите дату отправки")]
        public DateOnly DepartureDate { get; set; }
        [DataType(DataType.Time),
            DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true),
            Display(Name = "Время отправления"),
            Required(ErrorMessage = "Введите время отправки")]
        public TimeOnly DepartureTime { get; set; }

        public List<SelectListItem>? TypeItems { get; set; }
        public List<SelectListItem>? StatusItems { get; set; }

        public void UpdateParcel(Parcel parcel)
        {
            parcel.Sender = Sender;
            parcel.Recipient = Recipient;
            parcel.StatusId = StatusId;
            parcel.DepartureDate = DepartureDate;
            parcel.DepartureTime = DepartureTime;
            parcel.TypeId = TypeId;
        }
    }
}
