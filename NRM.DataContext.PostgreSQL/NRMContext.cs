using Microsoft.EntityFrameworkCore;
using NRM.Models.DataModels;

namespace NRM.DataContext;

public class NRMContext : DbContext
{
    public NRMContext(DbContextOptions<NRMContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Parcel> Parcels { get; set; }
    public DbSet<GroupParcel> GroupParcels { get; set; }
    public DbSet<Place> Places { get; set; }
    public DbSet<ParcelStatus> ParcelStatus { get; set; }
    public DbSet<ParcelType> ParcelTypes { get; set; }
    // public DbSet<LogType> LogTypes { get; set; }
    // public DbSet<RelationLogType> RelationLogTypes { get; set; }
    // public DbSet<LogParcel> LogParcels { get; set; }
    // public DbSet<LogGroupParcel> LogGroupParcels { get; set; }
    public DbSet<MilitaryUnit> MilitaryUnits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData( new Role[]
        {
            new Role
            {
                Id = 1,
                IsDeleted = false,
                Name = "Полный администратор",
            },
            new Role
            {
                Id = 2,
                IsDeleted = false,
                Name = "Администратор пользователей",
            },
            new Role
            {
                Id = 3,
                IsDeleted = false,
                Name = "Администратор посылок",
            },
            new Role
            {
                Id = 4,
                IsDeleted = false,
                Name = "Пользователь",
            }
        });
        modelBuilder.Entity<User>().HasData( new User[]
        {
            new User
            {
                Id = 1,
                IsDeleted = false,
                Login = "Admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("-7_mQVKdYr.n2u9"),
                Email = "ia-matvey@mail.ru",
                PhoneNumber = "+79147291215",
                LastName = "Чиков",
                FirstName = "Матвей",
                Patronymic = "Федорович",
                RoleId = 1
            },
            new User
            {
                Id = 2,
                IsDeleted = false,
                Login = "AdminParcel",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("VBDW!UVLZPZZvR8"),
                Email = "AdminParcel@mail.ru",
                PhoneNumber = "89147291213",
                LastName = "Иванов",
                FirstName = "Иван",
                Patronymic = "Иванович",
                RoleId = 3
            },
            new User
            {
                Id = 3,
                IsDeleted = false,
                Login = "AdminUser",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("8s95:NT!j56EZg9"),
                Email = "AdminUser@mail.ru",
                PhoneNumber = "+79147291212",
                LastName = "Петров",
                FirstName = "Петр",
                Patronymic = "Петрович",
                RoleId = 2
            },
            new User
            {
                Id = 4,
                IsDeleted = false,
                Login = "User",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("u-Z:!d34ZQJAcPq"),
                Email = "User@mail.ru",
                PhoneNumber = "+79147291211",
                LastName = "Антонов",
                FirstName = "Антон",
                Patronymic = "Антонович",
                RoleId = 4
            },
        });
        modelBuilder.Entity<ParcelStatus>().HasData(new ParcelStatus[]
        {
            new ParcelStatus
            {
                Id = 1,
                Name = "Обрабатывается"
            },
            new ParcelStatus
            {
                Id = 2,
                Name = "Ожидает отправки"
            },
            new ParcelStatus
            {
                Id = 3,
                Name = "Отправлена"
            },
            new ParcelStatus
            {
                Id = 4,
                Name = "Получена промежуточным узлом"
            },
            new ParcelStatus
            {
                Id = 5,
                Name = "Отправлено с промежуточного узла"
            },
            new ParcelStatus
            {
                Id = 6,
                Name = "Получена конечным узлом"
            },
            new ParcelStatus
            {
                Id = 7,
                Name = "Ожидает вручения"
            },
            new ParcelStatus
            {
                Id = 8,
                Name = "Вручена"
            }
        });
        modelBuilder.Entity<ParcelType>().HasData(new ParcelType[]
        {
            new ParcelType
            {
                Id = 1,
                Name = "Посылка",
            },
            new ParcelType
            {
                Id = 2,
                Name = "Отправление EMS",
            },
            new ParcelType
            {
                Id = 3,
                Name = "Посылка 1-го класса",
            },
            new ParcelType
            {
                Id = 4,
                Name = "Бандероль",
            },
        });
        modelBuilder.Entity<RelationLogType>().HasData(new RelationLogType[]
        {
            new RelationLogType
            {
                Id = 1,
                Name = "Нет зависимостей",
            },
            new RelationLogType
            {
                Id = 2,
                Name = "Посылки",
            },
            new RelationLogType
            {
                Id = 3,
                Name = "Групповые посылки",
            },
        });
        modelBuilder.Entity<LogType>().HasData(new LogType[]
        {
            new LogType
            {
                Id = 1,
                Name = "Смена данных посылки",
                RelationId = 2,
            },
            new LogType
            {
                Id = 2,
                Name = "Смена данных групповой посылки",
                RelationId = 3,
            },
            new LogType
            {
                Id = 3,
                Name = "Смена данных родительской групповой посылки",
                RelationId = 2,
            },
            new LogType
            {
                Id = 4,
                Name = "Удаление",
                RelationId = 1,
            },
            new LogType
            {
                Id = 5,
                Name = "Добавление посылки в группу",
                RelationId = 2,
            },
            new LogType
            {
                Id = 6,
                Name = "Создание посылки",
                RelationId = 2,
            },
            new LogType
            {
                Id = 7,
                Name = "Создание групповой посылки",
                RelationId = 3,
            },
            new LogType
            {
                Id = 8,
                Name = "Смена статуса посылки",
                RelationId = 2,
            },
            new LogType
            {
                Id = 9,
                Name = "Смена статуса групповой посылки",
                RelationId = 3,
            },
            new LogType
            {
                Id = 10,
                Name = "Смена статуса из-за смены статуса родителя посылки",
                RelationId = 2,
            }
        });
    }
}
