using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using NRM.Models.DataModels;
using NRM.Models.GroupParcelModels;
using NRM.Services.Queries;
using System.Linq;

namespace NRM.Services
{
    public class GroupParcelService
    {
        private AppDbContext _context;
        
        public GroupParcelService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавление новой групповой посылки.
        /// </summary>
        /// <param name="createModel">Данные групповой посылки для её создания.</param>
        /// <returns>bool значение, true - сохранение успешно завершено.</returns>
        public async Task<string> CreateGroupParcel(CreateModel createModel, string login)
        {
            string errorString = string.Empty;

            if (GroupParcelRepeatCheck(createModel.TrackNumber))
            {
                var user = await _context.Users.AsNoTracking().Include(u => u.Place).FirstAsync(u => u.Login == login);

                // Проверка пользователя (имеет ли он доступ устанавливать ответственного)
                if(user.RoleId != 1)
                {
                    createModel.UserId = user.Id;
                }
                else
                {
                    if(createModel.UserId == null || createModel.UserId == 0)
                    {
                        errorString = "Введите ответственного";
                        return errorString;
                    }
                }

                var groupParcel = createModel.ToGroupParcel();
                groupParcel.Parcels = new();
                foreach(int id in createModel.ParcelsId)
                {
                    groupParcel = await AddingToGroup(groupParcel, id);
                }
                groupParcel.LogGroupParcels = new List<LogGroupParcel>();
                groupParcel.LogGroupParcels.Add(new LogGroupParcel
                {
                    GroupParcel = groupParcel,
                    TypeId = 6,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Time = TimeOnly.FromDateTime(DateTime.Now),
                    UserId = _context.Users.First(f => f.Login == login).Id,
                    Message = $"Создана группа РПО с трек-номером {groupParcel.TrackNumber}. " +
                    $"Пользователь создавший группу РПО: {login}. " +
                    ((user.Place == null) ? string.Empty : $"Место создания группы РПО: {user.Place.Name}. ") +
                    $"Время создания: {DateOnly.FromDateTime(DateTime.Now)} {TimeOnly.FromDateTime(DateTime.Now)}"
                });
                await _context.GroupParcels.AddAsync(groupParcel);
                int ch = await _context.SaveChangesAsync();

                if (ch == 0)
                {
                    errorString = "Ошибка создания РПО";
                }
            }
            return errorString;
        }

        /// <summary>
        /// Добавление посылки в группу
        /// </summary>
        /// <param name="groupParcel">Групповая посылка</param>
        /// <param name="parcelId">Id посылка</param>
        /// <returns>Групповая посылка</returns>
        public async Task<GroupParcel> AddingToGroup(GroupParcel groupParcel, int parcelId)
        {
            var parcel = await _context.Parcels.Where(w => !w.IsDeleted && w.Id == parcelId).FirstOrDefaultAsync();
            if (parcel != null)
            {
                parcel.StatusId = groupParcel.StatusId;
                parcel.GroupParcel = groupParcel;
                groupParcel.Parcels.Add(parcel);
            }
            return groupParcel;
        }

        /// <summary>
        /// Проверка на уникальность трек-номера групповой посылки.
        /// </summary>
        /// <param name="trackNumber">Трек-номер групповой посылки.</param>
        /// <returns>bool значение, true - трек-номер уникален.</returns>
        private bool GroupParcelRepeatCheck(string trackNumber)
        {
            int count = _context.GroupParcels.Where(w => !w.IsDeleted && w.TrackNumber == trackNumber).Count();
            if (count == 0) return true;
            else return false;
        }

        /// <summary>
        /// Генерация случайного трек-номера групповой посылки
        /// </summary>
        /// <returns>строку с трек-номером</returns>
        public string RandomTrackNumber()
        {
            int sizeTN = 14;
            string trackNumber = String.Empty;
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            Random random = new Random();
            do
            {
                trackNumber += "G";
                for (int i = 1; i < sizeTN; i++)
                {
                    trackNumber += letters[random.Next(0, letters.Length - 1)];
                }
            }
            while (!GroupParcelRepeatCheck(trackNumber));
            return trackNumber;
        }

        /// <summary>
        /// Получение списка групповых посылок.
        /// </summary>
        /// <returns>Список<TableModel> с групповыми посылками.</returns>
        public async Task<List<TableModel>> GetGroupParcels(string userLogin)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Login == userLogin);

            if (user == null)
                throw new NullReferenceException($"Пользователь запрашивающий список групп РПО ({userLogin}) не найден.");

