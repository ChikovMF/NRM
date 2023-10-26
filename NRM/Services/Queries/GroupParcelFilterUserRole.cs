using NRM.Models.DataModels;

namespace NRM.Services.Queries
{
    public static class GroupParcelFilterUserRole
    {
        public static IQueryable<GroupParcel> GroupParcelFilterRole(this IQueryable<GroupParcel> groupParcels, User user)
        {
            if (user.RoleId == 1) return groupParcels;

            else return groupParcels.Where(gp => gp.PlaceOfDeliveryId == user.PlaceId || gp.PlaceOfDepartureId == user.PlaceId);
        }
    }
}
