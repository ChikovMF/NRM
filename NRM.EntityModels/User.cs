namespace NRM.Models.DataModels
{
    public class User
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Login { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Patronymic { get; set; }
        
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
        public int? PlaceId { get; set; }
        public Place? Place { get; set; }

        public ICollection<GroupParcel>? GroupParcels { get; set; }
    }
}
