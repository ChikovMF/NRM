﻿using Microsoft.EntityFrameworkCore;
using NRM.Models.PlaceModels;

namespace NRM.Services
{
    public class PlaceService
    {
        private AppDbContext _context { get; set; }

        public PlaceService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение списка мест.
        /// </summary>
        /// <returns>Список TableModel с местами.</returns>
        public async Task<List<TableModel>> GetTablePlaces()
        {
            return await _context.Places.Where(w => !w.IsDeleted).Select(s => new TableModel
            {
                Name = s.Name,
                Id = s.Id,
            }).ToListAsync();
        }

        /// <summary>
        /// Добавление нового места.
        /// </summary>
        /// <param name="createModel">Данные места для его создания.</param>
        /// <returns>bool значение, true сохранение успешно завершено.</returns>
        public async Task<bool> CreatePlace(CreateModel createModel)
        {
            if (PlaceRepeatCheck(createModel.Name))
            {
                await _context.Places.AddAsync(createModel.ToPlace());
                _context.SaveChanges();
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Проверка на уникальность места.
        /// </summary>
        /// <param name="name">Название места.</param>
        /// <returns>bool значение, true - место уникален.</returns>
        private bool PlaceRepeatCheck(string name)
        {
            int count = _context.Places.Where(w => !w.IsDeleted && w.Name == name).Count();
            if (count == 0) return true;
            else return false;
        }

        /// <summary>
        /// Получение данных места.
        /// </summary>
        /// <param name="id">id места</param>
        /// <returns>Данные места</returns>
        public async Task<ViewModel> ViewPlace(int id)
        {
            return await _context.Places.Where(w => !w.IsDeleted && w.Id == id).Select(s => new ViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Users = s.Users.Where(w => !w.IsDeleted).Select(u => new ViewModel.ItemUser
                {
                    Id = u.Id,
                    Login = u.Login,
                    FullName = $"{u.LastName} {u.FirstName} {u.Patronymic}"
                }).ToList(),
                GroupParcels = _context.GroupParcels.Where(w => !w.IsDeleted && w.PlaceOfDeliveryId == id).Select(p => new ViewModel.ItemGroupParcel
                {
                    Id= p.Id,
                    TrackNumber = p.TrackNumber,
                    Status = p.Status.Name,
                    ParcelCount = p.Parcels.Count
                }).ToList(),
            }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Изменение данных места.
        /// </summary>
        /// <param name="id">id места</param>
        /// <returns>true - изменение данных прошло успешно</returns>
        public async Task<bool> EditPlace(EditModel editModel, int id, string login)
        {
            var place = _context.Places.Where(w => !w.IsDeleted && w.Id == id).FirstOrDefault();
            if (place != null)
            {
                editModel.UpdatePlace(place);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Начальные данные для изменения места.
        /// </summary>
        /// <param name="id">id места</param>
        /// <returns>EditModel</returns>
        public async Task<EditModel> ViewEditPlace(int id)
        {
            var place = _context.Places.Where(w => !w.IsDeleted && w.Id == id).Select(s => new EditModel
            {
                Name = s.Name
            }).FirstOrDefault();
            return place;
        }
    }
}