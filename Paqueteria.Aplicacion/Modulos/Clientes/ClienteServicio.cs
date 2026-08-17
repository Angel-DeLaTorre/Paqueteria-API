using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;

namespace Paqueteria.Application.Modulos.Clientes;

public class ClienteServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : IClienteServicio
{
    public async Task<Resultado<ClienteResponseDto>> ObtenerPorIdAsync(Guid clienteId)
    {
        var cliente = await unit.Clientes.ObtenerPorIdAsync(clienteId, contextoUsuario.EmpresaId, false );

        return cliente == null ? 
            Resultado<ClienteResponseDto>.Error(CodigosError.Generic.NoEncontrado) 
            : 
            Resultado<ClienteResponseDto>.Exitoso(ClienteResponseDto.FromEntity(cliente));
    }
    
    public async Task<Resultado<IReadOnlyList<ClienteResponseDto>>> ObtenerTodosAsync()
    {
        var clientes = ( await unit.Clientes.ObtenerTodosAsync(contextoUsuario.EmpresaId) )
            .Select( ClienteResponseDto.FromEntity ).ToList();
        return Resultado<IReadOnlyList<ClienteResponseDto>>.Exitoso(clientes);
    }

    public async Task<Resultado<ClienteResponseDto>> AgregarAsync(ClienteCreateDto dto)
    {
        var cliente = dto.ToClienteEntity(contextoUsuario.EmpresaId);
        await unit.Clientes.AgregarAsync(cliente);

        if (dto.DireccionC is not null)
        {
            var nuevaDireccion = dto.ToDireccionEntity(cliente.Id);
            await unit.Clientes.AgregarDireccionAsync(nuevaDireccion);
        }
        await unit.CompletarAsync();

        return Resultado<ClienteResponseDto>.Exitoso(ClienteResponseDto.FromEntity(cliente));
    }

    public async Task<Resultado> ActualizarAsync(ClienteUpdateDto dto)
    {
        var cliente = await unit.Clientes.ObtenerPorIdAsync(dto.ClienteId, contextoUsuario.EmpresaId);

        if (cliente is null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);

        dto.UpdateEntity(cliente);
        await  unit.CompletarAsync();

        return Resultado.Exitoso();
    }
    
    public async Task<Resultado> DesactivarAsync(Guid clienteId)
    {
        var cliente = await unit.Clientes.ObtenerPorIdAsync(clienteId, contextoUsuario.EmpresaId);
        if (cliente == null) return Resultado.Error(CodigosError.Generic.NoEncontrado);
        
        cliente.Desactivar();
        await unit.CompletarAsync();
        
        return Resultado.Exitoso();
    }
    
    public async Task<Resultado> ActivarAsync(Guid clienteId)
    {
        var cliente = await unit.Clientes.ObtenerPorIdAsync(clienteId, contextoUsuario.EmpresaId);
        if (cliente == null) return Resultado.Error(CodigosError.Generic.NoEncontrado);
        
        cliente.Activar();
        await unit.CompletarAsync();
        
        return Resultado.Exitoso();
    }

    public async Task<Resultado> EliminarAsync(Guid clienteId)
    {
        var cliente = (await unit.Clientes.ObtenerPorIdAsync(clienteId, contextoUsuario.EmpresaId));

        if (cliente is null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);
        unit.Clientes.Eliminar(cliente);
        
        await unit.CompletarAsync();
        
        return Resultado.Exitoso();
    }
    
    public async Task<Resultado> ActivarDireccionAsync(Guid direccionId, Guid clienteId)
    {
        var cliente = await unit.Clientes.ObtenerPorIdAsync(clienteId, contextoUsuario.EmpresaId);
        if (cliente == null) return Resultado.Error(CodigosError.Generic.NoEncontrado);

        var direccion = await unit.Clientes.ObtenerDireccionPorIdAsync(clienteId, direccionId);
        if (direccion == null) return Resultado.Error(CodigosError.Generic.NoEncontrado);

        direccion.Activar();
        
        return Resultado.Exitoso();
    }
    
    public async Task<Resultado> DesactivarDireccionAsync(Guid direccionId, Guid clienteId)
    {
        var cliente = await unit.Clientes.ObtenerPorIdAsync(clienteId, contextoUsuario.EmpresaId);
        if (cliente == null) return Resultado.Error(CodigosError.Generic.NoEncontrado);

        var direccion = await unit.Clientes.ObtenerDireccionPorIdAsync(clienteId, direccionId);
        if (direccion == null) return Resultado.Error(CodigosError.Generic.NoEncontrado);

        direccion.Desactivar();
        
        return Resultado.Exitoso();
    }
}