using static NRM.Models.GroupParcelModels.CreateModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;
using NRM.Models.DataModels;
using System.Reflection;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NRM.Models.GroupParcelModels
{
    public class EditModel
    {
        public string? TrackNumber { get; set; }
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
        public Item? PlaceOfDepartureNow { get; set; }
        [Required(ErrorMessage = "Выберите место доставки"),
            DisplayName("Место доставки")]
        public int PlaceOfDeliveryId { get; set; }
        public Item? PlaceOfDeliveryNow { get; set; }
        [Required(ErrorMessage = "Введите ответственного"),
            DisplayName("Ответственный")]
        public int UserId { get; set; }
        [DisplayName("Статус РПО")]
        public int StatusId { get; set; }
        [Required(ErrorMessage = "Выберите РПО"), DisplayName("Список РПО")]
        public List<int> ParcelsId { get; set; }
        public List<Item>? ParcelsNow { get; set; }
        public StartUserItem? StartUser { get; set; }

        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public List<SelectListItem>? StatusItems { get; set; }
        public void UpdateGroupParcel(GroupParcel groupParcel)
        {
            groupParcel.DepartureDate = DepartureDate;
            groupParcel.DepartureTime = DepartureTime;
            groupParcel.PlaceOfDepartureId = PlaceOfDepartureId;
            groupParcel.PlaceOfDeliveryId = PlaceOfDeliveryId;
            groupParcel.UserId = UserId;
            groupParcel.StatusId = StatusId;
        }
    }
}
