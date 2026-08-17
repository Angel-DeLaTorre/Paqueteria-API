using Microsoft.Extensions.DependencyInjection;
using Paqueteria.Application.Modulos.Articulos;
using Paqueteria.Application.Modulos.Asignaciones;
using Paqueteria.Application.Modulos.Choferes;
using Paqueteria.Application.Modulos.Clientes;
using Paqueteria.Application.Modulos.Empresas;
using Paqueteria.Application.Modulos.Estados;
using Paqueteria.Application.Modulos.Guias;
using Paqueteria.Application.Modulos.Guias.Interfaces;
using Paqueteria.Application.Modulos.Guias.Servicios;
using Paqueteria.Application.Modulos.GuiasTransbordo.Interfaces;
using Paqueteria.Application.Modulos.GuiasTransbordo.Mapeador;
using Paqueteria.Application.Modulos.GuiasTransbordo.Servicios;
using Paqueteria.Application.Modulos.Municipios;
using Paqueteria.Application.Modulos.Permisos;
using Paqueteria.Application.Modulos.Roles;
using Paqueteria.Application.Modulos.Rutas;
using Paqueteria.Application.Modulos.Seguros;
using Paqueteria.Application.Modulos.Sesion;
using Paqueteria.Application.Modulos.Sucursales;
using Paqueteria.Application.Modulos.Usuarios;

namespace Paqueteria.Application;

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
        
        services.AddScoped<IGuiaTransbordoMapeador, GuiaTransbordoMapeador>();
        
        services.AddScoped<IGuiaPdfServicio, GuiaPdfServicio>();

        return services;
    }
}