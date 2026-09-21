using Microsoft.Extensions.DependencyInjection;
using Paqueteria.Aplicacion.Modulos.Articulos;
using Paqueteria.Aplicacion.Modulos.Asignaciones;
using Paqueteria.Aplicacion.Modulos.Choferes;
using Paqueteria.Aplicacion.Modulos.Clientes;
using Paqueteria.Aplicacion.Modulos.Empresas;
using Paqueteria.Aplicacion.Modulos.Estados;
using Paqueteria.Aplicacion.Modulos.Folios;
using Paqueteria.Aplicacion.Modulos.Guias.Interfaces;
using Paqueteria.Aplicacion.Modulos.Guias.Servicios;
using Paqueteria.Aplicacion.Modulos.GuiasTransbordo.Interfaces;
using Paqueteria.Aplicacion.Modulos.GuiasTransbordo.Servicios;
using Paqueteria.Aplicacion.Modulos.Municipios;
using Paqueteria.Aplicacion.Modulos.Permisos;
using Paqueteria.Aplicacion.Modulos.Roles;
using Paqueteria.Aplicacion.Modulos.Rutas;
using Paqueteria.Aplicacion.Modulos.Seguros;
using Paqueteria.Aplicacion.Modulos.Sesion;
using Paqueteria.Aplicacion.Modulos.Sucursales;
using Paqueteria.Aplicacion.Modulos.Usuarios;

namespace Paqueteria.Aplicacion;

public static class InyeccionDependencias
{
    public static IServiceCollection AgregarAplicacion(this IServiceCollection services)
    {
        services.AddScoped<IArticuloServicio, ArticuloServicio>();
        services.AddScoped<IAsignacionServicio, AsignacionServicio>();
        services.AddScoped<IChoferServicio, ChoferServicio>();
        services.AddScoped<IClienteServicio, ClienteServicio>();
        services.AddScoped<IEmpresaServicio, EmpresaServicio>();
        services.AddScoped<IEstadoServicio, EstadoServicio>();
        services.AddScoped<IFolioServicio, FolioServicio>();
        services.AddScoped<IGuiaServicio, GuiaServicio>();
        services.AddScoped<IGuiaTransbordoServicio, GuiaTransbordoServicio>();
        services.AddScoped<IMunicipioServicio, MunicipioServicio>();
        services.AddScoped<IPermisoServicio, PermisoServicio>();
        services.AddScoped<IRolServicio, RolServicio>();
        services.AddScoped<IRutaServicio, RutaServicio>();
        services.AddScoped<ISeguroServicio, SeguroServicio>();
        services.AddScoped<ISesionServicio, SesionServicio>();
        services.AddScoped<ISucursalServicio, SucursalServicio>();
        services.AddScoped<IUsuarioServicio, UsuarioServicio>();
        
        services.AddScoped<IGuiaPdfServicio, GuiaPdfServicio>();

        return services;
    }
}