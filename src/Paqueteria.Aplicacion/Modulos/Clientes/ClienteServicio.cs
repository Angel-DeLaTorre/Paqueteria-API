using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Comun.Mapeador;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Clientes.Dtos;
using Paqueteria.Dominio.Comun;
using Paqueteria.Dominio.Comun.Errors;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Clientes;

public class ClienteServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : IClienteServicio
{
    public async Task<Respuesta<ClienteRespuestaDto>> ObtenerPorIdAsync(Guid clienteId)
    {
        var cliente = await unit.Clientes.ObtenerPorIdAsync(clienteId, contextoUsuario.EmpresaId, false );

        return cliente == null ? 
            Respuesta<ClienteRespuestaDto>.Error(CodigosError.Comun.NoEncontrado) 
            : Respuesta<ClienteRespuestaDto>.Exitoso( cliente.MapeaRespuestaDto() );
    }
    
    public async Task<Respuesta<IReadOnlyList<ClienteRespuestaDto>>> ObtenerTodosAsync()
    {
        var clientes = ( await unit.Clientes.ObtenerTodosAsync(contextoUsuario.EmpresaId) )
            .Select( c => c.MapeaRespuestaDto() ).ToList();
        return Respuesta<IReadOnlyList<ClienteRespuestaDto>>.Exitoso(clientes);
    }

    public async Task<Respuesta<ClienteRespuestaDto>> AgregarAsync(ClienteCrearDto dto)
    {
        var cliente = Cliente.Crear
        (
            dto.Nombre, 
            dto.Rfc, 
            dto.TipoPersona,
            dto.Telefono, 
            dto.Telefono2, 
            dto.Correo, 
            dto.Contacto,
            dto.NumConvenio,
            dto.PolizaSeguro,
            contextoUsuario.EmpresaId 
        );
        
        if (dto.DireccionC is not null)
        {
            var direccion = dto.DireccionC.MapeaEntidad();
            var clienteDireccion = DireccionCliente.Crear(direccion, cliente.Id);
            cliente.AgregarDireccion(clienteDireccion);    
        }
        
        await unit.Clientes.AgregarAsync(cliente);
        
        var respuesta = await unit.GuardarCambiosAsync();
        return respuesta > 0 ? 
            Respuesta<ClienteRespuestaDto>.Exitoso( cliente.MapeaRespuestaDto() )
            : Respuesta<ClienteRespuestaDto>.Error(CodigosError.Comun.NoCreado);
    }

    public async Task<Respuesta> ActualizarAsync(ClienteActualizarDto dto)
    {
        var cliente = await unit.Clientes.ObtenerPorIdAsync(dto.ClienteId, contextoUsuario.EmpresaId);

        if (cliente is null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        cliente.ActualizarDatos
        (
            dto.Nombre,
            dto.Rfc,
            dto.Telefono,
            dto.Telefono2,
            dto.Correo,
            dto.Contacto,
            dto.NumConvenio,
            dto.PolizaSeguro
        );
        
        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? 
            Respuesta.Exitoso() 
            : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }
    
    public async Task<Respuesta> ActivarAsync(Guid clienteId)
    {
        var cliente = await unit.Clientes.ObtenerPorIdAsync(clienteId, contextoUsuario.EmpresaId);
        if (cliente == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);
        
        cliente.Activar();
        
        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? 
            Respuesta.Exitoso() 
            : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }
    
    public async Task<Respuesta> DesactivarAsync(Guid clienteId)
    {
        var cliente = await unit.Clientes.ObtenerPorIdAsync(clienteId, contextoUsuario.EmpresaId);
        if (cliente == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);
        
        cliente.Desactivar();
        
        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? 
            Respuesta.Exitoso() 
            : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }

    public async Task<Respuesta> EliminarAsync(Guid clienteId)
    {
        var cliente = (await unit.Clientes.ObtenerPorIdAsync(clienteId, contextoUsuario.EmpresaId));

        if (cliente is null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);
        
        unit.Clientes.Eliminar(cliente);
        
        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? 
            Respuesta.Exitoso() 
            : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }
    
    public async Task<Respuesta> ActivarDireccionAsync(Guid direccionId, Guid clienteId)
    {
        var cliente = await unit.Clientes.ObtenerPorIdAsync(clienteId, contextoUsuario.EmpresaId);
        if (cliente == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        var direccion = await unit.Clientes.ObtenerDireccionPorIdAsync(clienteId, direccionId);
        if (direccion == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        direccion.Activar();
        
        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? 
            Respuesta.Exitoso() 
            : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }
    
    public async Task<Respuesta> DesactivarDireccionAsync(Guid direccionId, Guid clienteId)
    {
        var cliente = await unit.Clientes.ObtenerPorIdAsync(clienteId, contextoUsuario.EmpresaId);
        if (cliente == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        var direccion = await unit.Clientes.ObtenerDireccionPorIdAsync(clienteId, direccionId);
        if (direccion == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        direccion.Desactivar();
        
        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? 
            Respuesta.Exitoso() 
            : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }
}