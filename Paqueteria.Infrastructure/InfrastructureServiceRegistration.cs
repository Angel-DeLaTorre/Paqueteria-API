using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Paqueteria.Application.Interfaces;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Settings;
using Paqueteria.Infrastructure.Data;
using Paqueteria.Infrastructure.Repositories;
using Paqueteria.Infrastructure.Services;

namespace Paqueteria.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Obtenemos la cadena de conexión del appsettings.json
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // 1. Mapear configuraciones de JWT desde Variables de Entorno o AppSettings
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString,
                // Configuramos el ensamblado de migraciones
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IClienteService, ClienteService>();

        return services;
    }
}