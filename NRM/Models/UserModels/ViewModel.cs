namespace NRM.Models.UserModels
{
    public class ViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }
        public List<ItemGroupParcel>? GroupParcels { get; set; }

        public Item? Role { get; set; }
        public Item? Place { get; set; }
        public string? DeviceID { get; set; }
        public bool LoginAllowed { get; set; }

        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
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
