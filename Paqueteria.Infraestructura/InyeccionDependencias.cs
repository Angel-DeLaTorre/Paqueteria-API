using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Sesion.Configuracion;
using Paqueteria.Core.Interfaces.Repositorios;
using Paqueteria.Infrastructure.Persistencia;
using Paqueteria.Infrastructure.Persistencia.Repositorios;
using Paqueteria.Infrastructure.Reportes;
using Paqueteria.Infrastructure.Reportes.Estrategias;
using Paqueteria.Infrastructure.Seguridad;

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

        services.AddScoped(typeof(IEntityRepositorio<>), typeof(EntityRepositorio<>));
        services.AddScoped<IUnitOfWork, UnidadDeTrabajo>();
        services.AddScoped<IArticuloRepositorio, ArticuloRepositorio>();
        services.AddScoped<IAsignacionRepositorio, AsignacionRepositorio>();
        services.AddScoped<IBitacoraAccesoRepositorio, BitacoraAccesoRepositorio>();
        services.AddScoped<IBitacoraSistemaRepositorio, BitacoraSistemaRepositorio>();
        services.AddScoped<IChoferRepositorio, ChoferRepositorio>();
        services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
        services.AddScoped<IFolioSucursalRepositorio, FolioSucursalRepositorio>();
        services.AddScoped<IEmpresaRepositorio, EmpresaRepositorio>();
        services.AddScoped<IEstadoRepositorio, EstadoRepositorio>();
        services.AddScoped<IGuiaRepositorio, GuiaRepositorio>();
        services.AddScoped<IGuiaTransbordoRepositorio, GuiaTransbordoRepositorio>();
        services.AddScoped<IMunicipioRepositorio, MunicipioRepositorio>();
        services.AddScoped<IRutaRepositorio, RutaRepositorio>();
        services.AddScoped<ISeguroRepositorio, SeguroRepositorio>();
        services.AddScoped<ISucursalRepositorio, SucursalRepositorio>();
        services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
        services.AddScoped<IRolRepositorio, RolRepositorio>();
        services.AddScoped<IPermisoRepositorio, PermisoRepositorio>();
        services.AddScoped<IHashServicio, BCryptHashServicio>();
        
        // Reporteria
        services.AddScoped<IFabricaPdf, FabricaPdf>();
        
        //Estrategias
        services.AddScoped<IPdfEstrategia, GuiaPdfEstrategia>();
        services.AddScoped<IPdfEstrategia, ReporteSalidaOperador>();
        services.AddScoped<IPdfEstrategia, RemisionPdfEstrategia>();
        
        return services;
    }
}