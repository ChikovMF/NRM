﻿using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using NRM.Models.DataModels;

namespace NRM
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<GroupParcel> GroupParcels { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<ParcelStatus> ParcelStatus { get; set; }
        public DbSet<ParcelType> ParcelTypes { get; set; }
        public DbSet<LogType> LogTypes { get; set; }
        public DbSet<RelationLogType> RelationLogTypes { get; set; }
        public DbSet<LogParcel> LogParcels { get; set; }
        public DbSet<LogGroupParcel> LogGroupParcels { get; set; }
        public DbSet<MilitaryUnit> MilitaryUnits { get; set; }
        public DbSet<InvoicePhoto> InvoicePhoto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData( new Role[]
            {
                new Role
                {
                    Id = 1,
                    IsDeleted = false,
                    Name = "Администратор",
                },
                new Role
                {
                    Id = 2,
                    IsDeleted = false,
                    Name = "Администратор узла",
                },
                new Role
                {
                    Id = 3,
                    IsDeleted = false,
                    Name = "Оператор отправлений",
                },
                new Role
                {
                    Id = 4,
                    IsDeleted = false,
                    Name = "Пользователь",
                }
            });
            modelBuilder.Entity<Place>().HasData(new Place[]
            {
                new Place
                {
                    Id = 1,
                    IsDeleted = false,
                    Name = "6 ЦУ ФПС",
                },
                new Place
                {
                    Id = 2,
                    IsDeleted = false,
                    Name = "УФПС в/ч 31895",
                },
                new Place
                {
                    Id = 3,
                    IsDeleted = false,
                    Name = "УФПС в/ч 71609",
                }
            });
            modelBuilder.Entity<MilitaryUnit>().HasData(new MilitaryUnit[]
            {
                new() {
                    Id = 1,
                    Name = "18425",
                    PlaceId = 2,
                },
                new() {
                    Id = 2,
                    Name = "29303",
                    PlaceId = 2,
                },
                new() {
                    Id = 3,
                    Name = "29310",
                    PlaceId = 2,
                },
                new() {
                    Id = 4,
                    Name = "17640",
                    PlaceId = 2,
                },
                new() {
                    Id = 5,
                    Name = "33840",
                    PlaceId = 3,
                },
                new() {
                    Id = 6,
                    Name = "45420",
                    PlaceId = 3,
                },
                new() {
                    Id = 7,
                    Name = "28460",
                    PlaceId = 3,
                },
                new() {
                    Id = 8,
                    Name = "63940",
                    PlaceId = 3,
                },
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
                    RoleId = 1,
                    PlaceId = 1,
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
                    RoleId = 3,
                    PlaceId = 1,
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
                    RoleId = 2,
                    PlaceId = 1,
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
                    RoleId = 4,
                    PlaceId = 1,
                },
                new()
                {
                    Id = 5,
                    IsDeleted = false,
                    Login = "fe1",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123!@#qweQWE"),
                    Email = "fe1@mail.ru",
                    PhoneNumber = "+79114561223",
                    LastName = "Быстров",
                    FirstName = "Алексей",
                    Patronymic = "Петрович",
                    RoleId = 3,
                    PlaceId = 1,
                },
                new()
                {
                    Id = 6,
                    IsDeleted = false,
                    Login = "fe2",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123!@#qweQWE"),
                    Email = "fe1@mail.ru",
                    PhoneNumber = "+79114567889",
                    LastName = "Быстров",
                    FirstName = "Николай",
                    Patronymic = "Ефимович",
                    RoleId = 3,
                    PlaceId = 1,
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
                    Name = "Доставляется"
                },
                new ParcelStatus
                {
                    Id = 5,
                    Name = "Получена промежуточным узлом"
                },
                new ParcelStatus
                {
                    Id = 6,
                    Name = "Отправлено с промежуточного узла"
                },
                new ParcelStatus
                {
                    Id = 7,
                    Name = "Получена конечным узлом"
                },
                new ParcelStatus
                {
                    Id = 8,
                    Name = "Ожидает вручения"
                },
                new ParcelStatus
                {
                    Id = 9,
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
                new ParcelType
                {
                    Id = 5,
                    Name = "Письмо",
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
                    Name = "РПО",
                },
                new RelationLogType
                {
                    Id = 3,
                    Name = "Группа РПО",
                },
            });
            modelBuilder.Entity<LogType>().HasData(new LogType[]
            {
                new LogType
                {
                    Id = 1,
                    Name = "Смена данных РПО",
                    RelationId = 2,
                },
                new LogType
                {
                    Id = 2,
                    Name = "Смена данных группы РПО",
                    RelationId = 3,
                },
                new LogType
                {
                    Id = 3,
                    Name = "Смена данных родительской группы РПО",
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
                    Name = "Добавление РПО в группу",
                    RelationId = 2,
                },
                new LogType
                {
                    Id = 6,
                    Name = "Создание РПО",
                    RelationId = 2,
                },
                new LogType
                {
                    Id = 7,
                    Name = "Создание группы РПО",
                    RelationId = 3,
                },
                new LogType
                {
                    Id = 8,
                    Name = "Смена статуса РПО",
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
                    Name = "Смена статуса из-за смены статуса родителя РПО",
                    RelationId = 2,
                },
                new LogType
                {
                    Id = 11,
                    Name = "Удаление места отправки/доставки РПО",
                    RelationId = 2,
                },
                new LogType
                {
                    Id = 12,
                    Name = "Удаление места отправки/доставки группы РПО",
                    RelationId = 3,
                },
                new LogType
                {
                    Id = 13,
                    Name = "РПО разгруппировано",
                    RelationId = 2,
                },
            });
        }
    }
}
