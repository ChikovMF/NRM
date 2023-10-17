using NRM.Models.DataModels;

namespace NRM.Models.LogParcelModels
{
    public class ViewModel
    {
        public string Message { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        public TypeItem Type { get; set; }
        public UserItem User { get; set; }
        //public int ParcelId { get; set; }
        //public Parcel Parcel { get; set; }

        public class UserItem
        {
            public int Id { get; set; }
            public string Login { get; set; }
            public string FullName { get; set; }
        }

        public class TypeItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
