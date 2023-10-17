using NRM.Models.DataModels;
using System.ComponentModel.DataAnnotations;

namespace NRM.Services.Queries
{
    public enum ParcelsFilterBy
    {
        [Display(Name = "Без фильтра")] NoFilter = 0,
        [Display(Name = "По типу...")] ByTypes,
        [Display(Name = "По статусу...")] ByStatus,
    }

    public static class ParcelListDtoFilter
    {
        public static IQueryable<Parcel> FilterParcelsBy(
            this IQueryable<Parcel> parcels,
            ParcelsFilterBy filterBy, string filterValue)
        {
            if (string.IsNullOrEmpty(filterValue))
                return parcels;

            switch (filterBy)
            {
                case ParcelsFilterBy.NoFilter:
                    return parcels;
                case ParcelsFilterBy.ByTypes:
                    var filterType = int.Parse(filterValue);
                    return parcels.Where(x =>
                        x.TypeId == filterType);
                case ParcelsFilterBy.ByStatus:
                    var filterStatus = int.Parse(filterValue);
                    return parcels.Where(x =>
                        x.StatusId == filterStatus);
                default:
                    throw new ArgumentOutOfRangeException
                        (nameof(filterBy), filterBy, null);
            }
        }
    }
}
