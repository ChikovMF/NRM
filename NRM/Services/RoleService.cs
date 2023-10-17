using Microsoft.EntityFrameworkCore;
using NRM.Models.RoleModels;

namespace NRM.Services
{
    public class RoleService
    {
        private AppDbContext _context {  get; set; }

        public RoleService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавление новой роли.
        /// </summary>
        /// <param name="createModel">Данные роли для её создания.</param>
        /// <returns>bool значение, true сохранение успешно завершено.</returns>
        public async Task<bool> CreateRole(CreateModel createModel)
        {
            if (RoleRepeatCheck(createModel.Name))
            {
                await _context.Roles.AddAsync(createModel.ToRole());
                _context.SaveChanges();
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Проверка на уникальность название роли.
        /// </summary>
        /// <param name="name">Название роли.</param>
        /// <returns>bool значение, true - логин уникален.</returns>
        private bool RoleRepeatCheck(string name)
        {
            int count = _context.Roles.Where(w => !w.IsDeleted && w.Name == name).Count();
            if (count == 0) return true;
            else return false;
        }

        /// <summary>
        /// Получение списка ролей пользователей.
        /// </summary>
        /// <returns>Список<TableModel> с ролями пользователей.</returns>
        public async Task<List<TableModel>> GetTableRoles()
        {
            return await _context.Roles.Where(w => !w.IsDeleted).Select(s => new TableModel
            {
                Name = s.Name,
                NumberOfUsers = s.Users.Count(),
                Id = s.Id,
            }).ToListAsync();
        }

        /// <summary>
        /// Получение данных по роли.
        /// </summary>
        /// <param name="id">id роли</param>
        /// <returns>Данные по роли</returns>
        public async Task<ViewModel> ViewRole(int id)
        {
            return await _context.Roles.Where(w => !w.IsDeleted && w.Id == id).Select(s => new ViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Users = s.Users.Where(w => !w.IsDeleted).Select(s => new ViewModel.UserItem
                {
                    Id = s.Id,
                    Login = s.Login,
                    FullName = $"{s.LastName} {s.FirstName} {s.Patronymic}",
                }).ToList()
            }).FirstOrDefaultAsync();
        }
    }
}