            return await _context.GroupParcels.Where(w => !w.IsDeleted).GroupParcelFilterRole(user)
                .OrderByDescending(o => o.DepartureDate).ThenByDescending(t => t.DepartureTime).Select(s => new TableModel
            {
                Id = s.Id,
                DepartureDateTime = $"{s.DepartureTime.ToShortTimeString()} {s.DepartureDate.ToShortDateString()}",
                ParcelCount = s.Parcels.Where(w => !w.IsDeleted).Count(),
                PlaceOfDelivery = s.PlaceOfDelivery.Name,
                PlaceOfDeparture = s.PlaceOfDeparture.Name,
                Status = s.Status.Name,
                TrackNumber = s.TrackNumber
            }).ToListAsync();
        }

        /// <summary>
        /// Получение ответственного по умолчанию
        /// </summary>
        /// <param name="login">логин пользователя</param>
        /// <returns>ФИО и логин</returns>
        public async Task<CreateModel.StartUserItem> GetStartUser(string login)
        {
            return await _context.Users.Where(w => !w.IsDeleted && w.Login == login).Select(s => new CreateModel.StartUserItem()
            {
                FullName = $"{s.LastName} {s.FirstName} {s.Patronymic} ({s.Login})",
                Id = s.Id,
                Place = s.Place.Name,
                PlaceId = s.PlaceId
            }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Получение данных групповой посылки.
        /// </summary>
        /// <param name="id">id групповой посылки</param>
        /// <returns>Данные групповой посылки</returns>
        public async Task<ViewModel> ViewGroupParcel(int id)
        {
            var groupParcel = await _context.GroupParcels.Where(w => !w.IsDeleted && w.Id == id)
                .Select(s => new ViewModel
            {
                Id = s.Id,
                DepartureDate = s.DepartureDate,
                DepartureTime = s.DepartureTime,
                TrackNumber = s.TrackNumber,
                LogGroupParcels = _context.LogGroupParcels.Where(w => w.GroupParcelId == id).OrderByDescending(o => o.Date).ThenByDescending(t => t.Time)
                    .Select(l => new ViewModel.ItemLogGroupParcels()
                {
                    Id = l.Id,
                    Message = l.Message,
                    Type = l.Type.Name
                }).ToList(),
                Parcels = s.Parcels.Where(w => !w.IsDeleted)
                .Select(p => new ViewModel.ParcelItem()
                {
                    Id = p.Id,
                    TrackNumber = p.TrackNumber,
                    Sender = p.Sender,
                    Recipient = p.Recipient,
                    Type = new ViewModel.Item { Id = p.Type.Id, Name = p.Type.Name },
                    MilitaryUnit = (p.MilitaryUnit != null) ? $"В/ч {p.MilitaryUnit.Name}" : string.Empty
                }).ToList(),
                PlaceOfDelivery = new ViewModel.Item { Id = s.PlaceOfDelivery.Id, Name= s.PlaceOfDelivery.Name },
                PlaceOfDeparture = new ViewModel.Item { Id = s.PlaceOfDeparture.Id, Name = s.PlaceOfDeparture.Name },
                Status = new ViewModel.Item { Id = s.Status.Id, Name = s.Status.Name},
                User = new ViewModel.UserItem { Id = s.User.Id, 
                                                Name = s.User.Login, 
                                                UName = $"{ s.User.LastName } " + 
                                                    $"{s.User.FirstName} " + $"{s.User.Patronymic}", 
                                                UWork = ((s.User.Place == null) ? string.Empty : s.User.Place.Name)
                }
                }).FirstOrDefaultAsync();

            return groupParcel;
        }

        /// <summary>
        /// Удаление групповой посылки
        /// </summary>
        /// <param name="id">id групповой посылки</param>
        /// <returns></returns>
        public async Task DeleteGroupParcel(int id, string login)
        {
            var user = await _context.Users.AsNoTracking().Include(u => u.Place).FirstOrDefaultAsync(u => u.Login == login);

            var status = await _context.ParcelStatus.FirstOrDefaultAsync(p => p.Id == 1);

            var groupParcel = await _context.GroupParcels.Where(w => !w.IsDeleted && w.Id == id)
                .Include(i => i.Parcels).ThenInclude(t => t.LogParcels).Include(i => i.LogGroupParcels).FirstOrDefaultAsync();
            if (groupParcel != null)
            {
                groupParcel.IsDeleted = true;
                groupParcel.LogGroupParcels.Add(new Models.DataModels.LogGroupParcel
                {
                    GroupParcel = groupParcel,
                    TypeId = 4,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Time = TimeOnly.FromDateTime(DateTime.Now),
                    UserId = _context.Users.First(f => f.Login == login).Id,
                    Message = $"Удалена группа РПО с трек-номером {groupParcel.TrackNumber}. " +
                    $"Пользователь удаливший группу РПО: {login}. " +
                    $"Время удаления группы РПО: {TimeOnly.FromDateTime(DateTime.Now)} {DateOnly.FromDateTime(DateTime.Now)}"
                });
                if(groupParcel.Parcels != null)
                {                    
                    foreach (var parcel in groupParcel.Parcels)
                    {
                        parcel.GroupParcel = null;
                        parcel.StatusId = 1;
                        parcel.LogParcels.Add(new LogParcel
                        {
                            Parcel = parcel,
                            TypeId = 13,
                            Date = DateOnly.FromDateTime(DateTime.Now),
                            Time = TimeOnly.FromDateTime(DateTime.Now),
                            UserId = _context.Users.First(f => f.Login == login).Id,
                            Message = $"РПО с трек-номером {parcel.TrackNumber} разгруппировано. " +
                            $"Пользователь совершивший действие: {login}. " +
                            $"Время действия: {TimeOnly.FromDateTime(DateTime.Now)} {DateOnly.FromDateTime(DateTime.Now)}"
                        });
                        parcel.LogParcels.Add(new LogParcel
                        {
                            Parcel = parcel,
                            TypeId = 8,
                            Date = DateOnly.FromDateTime(DateTime.Now),
                            Time = TimeOnly.FromDateTime(DateTime.Now),
                            UserId = _context.Users.First(f => f.Login == login).Id, 
                            Message = $"Смена статуса РПО с трек-номером {parcel.TrackNumber}. " +
                            $"Новый статус: {status.Name}. " +
                            $"Пользователь сменивший статус РПО: {login}. " +
                            ((user.Place == null) ? string.Empty : $"Место смены статуса: {user.Place.Name}. ") +
                            $"Время смены статуса: {TimeOnly.FromDateTime(DateTime.Now)} {DateOnly.FromDateTime(DateTime.Now)}"
                            });
                    }
                }
                _context.SaveChanges();
            }

        }

        /// <summary>
        /// Получение Select со статусами групповой посылки
        /// </summary>
        /// <returns>Список статусов групповой посылки</returns>
        public async Task<List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>> GetGroupParcelStatusSelect()
        {
            var SelectList = await _context.ParcelStatus.Where(w => !w.IsDeleted).Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToListAsync();
            return SelectList;
        }

        /// <summary>
        /// Получение места по умолчанию
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EditModel.Item> GetPlaceStart(int id)
        {
            return await _context.Places.Where(w => !w.IsDeleted && w.Id == id).Select(s => new EditModel.Item
            {
                Id = s.Id,
                Name = s.Name,
            }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Изменение данных групповой посылки.
        /// </summary>
        /// <param name="id">id групповой посылки</param>
        /// <returns>true - изменение данных прошло успешно</returns>
        public async Task<string> EditGroupParcel(EditModel editModel, int id, string login)
        {
            string errorString = String.Empty;

            var user = await _context.Users.AsNoTracking().Include(u => u.Place).FirstAsync(u => u.Login == login);

            var groupParcel = await _context.GroupParcels.Where(w => !w.IsDeleted && w.Id == id)
                .Include(i => i.LogGroupParcels).Include(i => i.Parcels).FirstOrDefaultAsync();

            // Проверка пользователя (имеет ли он доступ устанавливать ответственного)
            if (user.RoleId != 1)
            {
                editModel.UserId = user.Id;
            }
            else
            {
                if (editModel.UserId == null || editModel.UserId == 0)
                {
                    errorString = "Введите ответственного";
                    return errorString;
                }
            }

            if (groupParcel != null)
            {
                var userId = _context.Users.First(f => f.Login == login).Id;
                editModel.UpdateGroupParcel(groupParcel);
                groupParcel.LogGroupParcels.Add(new LogGroupParcel
                {
                    GroupParcel = groupParcel,
                    TypeId = 2,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Time = TimeOnly.FromDateTime(DateTime.Now),
                    UserId = userId,
                    Message = $"Изменение данных группы РПО с трек-номером {groupParcel.TrackNumber}. " +
                    $"Пользователь изменивший группу РПО: {login}. " +
                    $"Время изменения: {TimeOnly.FromDateTime(DateTime.Now)} {DateOnly.FromDateTime(DateTime.Now)}"
                });

                groupParcel.Parcels.Clear();

                foreach (var parcelId in editModel.ParcelsId)
                {
                    groupParcel = await AddingToGroup(groupParcel, parcelId);
                }
                int ch = await _context.SaveChangesAsync();

                if (ch == 0)
                {
                    errorString = "Ошибка создания РПО";
                }
            }
            return errorString;
        }

        /// <summary>
        /// Начальные данные для изменения групповой посылки.
        /// </summary>
        /// <param name="id">id групповой посылки</param>
        /// <returns>EditModel</returns>
        public async Task<EditModel> ViewEditParcel(int id, string login)
        {
            var groupParcel = await _context.GroupParcels.Where(w => !w.IsDeleted && w.Id == id).Select(s => new EditModel
            {
                DepartureDate = s.DepartureDate,
                DepartureTime = s.DepartureTime,
                TrackNumber = s.TrackNumber,
                StatusItems = GetGroupParcelStatusSelect().Result,
                StartUser = GetStartUser(login).Result,
                PlaceOfDeliveryNow = _context.Places.Where(w => !w.IsDeleted && w.Id == s.PlaceOfDeliveryId).Select(s => new EditModel.Item
                {
                    Id = s.Id,
                    Name = s.Name,
                }).FirstOrDefault(),
                PlaceOfDepartureNow = _context.Places.Where(w => !w.IsDeleted && w.Id == s.PlaceOfDepartureId).Select(s => new EditModel.Item
                {
                    Id = s.Id,
                    Name = s.Name,
                }).FirstOrDefault(),
                ParcelsNow = _context.Parcels.Where(w => !w.IsDeleted && w.GroupParcelId == id).Select(s => new EditModel.Item
                {
                    Id = s.Id,
                    Name = s.TrackNumber
                }).ToList()
            }).FirstOrDefaultAsync();
            return groupParcel;
        }

        /// <summary>
        /// Смена статуса групповой посылки
        /// </summary>
        /// <param name="statusId">id нового статуса</param>
        /// <param name="parcelId">id групповой посылки</param>
        /// <param name="login">логин пользователя</param>
        /// <returns>true - статус успешно сменен</returns>
        public async Task<bool> ChangeStatusGroupParcel(int statusId, int parcelId, string login)
        {
            var user = await _context.Users.AsNoTracking().Include(u => u.Place).FirstOrDefaultAsync(u => u.Login == login);

            var status = await _context.ParcelStatus.FirstOrDefaultAsync(p => p.Id == statusId);

            var groupParcel = await _context.GroupParcels.Where(w => !w.IsDeleted && w.Id == parcelId)
                .Include(i => i.LogGroupParcels).Include(i => i.Parcels).ThenInclude(t => t.LogParcels).FirstOrDefaultAsync();

            if (groupParcel != null)
            {
                groupParcel.StatusId = statusId;
                groupParcel.LogGroupParcels.Add(new Models.DataModels.LogGroupParcel
                {
                    GroupParcel = groupParcel,
                    TypeId = 9,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Time = TimeOnly.FromDateTime(DateTime.Now),
                    UserId = user.Id,
                    Message = $"Смена статуса группы РПО с трек-номером {groupParcel.TrackNumber}. " +
                        $"Новый статус: {status.Name}. " +
                        $"Пользователь сменивший статус группы РПО: {login}. " +
                        ((user.Place == null) ? string.Empty : $"Место смены статуса: {user.Place.Name}. ") +
                        $"Время смены статуса: {TimeOnly.FromDateTime(DateTime.Now)} {DateOnly.FromDateTime(DateTime.Now)}"
                });
                foreach(var parcel in groupParcel.Parcels)
                {
                    parcel.StatusId = statusId;
                    parcel.LogParcels.Add(new Models.DataModels.LogParcel
                    {
                        Parcel = parcel,
                        TypeId = 10,
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        Time = TimeOnly.FromDateTime(DateTime.Now),
                        UserId = _context.Users.First(f => f.Login == login).Id,
                        Message = $"Смена статуса РПО с трек-номером {parcel.TrackNumber}. " +
                        $"Новый статус: {status.Name}. " +
                        $"Пользователь сменивший статус РПО: {login}. " +
                        ((user.Place == null) ? string.Empty : $"Место смены статуса: {user.Place.Name}. ") +
                        $"Время смены статуса: {TimeOnly.FromDateTime(DateTime.Now)} {DateOnly.FromDateTime(DateTime.Now)}"
                    });
                }
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
