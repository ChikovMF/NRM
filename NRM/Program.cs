using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using NRM.Services;
using NRM.Services.Queries;
using System.Globalization;
using System.Net;
using System.Security.Claims;

namespace NRM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



        builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.Limits.MaxRequestBodySize = long.MaxValue;
            });

            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/AdminPanel");
                options.Conventions.AllowAnonymousToPage("/AdminPanel/Login");

                options.Conventions.AuthorizeFolder("/AdminPanel/Place", "Только администратор");

                options.Conventions.AuthorizeFolder("/AdminPanel/Role", "Управление пользователями");
                options.Conventions.AuthorizeFolder("/AdminPanel/User", "Управление пользователями");

                options.Conventions.AuthorizeFolder("/AdminPanel/ParcelStatus", "Управление посылками");
                options.Conventions.AuthorizeFolder("/AdminPanel/ParcelType", "Управление посылками");
                options.Conventions.AuthorizeFolder("/AdminPanel/LogParcel", "Управление посылками");
                options.Conventions.AuthorizeFolder("/AdminPanel/LogType", "Управление посылками");

                options.Conventions.AuthorizeFolder("/AdminPanel/Parcel", "Пользователь");
                options.Conventions.AuthorizeFolder("/AdminPanel/GroupParcel", "Пользователь");
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Только администратор", policyBuilder
                    => policyBuilder.RequireRole(Claims.FullAccessEnter));
                options.AddPolicy("Управление пользователями", policyBuilder
                    => policyBuilder.RequireRole(Claims.UserAdministratorEnter, Claims.FullAccessEnter));
                options.AddPolicy("Управление посылками", policyBuilder
                    => policyBuilder.RequireRole(Claims.ParcelAdministratorEnter, Claims.FullAccessEnter));
                options.AddPolicy("Пользователь", policyBuilder
                    => policyBuilder.RequireRole(Claims.UserEnter, Claims.ParcelAdministratorEnter, Claims.FullAccessEnter));
            });
            builder.Services.AddControllers();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                    options.ExpireTimeSpan = TimeSpan.FromDays(7);
                    options.LoginPath = "/AdminPanel/Login";
                    options.Cookie.Name = "nrm_auth_cookie";
                    options.AccessDeniedPath = "/AccessDenied";
                });

            var connString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
                throw new InvalidOperationException("Не удалось подключиться к базе данных.");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connString)
            );

            builder.Services.AddScoped<AuthorizationService>();
            builder.Services.AddScoped<RoleService>();
            builder.Services.AddScoped<ParcelService>();
            builder.Services.AddScoped<GroupParcelService>();
            builder.Services.AddScoped<PlaceService>();
            builder.Services.AddScoped<ParcelStatusService>();
            builder.Services.AddScoped<ParcelTypeService>();
            builder.Services.AddScoped<LogTypeService>();
            builder.Services.AddScoped<LogParcelService>();
            builder.Services.AddScoped<Select2Service>();
            builder.Services.AddScoped<ChartService>();
            builder.Services.AddScoped<ExcelService>();
            builder.Services.AddTransient<ParcelFilterDropdownService>();

            builder.Services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue; // максимальный размер загружаемых файлов в байтах 2гб
            });

            var app = builder.Build();

            /*
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            else app.UseDeveloperExceptionPage();
            */
            app.UseDeveloperExceptionPage();
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthorization();

            // Установка русской культуры даты и времени.
            var ci = new CultureInfo("ru-RU");
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(ci),
                SupportedCultures = new List<CultureInfo>
                    {
                        ci,
                    },
                SupportedUICultures = new List<CultureInfo>
                    {
                        ci,
                    }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });



            app.Run();
        }
    }
}