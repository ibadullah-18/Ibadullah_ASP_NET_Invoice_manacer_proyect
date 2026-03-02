using Ibadullah_ASP_NET_Invoice_manacer_proyect.Data;
using Microsoft.EntityFrameworkCore;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Extensions;

public static class DatabaseServiceExtension
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnectionString")));

        return services;
    }
}