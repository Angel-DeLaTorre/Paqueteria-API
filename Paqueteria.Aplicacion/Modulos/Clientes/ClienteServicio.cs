using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Dtos;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Clientes.Dtos;
using Paqueteria.Core.Comun;
using Paqueteria.Core.Comun.Errors;

namespace Paqueteria.Application.Modulos.Clientes;

public class ClienteServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : IClienteServicio
{
    public async Task<Respuesta<ClienteResponseDto>> ObtenerPorIdAsync(Guid clienteId)
    {
        var cliente = await unit.Clientes.ObtenerPorIdAsync(clienteId, contextoUsuario.EmpresaId, false );

        return cliente == null ? 
            Respuesta<ClienteResponseDto>.Error(CodigosError.Generic.NoEncontrado) 
            : 
            Respuesta<ClienteResponseDto>.Exitoso(ClienteResponseDto.FromEntity(cliente));
    }
    
    public async Task<Respuesta<IReadOnlyList<ClienteResponseDto>>> ObtenerTodosAsync()
    {
        var clientes = ( await unit.Clientes.ObtenerTodosAsync(contextoUsuario.EmpresaId) )
            .Select( ClienteResponseDto.FromEntity ).ToList();
        return Respuesta<IReadOnlyList<ClienteResponseDto>>.Exitoso(clientes);
    }

    public async Task<Respuesta<ClienteResponseDto>> AgregarAsync(ClienteCreateDto dto)
    {
        var cliente = dto.ToClienteEntity(contextoUsuario.EmpresaId);
        await unit.Clientes.AgregarAsync(cliente);

        if (dto.DireccionC is not null)
        {
            var nuevaDireccion = dto.ToDireccionEntity(cliente.Id);
            await unit.Clientes.AgregarDireccionAsync(nuevaDireccion);
        }
        await unit.GuardarCambiosAsync();

        return Respuesta<ClienteResponseDto>.Exitoso(ClienteResponseDto.FromEntity(cliente));
    }

    public async Task<Respuesta> ActualizarAsync(ClienteUpdateDto dto)
    {
        var cliente = await unit.Clientes.ObtenerPorIdAsync(dto.ClienteId, contextoUsuario.EmpresaId);

        if (cliente is null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        dto.UpdateEntity(cliente);
        await  unit.GuardarCambiosAsync();

        return Respuesta.Exitoso();
    }
    
    public async Task<Respuesta> DesactivarAsync(Guid clienteId)
    {
        var cliente = await unit.Clientes.ObtenerPorIdAsync(clienteId, contextoUsuario.EmpresaId);
        if (cliente == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);
        
        cliente.Desactivar();
        await unit.GuardarCambiosAsync();
        
        return Respuesta.Exitoso();
    }
    
    public async Task<Respuesta> ActivarAsync(Guid clienteId)
    {
        var cliente = await unit.Clientes.ObtenerPorIdAsync(clienteId, contextoUsuario.EmpresaId);
        if (cliente == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);
        
        cliente.Activar();
        await unit.GuardarCambiosAsync();
        
        return Respuesta.Exitoso();
    }

    public async Task<Respuesta> EliminarAsync(Guid clienteId)
    {
        var cliente = (await unit.Clientes.ObtenerPorIdAsync(clienteId, contextoUsuario.EmpresaId));

        if (cliente is null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);
        unit.Clientes.Eliminar(cliente);
        
        await unit.GuardarCambiosAsync();
        
        return Respuesta.Exitoso();
    }
    
    public async Task<Respuesta> ActivarDireccionAsync(Guid direccionId, Guid clienteId)
    {
        var cliente = await unit.Clientes.ObtenerPorIdAsync(clienteId, contextoUsuario.EmpresaId);
        if (cliente == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        var direccion = await unit.Clientes.ObtenerDireccionPorIdAsync(clienteId, direccionId);
        if (direccion == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        direccion.Activar();
        
        return Respuesta.Exitoso();
    }
    
    public async Task<Respuesta> DesactivarDireccionAsync(Guid direccionId, Guid clienteId)
    {
        var cliente = await unit.Clientes.ObtenerPorIdAsync(clienteId, contextoUsuario.EmpresaId);
        if (cliente == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        var direccion = await unit.Clientes.ObtenerDireccionPorIdAsync(clienteId, direccionId);
        if (direccion == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        direccion.Desactivar();
        
        return Respuesta.Exitoso();
    }
}