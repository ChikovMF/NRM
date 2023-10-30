using Microsoft.EntityFrameworkCore; // UseNpgsql
using Microsoft.Extensions.DependencyInjection; // IServiceCollection


namespace NRM.DataContext;

public static class NRMContextExtensions
{
    public static IServiceCollection AddNRMContext(
        this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<NRMContext>(options =>
            options.UseNpgsql(connectionString)
        );

        return services;
    }
}