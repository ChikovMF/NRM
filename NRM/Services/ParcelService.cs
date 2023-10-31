using Microsoft.EntityFrameworkCore;
using NRM.Models.DataModels;
using NRM.Models.ParcelModels;
using NRM.Pages;
using NRM.Services.Queries;

namespace NRM.Services
{
    public class ParcelService
    {
        private AppDbContext _context { get; set; }

        public ParcelService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавление новой посылки.
        /// </summary>
        /// <param name="createModel">Данные посылки для её создания.</param>
        /// <returns>bool значение, true - сохранение успешно завершено.</returns>
        public async Task<bool> CreateParcel(CreateModel createModel, string login)
        {
            if (ParcelRepeatCheck(createModel.TrackNumber))
            {
                var parcel = createModel.ToParcel();

                var placeOfDeliveryId = await _context
                    .MilitaryUnits
                    .Where(m => m.Id == createModel.MilitaryUnitId)
                    .Select(m => m.PlaceId).FirstAsync();

                parcel.PlaceOfDeliveryId = placeOfDeliveryId;

                parcel.LogParcels = new List<Models.DataModels.LogParcel>();
                parcel.LogParcels.Add(new Models.DataModels.LogParcel
                {
                    Parcel = parcel,
                    TypeId = 6,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Time = TimeOnly.FromDateTime(DateTime.Now),
                    UserId = _context.Users.First(f => f.Login == login).Id,
                    Message = $"Создана РПО с трек-номером {parcel.TrackNumber}. " +
                        $"Пользователь создавший РПО: {login}. " +
                        $"Время создания: {TimeOnly.FromDateTime(DateTime.Now)} {DateOnly.FromDateTime(DateTime.Now)}"
                });
                await _context.Parcels.AddAsync(parcel);
                _context.SaveChanges();
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Проверка на уникальность трек-номера посылки.
        /// </summary>
        /// <param name="trackNumber">Трек-номер посылки.</param>
        /// <returns>bool значение, true - трек-номер уникален.</returns>
        private bool ParcelRepeatCheck(string trackNumber)
        {
            int count = _context.Parcels.Where(w => !w.IsDeleted && w.TrackNumber == trackNumber).Count();
            if (count == 0) return true;
            else return false;
        }

        /// <summary>
        /// Получение списка посылок.
        /// </summary>
        /// <returns>Список<TableModel> с посылками.</returns>
        public async Task<List<TableModel>> GetParcels(SortFilterParcelPageOptions options, string? login)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Login == login);

            if (user == null)
                throw new NullReferenceException($"Пользователь запрашивающий список гр. посылок ({login}) не найден.");

            return await _context.Parcels.Where(w => !w.IsDeleted)
                .ParcelFilterRole(user)
                .OrderParcelBy(options.OrderByOptions)
                .FilterParcelsBy(options.FilterBy, options.FilterValue)
                .Select(s => new TableModel
            {
                Recipient = s.Recipient,
                Sender = s.Sender,
                Status = s.Status.Name,
                TrackNumber = s.TrackNumber,
                Type = s.Type.Name,
                Id = s.Id,
                PlaceOfDelivery = s.PlaceOfDelivery.Name + ((s.MilitaryUnit != null) ? $" (в/ч {s.MilitaryUnit.Name})" : string.Empty),
                DateTime = s.LogParcels.Where(w => w.TypeId == 6).Select(s => $"{s.Time.ToShortTimeString()} {s.Date.ToShortDateString()}").First(),
            }).ToListAsync();
        }

        /// <summary>
        /// Получение Select с типами посылки
        /// </summary>
        /// <returns>Список типов посылки</returns>
        public async Task<List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>> GetParcelTypesSelect()
        {
            var SelectList = await _context.ParcelTypes.Where(w => !w.IsDeleted).Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToListAsync();
            return SelectList;
        }

        /// <summary>
        /// Получение Select со статусами посылки
        /// </summary>
        /// <returns>Список статусов посылки</returns>
        public async Task<List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>> GetParcelStatusSelect()
        {
            var SelectList = await _context.ParcelStatus.Where(w => !w.IsDeleted).Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToListAsync();
            return SelectList;
        }

