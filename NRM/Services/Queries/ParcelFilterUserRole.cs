using NRM.Models.DataModels;

namespace NRM.Services.Queries
{
    public static class ParcelFilterUserRole
    {
        public static IQueryable<Parcel> ParcelFilterRole(this IQueryable<Parcel> parcels, User user)
        {
            if (user.RoleId == 1 || user.PlaceId == null) return parcels;

            else return parcels
                    .Where(p => p.PlaceOfDeliveryId == user.PlaceId || 
                        p.PlaceOfDepartureId == user.PlaceId ||
                        p.PlaceOfDeliveryId == null ||
                        p.PlaceOfDepartureId == null);
        }
    }
}
