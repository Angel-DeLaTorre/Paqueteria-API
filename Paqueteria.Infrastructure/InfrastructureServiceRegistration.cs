using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Settings;
using Paqueteria.Infrastructure.Data;
using Paqueteria.Infrastructure.Persistence;
using Paqueteria.Infrastructure.Repositories;
using Paqueteria.Infrastructure.Services;

namespace Paqueteria.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no fue encontrada.");
        }


        // 1. Mapear configuraciones de JWT desde Variables de Entorno o AppSettings
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString,
                // Configuramos el ensamblado de migraciones
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
        services.AddScoped<IUnitOfWorkBase, UnitOfWorkBaseBase>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IArticuloRepository, ArticuloRepository>();
        services.AddScoped<IArticuloService, ArticuloService>();

        services.AddScoped<IAsignacionRepository, AsignacionRepository>();
        services.AddScoped<IAsignacionSerivce, AsignacionService>();

        services.AddScoped<IBitacoraAccesoRepository, BitacoraAccesoRepository>();
        services.AddScoped<IBitacoraSistemaRepository, BitacoraSistemaRepository>();

        services.AddScoped<IChoferRepository, ChoferRepository>();
        services.AddScoped<IChoferService, ChoferService>();

        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IClienteService, ClienteService>();
        

        services.AddScoped<IEmpresaRepository, EmpresaRepository>();
        services.AddScoped<IEmpresaService, EmpresaService>();

        services.AddScoped<IEstadoRepository, EstadoRepository>();
        services.AddScoped<IEstadoService, EstadoService>();

        services.AddScoped<IGuiaRepository, GuiaRepository>();
        services.AddScoped<IGuiaService, GuiaService>();

        services.AddScoped<IMunicipioRepository, MunicipioRepository>();
        services.AddScoped<IMunicipioService, MunicipioService>();

        services.AddScoped<IRutaRepository, RutaRepository>();
        services.AddScoped<IRutaService, RutaService>();

        services.AddScoped<ISeguroRepository, SeguroRepository>();
        services.AddScoped<ISeguroService, SeguroService>();

        services.AddScoped<ISucursalRepository, SucursalRepository>();
        services.AddScoped<ISucursalService, SucursalService>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        
        services.AddScoped<IRolRepository, RolRepository>();
        services.AddScoped<IRolService, RolService>();
        
        services.AddScoped<IPermisoRepository, PermisoRepository>();
        services.AddScoped<IPermisoService, PermisoService>();

        return services;
    }
}