        /// <summary>
        /// Генерация случайного трек-номера посылки
        /// </summary>
        /// <returns>строку с трек-номером</returns>
        public string RandomTrackNumber()
        {
            int sizeTN = 14;
            string trackNumber = String.Empty;
            char[] firstChar = "ABCDEFHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            Random random = new Random();
            do
            {
                trackNumber += firstChar[random.Next(0, firstChar.Length - 1)];
                for (int i = 1; i < sizeTN; i++)
                {
                    trackNumber += letters[random.Next(0, letters.Length - 1)];
                }
            }
            while (!ParcelRepeatCheck(trackNumber));
            return trackNumber;
        }

        /// <summary>
        /// Получение данных посылки.
        /// </summary>
        /// <param name="id">id посылки</param>
        /// <returns>Данные посылки</returns>
        public async Task<ViewModel> ViewParcel(int id)
        {
            var parcel = await _context.Parcels.Where(w => !w.IsDeleted && w.Id == id).Select(s => new ViewModel
            {
                Id = s.Id,
                DepartureDate = s.DepartureDate,
                DepartureTime = s.DepartureTime,
                Sender = s.Sender,
                Recipient = s.Recipient,
                PlaceOfDeparture = s.PlaceOfDeparture.Name,
                PlaceOfDelivery = s.PlaceOfDelivery.Name,
                MilitaryUnit = s.MilitaryUnit.Name,
                Status = new ViewModel.Item
                {
                    Id = s.Status.Id,
                    Name = s.Status.Name
                },
                TrackNumber = s.TrackNumber,
                LogParcels = _context.LogParcels.Where(w => w.ParcelId == id).OrderByDescending(o => o.Date).ThenByDescending(t => t.Time)
                    .Select(s => new ViewModel.ItemLogParcels()
                {
                    Id = s.Id,
                    Message = s.Message,
                    Type = s.Type.Name
                }).ToList(),
                Type = new ViewModel.Item
                {
                    Id = s.Type.Id,
                    Name = s.Type.Name
                },
                GroupParcel = s.GroupParcelId != null ? (!s.GroupParcel.IsDeleted ? new ViewModel.ItemGroupParcel { Id = s.GroupParcel.Id, TrackNumber = s.GroupParcel.TrackNumber } : null) : null
            }).FirstOrDefaultAsync();

            return parcel;
        }

        /// <summary>
        /// Получение публичных данных посылки.
        /// </summary>
        /// <param name="id">id посылки</param>
        /// <returns>Публичные данные посылки</returns>
        public async Task<PublicViewModel> ViewPublicParcel(int id)
        {
            var parcel = await _context.Parcels.Where(w => !w.IsDeleted && w.Id == id).Select(s => new PublicViewModel
            {
                Id = s.Id,
                DepartureDate = s.DepartureDate,
                DepartureTime = s.DepartureTime,
                Sender = s.Sender,
                Recipient = s.Recipient,
                TrackNumber = s.TrackNumber,
                Type = s.Type.Name,
                Status = s.Status.Name
            }).FirstOrDefaultAsync();

            return parcel;
        }

        /// <summary>
        /// Удаление посылки
        /// </summary>
        /// <param name="id">id посылки</param>
        /// <returns></returns>
        public async Task DeleteParcel(int id, string login)
        {
            var parcel = await _context.Parcels.Where(w => !w.IsDeleted && w.Id == id).Include(i => i.LogParcels).FirstOrDefaultAsync();
            if (parcel != null)
            {
                parcel.IsDeleted = true;
                parcel.LogParcels.Add(new Models.DataModels.LogParcel
                {
                    Parcel = parcel,
                    TypeId = 4,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Time = TimeOnly.FromDateTime(DateTime.Now),
                    UserId = _context.Users.First(f => f.Login == login).Id,
                    Message = $"Удалена РПО с трек-номером {parcel.TrackNumber}. " +
                    $"Пользователь удаливший РПО: {login}. " +
                    $"Время удаления: {TimeOnly.FromDateTime(DateTime.Now)} {DateOnly.FromDateTime(DateTime.Now)}"
                });
                _context.SaveChanges();
            }
            
        }

