using Microsoft.EntityFrameworkCore;
using NRM.Models.LogTypeModels;

namespace NRM.Services
{
    public class LogTypeService
    {
        private AppDbContext _context { get; set; }

        public LogTypeService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение списка типов логов.
        /// </summary>
        /// <returns>Список TableModel с типами логов.</returns>
        public async Task<List<TableModel>> GetTableLogTypes()
        {
            return await _context.LogTypes.Where(w => !w.IsDeleted).Select(s => new TableModel
            {
                Name = s.Name,
                Id = s.Id,
            }).ToListAsync();
        }

        /// <summary>
        /// Добавление нового типа логов.
        /// </summary>
        /// <param name="createModel">Данные типа логов для его создания.</param>
        /// <returns>bool значение, true сохранение успешно завершено.</returns>
        public async Task<bool> CreateLogType(CreateModel createModel)
        {
            if (LogTypeRepeatCheck(createModel.Name))
            {
                await _context.LogTypes.AddAsync(createModel.ToLogType());
                _context.SaveChanges();
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Проверка на уникальность типа логов.
        /// </summary>
        /// <param name="name">Название типа логов.</param>
        /// <returns>bool значение, true - тип логов уникален.</returns>
        private bool LogTypeRepeatCheck(string name)
        {
            int count = _context.LogTypes.Where(w => !w.IsDeleted && w.Name == name).Count();
            if (count == 0) return true;
            else return false;
        }

        /// <summary>
        /// Получение данных типа логов.
        /// </summary>
        /// <param name="id">id типа логов</param>
        /// <returns>Данные типа логов</returns>
        public async Task<ViewModel> ViewLogType(int id)
        {
            return await _context.LogTypes.Where(w => !w.IsDeleted && w.Id == id).Select(s => new ViewModel
            {
                Name = s.Name,
                Logs = s.Logs.OrderByDescending(o => o.Date).ThenByDescending(t => t.Time).Select(l => new ViewModel.ItemLog
                {
                    Id = l.Id,
                    Message = l.Message,
                    Date = l.Date,
                    Time = l.Time
                }).ToList()
            }).FirstOrDefaultAsync();
        }
    }
}
