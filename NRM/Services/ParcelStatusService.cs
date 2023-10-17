using Microsoft.EntityFrameworkCore;
using NRM.Models.ParcelStatusModels;

namespace NRM.Services
{
    public class ParcelStatusService
    {
        private AppDbContext _context { get; set; }

        public ParcelStatusService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение списка статусов.
        /// </summary>
        /// <returns>Список TableModel со статусами.</returns>
        public async Task<List<TableModel>> GetTableParcelStatus()
        {
            return await _context.ParcelStatus.Where(w => !w.IsDeleted).Select(s => new TableModel
            {
                Name = s.Name,
                Id = s.Id,
            }).ToListAsync();
        }

        /// <summary>
        /// Добавление нового статуса.
        /// </summary>
        /// <param name="createModel">Данные статуса для его создания.</param>
        /// <returns>bool значение, true сохранение успешно завершено.</returns>
        public async Task<bool> CreateParcelStatus(CreateModel createModel)
        {
            if (ParcelStatusRepeatCheck(createModel.Name))
            {
                await _context.ParcelStatus.AddAsync(createModel.ToParcelStatus());
                _context.SaveChanges();
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Проверка на уникальность статуса.
        /// </summary>
        /// <param name="name">Название статуса.</param>
        /// <returns>bool значение, true - статус уникален.</returns>
        private bool ParcelStatusRepeatCheck(string name)
        {
            int count = _context.ParcelStatus.Where(w => !w.IsDeleted && w.Name == name).Count();
            if (count == 0) return true;
            else return false;
        }

        /// <summary>
        /// Получение данных статуса.
        /// </summary>
        /// <param name="id">id статуса</param>
        /// <returns>Данные статуса</returns>
        public async Task<ViewModel> ViewParcelStatus(int id)
        {
            return await _context.ParcelStatus.Where(w => !w.IsDeleted && w.Id == id).Select(s => new ViewModel
            {
                Name = s.Name,
                Parcels = _context.Parcels.Where(w => !w.IsDeleted && w.StatusId == id)
                    .Select(s => new ViewModel.ParcelItem()
                {
                    Id = s.Id,
                    TrackNumber = s.TrackNumber,
                    Sender = s.Sender,
                    Recipient = s.Recipient
                }).ToList(),
            }).FirstOrDefaultAsync();
        }
    }
}