        /// <summary>
        /// Изменение данных посылки.
        /// </summary>
        /// <param name="id">id посылки</param>
        /// <returns>true - изменение данных прошло успешно</returns>
        public async Task<bool> EditParcel(EditModel editModel, int id, string login)
        {
            var parcel = _context.Parcels.Where(w => !w.IsDeleted && w.Id == id).Include(i => i.LogParcels).FirstOrDefault();
            if(parcel != null)
            {
                editModel.UpdateParcel(parcel);
                parcel.LogParcels.Add(new Models.DataModels.LogParcel
                {
                    Parcel = parcel,
                    TypeId = 1,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Time = TimeOnly.FromDateTime(DateTime.Now),
                    UserId = _context.Users.First(f => f.Login == login).Id,
                    Message = $"Изменение данных РПО с трек-номером {parcel.TrackNumber}. " +
                    $"Пользователь изменивший РПО: {login}. " +
                    $"Время изменения: {TimeOnly.FromDateTime(DateTime.Now)} {DateOnly.FromDateTime(DateTime.Now)}"
                });
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Начальные данные для изменения посылки.
        /// </summary>
        /// <param name="id">id посылки</param>
        /// <returns>EditModel</returns>
        public async Task<EditModel> ViewEditParcel(int id)
        {
            var parcel = _context.Parcels.Where(w => !w.IsDeleted && w.Id == id).Select(s => new EditModel
            {
                DepartureDate = s.DepartureDate,
                DepartureTime = s.DepartureTime,
                Recipient = s.Recipient,
                Sender = s.Sender,
                TrackNumber = s.TrackNumber,
                TypeItems = GetParcelTypesSelect().Result,
                StatusItems = GetParcelStatusSelect().Result,
            }).FirstOrDefault();
            return parcel;
        }

        /// <summary>
        /// Проверка на существование посылки с переданным трек-номером
        /// </summary>
        /// <param name="trackNumber">трек-номер посылки</param>
        /// <returns>id - посылки</returns>
        public async Task<int> ExistenceCheck(string trackNumber)
        {
           return await _context.Parcels.Where(w => !w.IsDeleted && w.TrackNumber == trackNumber).Select(s => s.Id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Смена статуса посылки
        /// </summary>
        /// <param name="statusId">id нового статуса</param>
        /// <param name="placeId">id посылки</param>
        /// <param name="login">логин пользователя</param>
        /// <returns>true - статус успешно сменен</returns>
        public async Task<bool> ChangeStatusParcel(int statusId,int placeId, string login)
        {
            var user = await _context.Users.AsNoTracking().Include(u => u.Place).FirstOrDefaultAsync(u => u.Login == login);

            if (user == null)
            {
                throw new NullReferenceException("Пользователь запрашивающий смену статуса не найден");
            }

            var parcel = await _context.Parcels.Where(w => !w.IsDeleted && w.Id == placeId).Include(i => i.LogParcels).FirstOrDefaultAsync();
            if(parcel != null)
            {
                parcel.StatusId = statusId;
                parcel.LogParcels.Add(new Models.DataModels.LogParcel
                {
                    Parcel = parcel,
                    TypeId = 8,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Time = TimeOnly.FromDateTime(DateTime.Now),
                    UserId = _context.Users.First(f => f.Login == login).Id,
                    Message = $"Смена статуса РПО с трек-номером {parcel.TrackNumber}. " +
                        $"Новый статус: {_context.ParcelStatus.FirstOrDefault(p => p.Id == statusId).Name}. " +
                        $"Пользователь сменивший статус РПО: {login}. " +
                        ((user.Place == null) ? string.Empty : $"Место смены статуса: {user.Place.Name}. ") +
                        $"Время смены статуса: {TimeOnly.FromDateTime(DateTime.Now)} {DateOnly.FromDateTime(DateTime.Now)}"
                });
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<CreateModel.StartItems> GetStartItems(string? login)
        {
            return await _context.Users.Where(w => !w.IsDeleted && w.Login == login).Select(s => new Models.ParcelModels.CreateModel.StartItems()
            {
                Place = s.Place.Name,
                PlaceId = s.PlaceId
            }).FirstOrDefaultAsync();
        }
    }
}