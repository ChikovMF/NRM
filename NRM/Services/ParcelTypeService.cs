using Microsoft.EntityFrameworkCore;
using NRM.Models.ParcelTypeModels;

namespace NRM.Services
{
    public class ParcelTypeService
    {
        private AppDbContext _context { get; set; }

        public ParcelTypeService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение списка типов.
        /// </summary>
        /// <returns>Список TableModel с типами.</returns>
        public async Task<List<TableModel>> GetTableParcelTypes()
        {
            return await _context.ParcelTypes.Where(w => !w.IsDeleted).Select(s => new TableModel
            {
                Name = s.Name,
                Id = s.Id,
            }).ToListAsync();
        }

        /// <summary>
        /// Добавление нового типа.
        /// </summary>
        /// <param name="createModel">Данные типа для его создания.</param>
        /// <returns>bool значение, true сохранение успешно завершено.</returns>
        public async Task<bool> CreateParcelType(CreateModel createModel)
        {
            if (ParcelTypeRepeatCheck(createModel.Name))
            {
                await _context.ParcelTypes.AddAsync(createModel.ToParcelType());
                _context.SaveChanges();
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Проверка на уникальность типа.
        /// </summary>
        /// <param name="name">Название типа.</param>
        /// <returns>bool значение, true - тип уникален.</returns>
        private bool ParcelTypeRepeatCheck(string name)
        {
            int count = _context.ParcelTypes.Where(w => !w.IsDeleted && w.Name == name).Count();
            if (count == 0) return true;
            else return false;
        }

        /// <summary>
        /// Получение данных типа посылки.
        /// </summary>
        /// <param name="id">id типа</param>
        /// <returns>Данные типа</returns>
        public async Task<ViewModel> ViewParcelType(int id)
        {
            return await _context.ParcelTypes.Where(w => !w.IsDeleted && w.Id == id).Select(s => new ViewModel
            {
                Name = s.Name,
                Parcels = _context.Parcels.Where(w => !w.IsDeleted && w.TypeId == id)
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
