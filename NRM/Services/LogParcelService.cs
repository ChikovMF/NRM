using Microsoft.EntityFrameworkCore;
using NRM.Models.LogParcelModels;

namespace NRM.Services
{
    public class LogParcelService
    {
        private AppDbContext _context {  get; set; }

        public LogParcelService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение списка логов посылок.
        /// </summary>
        /// <returns>Список с логами посылок.</returns>
        public async Task<List<TableModel>> GetTableLogParcels()
        {
            return await _context.LogParcels.OrderByDescending(o => o.Date).ThenByDescending(t => t.Time).Select(s => new TableModel
            {
                Id = s.Id,
                Message = s.Message,
                Date = s.Date,
                Time = s.Time
            }).ToListAsync();
        }

        /// <summary>
        /// Получение данных по логу посылки.
        /// </summary>
        /// <param name="id">id лога посылки</param>
        /// <returns>Данные по логу посылки</returns>
        public async Task<ViewModel> ViewLogParcel(int id)
        {
            var logParcel = await _context.LogParcels.Where(w => w.Id == id).Select(s => new ViewModel
            {
                Message = s.Message,
                Date = s.Date,
                Time = s.Time,
                Type = new ViewModel.TypeItem
                {
                    Id = s.Type.Id,
                    Name = s.Type.Name
                },
                User = new ViewModel.UserItem
                {
                    Id = s.User.Id,
                    Login = s.User.Login,
                    FullName = $"{s.User.LastName} {s.User.FirstName} {s.User.Patronymic}"
                }
            }).FirstOrDefaultAsync();

            if (logParcel == null)
            {
                logParcel = await _context.LogGroupParcels.Where(w => w.Id == id).Select(s => new ViewModel
                {
                    Message = s.Message,
                    Date = s.Date,
                    Time = s.Time,
                    Type = new ViewModel.TypeItem
                    {
                        Id = s.Type.Id,
                        Name = s.Type.Name
                    },
                    User = new ViewModel.UserItem
                    {
                        Id = s.User.Id,
                        Login = s.User.Login,
                        FullName = $"{s.User.LastName} {s.User.FirstName} {s.User.Patronymic}"
                    }
                }).FirstOrDefaultAsync();
            }

            return logParcel;
        }
    }
}
