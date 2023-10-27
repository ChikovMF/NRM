using NRM.Models.DataModels;

namespace NRM.Models.PlaceModels
{
    public class ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ItemUser>? Users { get; set; }
        public List<ItemGroupParcel>? GroupParcels { get; set; }
        public List<MilitaryUnit>? MilitaryUnits { get; set; }

        public class ItemUser
        {
            public int Id { get; set; }
            public string Login { get; set; }
            public string FullName { get; set; }
        }

        public class ItemGroupParcel
        {
            public int Id { get; set; }
            public string TrackNumber { get; set; }
            public int ParcelCount { get; set; }
            public string Status { get; set; }
        }
    }
}
