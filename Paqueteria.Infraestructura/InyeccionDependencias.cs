using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Services.Reportes;
using Paqueteria.Application.Modulos.Sesion.Configuracion;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Audit;
using Paqueteria.Core.Interfaces.Reportes;
using Paqueteria.Core.Interfaces.Repositories;
using Paqueteria.Infrastructure.Data;
using Paqueteria.Infrastructure.Persistence;
using Paqueteria.Infrastructure.Reportes;
using Paqueteria.Infrastructure.Reportes.Estrategias;
using Paqueteria.Infrastructure.Repositories;
using Paqueteria.Infrastructure.Seguridad;
using Paqueteria.Infrastructure.Services;

namespace Paqueteria.Infrastructure;

public static class InyeccionDependencias
{
    public static IServiceCollection AgregarInfraestructura(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no fue encontrada.");
        }


        // 1. Mapear configuraciones de JWT desde Variables de Entorno o AppSettings
        services.Configure<JwtOpciones>(configuration.GetSection(JwtOpciones.SectionName));

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString,
                // Configuramos el ensamblado de migraciones
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
        services.AddScoped<IUnitOfWorkBase, UnitOfWorkBaseBase>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IArticuloRepository, ArticuloRepository>();
        services.AddScoped<IAsignacionRepository, AsignacionRepository>();
        services.AddScoped<IBitacoraAccesoRepository, BitacoraAccesoRepository>();
        services.AddScoped<IBitacoraSistemaRepository, BitacoraSistemaRepository>();
        services.AddScoped<IChoferRepository, ChoferRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IFolioSucursalRepository, FolioSucursalRepository>();
        services.AddScoped<IEmpresaRepository, EmpresaRepository>();
        services.AddScoped<IEstadoRepository, EstadoRepository>();
        services.AddScoped<IGuiaRepository, GuiaRepository>();
        services.AddScoped<IGuiaTransbordoRepositorio, GuiaTransbordoRepositorio>();
        services.AddScoped<IMunicipioRepository, MunicipioRepository>();
        services.AddScoped<IRutaRepository, RutaRepository>();
        services.AddScoped<ISeguroRepository, SeguroRepository>();
        services.AddScoped<ISucursalRepository, SucursalRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IRolRepository, RolRepository>();
        services.AddScoped<IPermisoRepository, PermisoRepository>();
        services.AddScoped<IAuditLogService, FakeAuditLogService>();
        services.AddScoped<IHashServicio, BCryptHashServicio>();
        
        // Reporteria
        services.AddScoped<IFabricaPdf, FabricaPdf>();
        
        //Estrategias
        services.AddScoped<IPdfEstrategia, GuiaPdfEstrategia>();
        services.AddScoped<IPdfEstrategia, ReporteSalidaOperador>();
        
        return services;
    }
}