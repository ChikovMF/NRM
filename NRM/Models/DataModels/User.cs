namespace NRM.Models.DataModels
{
    public class User
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }
        
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        public int? PlaceId { get; set; }
        public Place? Place { get; set; }

        public List<GroupParcel>? GroupParcels { get; set; }
    }
}
