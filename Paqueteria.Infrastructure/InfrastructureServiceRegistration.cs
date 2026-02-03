using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Obtenemos la cadena de conexión del appsettings.json
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString,
                // Configuramos el ensamblado de migraciones
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        return services;
    }
}