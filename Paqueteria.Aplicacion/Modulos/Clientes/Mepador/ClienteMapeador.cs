using System.Linq.Expressions;
using Paqueteria.Application.Comun.Mapeador;
using Paqueteria.Application.Modulos.Clientes.Dtos;
using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Clientes.Mepador;

public static class ClienteMapeador
{
    public static Expression<Func<Cliente, ClienteRespuestaDto>> ProyeccionRespuesta => entidad => new ClienteRespuestaDto(
        entidad.Id,
        entidad.Nombre,
        entidad.Rfc,
        entidad.Estatus,
        entidad.Telefono,
        entidad.Telefono2,
        entidad.Correo,
        entidad.Contacto,
        entidad.NumConvenio,
        entidad.PolizaSeguro,
        entidad.Direcciones.Select(d => d.ARespuestaDto())
    );

    public static ClienteRespuestaDto ARespuestaDto(this Cliente entidad)
    {
        return new ClienteRespuestaDto(
            entidad.Id,
            entidad.Nombre,
            entidad.Rfc,
            entidad.Estatus,
            entidad.Telefono,
            entidad.Telefono2,
            entidad.Correo,
            entidad.Contacto,
            entidad.NumConvenio,
            entidad.PolizaSeguro,
            entidad.Direcciones.Select(d => d.ARespuestaDto())
        );
    }

    private static ClienteDireccionRespuestaDto ARespuestaDto(this DireccionCliente entidad)
    {
        var direccion = entidad.Direccion.ARespuestaDto();
        return new ClienteDireccionRespuestaDto(
            entidad.Id,
            direccion,
            entidad.Estatus
        );      
    }
}