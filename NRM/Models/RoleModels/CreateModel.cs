using NRM.Models.DataModels;

namespace NRM.Models.RoleModels
{
    public class CreateModel
    {
        public string Name { get; set; }

        public Role ToRole()
        {
            return new Role
            {
                Name = Name,
                IsDeleted = false,
            };
        }
    }
}
