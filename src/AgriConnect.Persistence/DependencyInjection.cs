using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AgriConnect.Persistence.Data;

namespace AgriConnect.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            options =>
            options.UseNpgsql(
                configuration.GetConnectionString(
                    "DefaultConnection")));

        return services;
    }
}