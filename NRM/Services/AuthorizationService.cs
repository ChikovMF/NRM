using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NRM.Models.DataModels;
using NRM.Models.UserModels;

namespace NRM.Services
{
    public class AuthorizationService
    {
        private AppDbContext _context { get; set; }

        public AuthorizationService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="loginModel">Данные пользователя для входа.</param>
        /// <returns>user, данные пользователя из бд.</returns>
        public async Task<User> Login(LoginModel loginModel)
        {
            var user = await _context.Users.Where(w => !w.IsDeleted && w.Login == loginModel.Login).Include(i => i.Role).FirstOrDefaultAsync();
            if(user != null && BCrypt.Net.BCrypt.Verify(loginModel.Password, user.PasswordHash))
            {
                return user;
            }
            else return null;
        }

        /// <summary>
        /// Добавление нового пользователя.
        /// </summary>
        /// <param name="createModel">Данные пользователя для его создания.</param>
        /// <returns>bool значение, true сохранение успешно завершено.</returns>
        public async Task<string> CreateUser(CreateModel createModel, string login)
        {
            var user = await _context.Users.AsNoTracking().Include(u => u.Place).FirstAsync(u => u.Login == login);

            string errorString = string.Empty;

            //Required(ErrorMessage = "Введите место работы"), 
            if (UserRepeatCheck(createModel.Login))
            {
                if (user.RoleId != 1)
                {
                    createModel.PlaceId = user.PlaceId;
                }
                else
                {
                    if (createModel.PlaceId == null || createModel.PlaceId == 0)
                    {
                        errorString = "Введите место работы";
                        return errorString;
                    }
                }

                await _context.Users.AddAsync(createModel.ToUser());
                int ch = await _context.SaveChangesAsync();

                if (ch == 0)
                {
                    errorString = "Ошибка создания пользователя";
                }
            }
            return errorString;
        }

        /// <summary>
        /// Получение Select с ролями пользователей
        /// </summary>
        /// <returns>Список SelectListItem</returns>
        public async Task<List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>> GetRolesSelect(int roleId = 1)
        {
            var SelectList = await _context.Roles.Where(w => !w.IsDeleted).Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToListAsync();
            /*
            var activItem = SelectList.Where(w => w.Value == roleId.ToString()).FirstOrDefault();
            if(activItem != null)
            {
                activItem.Selected = true;
            }
            */
            return SelectList;
        }

        /// <summary>
        /// Проверка на уникальность логина пользователя.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <returns>bool значение, true - логин уникален.</returns>
        private bool UserRepeatCheck(string login)
        {
            int count = _context.Users.Where(w => !w.IsDeleted && w.Login == login).Count();
            if (count == 0) return true;
            else return false;
        }

        /// <summary>
        /// Получение списка пользователей.
        /// </summary>
        /// <returns>Список<TableModel> с пользователями.</returns>
        public async Task<List<TableModel>> GetTableUsers()
        {
            return await _context.Users.Where(w => !w.IsDeleted).Select(s => new TableModel
            {
                Login = s.Login,
                FullName = $"{s.LastName} {s.FirstName} {s.Patronymic}",
                Id = s.Id,
            }).ToListAsync();
        }

        /// <summary>
        /// Получение данных пользователя.
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>Данные пользователя</returns>
        public async Task<ViewModel> ViewUser(int id)
        {
            return await _context.Users.Where(w => !w.IsDeleted && w.Id == id).Select(s => new ViewModel
            {
                Id = s.Id,
                Email = s.Email,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Patronymic = s.Patronymic,
                Login = s.Login,
                PhoneNumber = s.PhoneNumber,
                DeviceID = s.DeviceID,
                LoginAllowed = s.LoginAllowed,
                Role = new ViewModel.Item
                {
                    Id = s.Role.Id,
                    Name = s.Role.Name
                },
                Place = s.PlaceId == null ? null : new ViewModel.Item
                {
                    Id = s.Place.Id,
                    Name = s.Place.Name
                },
                GroupParcels = s.GroupParcels.Where(w => !w.IsDeleted).Select(p => new ViewModel.ItemGroupParcel
                {
                    Id = p.Id,
                    TrackNumber = p.TrackNumber,
                    Status = p.Status.Name,
                    ParcelCount = p.Parcels.Count
                }).ToList(),
            }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Изменение данных пользователя.
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>true - изменение данных прошло успешно</returns>
        public async Task<bool> EditUser(EditModel editModel, int id)
        {
            var user = await _context.Users.Where(w => !w.IsDeleted && w.Id == id).FirstOrDefaultAsync();
            if (user != null)
            {
                editModel.UpdateUser(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Начальные данные для изменения пользователя.
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>EditModel</returns>
        public async Task<EditModel> ViewEditUser(int id)
        {
            var user = await _context.Users.Where(w => !w.IsDeleted && w.Id == id).Select(s => new EditModel
            {
                Email = s.Email,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Patronymic = s.Patronymic,
                PhoneNumber = s.PhoneNumber,                
                StartPlace = s.PlaceId == null ? null : new EditModel.Item
                {
                    Id = s.Place.Id,
                    Name = s.Place.Name
                },                
                RoleItems = _context.Roles.Where(w => !w.IsDeleted).Select(ns => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = ns.Name,
                    Value = ns.Id.ToString(),                   

                }).ToList(),
                RoleId = s.RoleId,
                DeviceID = s.DeviceID,
                
                LoginAllowed = s.LoginAllowed
        }).FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> ChangePassword(int id, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(f => !f.IsDeleted && f.Id == id);
            if (user != null)
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
