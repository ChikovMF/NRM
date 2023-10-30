using NRM.Models.DataModels;
using NRM.Models.ParcelModels;
using System.ComponentModel.DataAnnotations;

namespace NRM.Services.Queries
{
    public enum OrderByOptions
    {
        [Display(Name = "Сортировка по...")] SimpleOrder = 0,
        [Display(Name = "Дате ↑")] ByDateTimeHighestFirst,
        [Display(Name = "Дате ↓")] ByDateTimeLowestFirst,
        [Display(Name = "Типу РПО")] ByParcelType,
        [Display(Name = "Статусу")] ByParcelStatus,
    }
    public static class ParcelListDtoSort
    {
        public static IQueryable<Parcel> OrderParcelBy
        (this IQueryable<Parcel> parcels,
            OrderByOptions orderByOptions)
        {
            switch (orderByOptions)
            {
                case OrderByOptions.SimpleOrder:
                    return parcels.OrderByDescending(
                        x => x.Id);
                case OrderByOptions.ByDateTimeHighestFirst:
                    return parcels.OrderByDescending(x =>x.DepartureDate)
                        .ThenByDescending(x => x.DepartureTime);
                case OrderByOptions.ByDateTimeLowestFirst:
                    return parcels.OrderBy(x => x.DepartureDate)
                        .ThenBy(x => x.DepartureTime);
                case OrderByOptions.ByParcelType:
                    return parcels.OrderBy(x => x.TypeId);
                case OrderByOptions.ByParcelStatus:
                    return parcels.OrderByDescending(x => x.StatusId);
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(orderByOptions), orderByOptions, null);
            }
        }
    }
}
