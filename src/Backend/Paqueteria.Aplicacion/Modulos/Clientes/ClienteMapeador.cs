using System.Linq.Expressions;
using Paqueteria.Aplicacion.Comun.Mapeador;
using Paqueteria.Comun.Dtos.Clientes;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Aplicacion.Modulos.Clientes;

public static class ClienteMapeador
{
    extension(Cliente cliente)
    {
        public ClienteRespuestaDto MapeaRespuestaDto()
        {
            return new ClienteRespuestaDto(
                cliente.Id,
                cliente.Nombre,
                cliente.Rfc,
                cliente.Estatus,
                cliente.Telefono,
                cliente.Telefono2,
                cliente.Correo,
                cliente.Contacto,
                cliente.NumConvenio,
                cliente.PolizaSeguro,
                cliente.Direcciones.Select(d => d.MapeaRespuestaDto())
            );
        }
    }

    extension(DireccionCliente direccionCliente)
    {
        public ClienteDireccionRespuestaDto MapeaRespuestaDto()
        {
            var direccionRespuesta = direccionCliente.Direccion.MapeaRespuestaDto();
            
            return new ClienteDireccionRespuestaDto(
                direccionCliente.Id,
                direccionRespuesta,
                direccionCliente.Estatus
            );    
        }
    }
    
    
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
        entidad.Direcciones.Select(d => d.MapeaRespuestaDto())
    );
}