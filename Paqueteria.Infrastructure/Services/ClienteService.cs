using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class ClienteService(IUnitOfWork unit, IUserContextService userContext) : IClienteService
{
    public async Task<Result<ClienteResponseDto>> GetByIdAsync(Guid clienteId)
    {
        var cliente = await unit.Clientes.GetByIdAsync(clienteId, userContext.EmpresaId, false );

        return cliente == null ? 
            Result<ClienteResponseDto>.Failure(ErrorCodes.Generic.NoEncontrado) 
            : 
            Result<ClienteResponseDto>.Success(ClienteResponseDto.FromEntity(cliente));
    }
    
    public async Task<Result<IReadOnlyList<ClienteResponseDto>>> GetAllAsync()
    {
        var clientes = ( await unit.Clientes.GetAllAsync(userContext.EmpresaId) )
            .Select( ClienteResponseDto.FromEntity ).ToList();
        return Result<IReadOnlyList<ClienteResponseDto>>.Success(clientes);
    }

    public async Task<Result<ClienteResponseDto>> CreateAsync(ClienteCreateDto dto)
    {
        var cliente = dto.ToClienteEntity(userContext.EmpresaId);
        await unit.Clientes.AddAsync(cliente);

        if (dto.DireccionC is not null)
        {
            var nuevaDireccion = dto.ToDireccionEntity(cliente.Id);
            await unit.Clientes.AddDireccion(nuevaDireccion);
        }
        await unit.CompleteAsync();

        return Result<ClienteResponseDto>.Success(ClienteResponseDto.FromEntity(cliente));
    }

    public async Task<Result> UpdateAsync(ClienteUpdateDto dto)
    {
        var cliente = await unit.Clientes.GetByIdAsync(dto.ClienteId, userContext.EmpresaId);

        if (cliente is null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        dto.UpdateEntity(cliente);
        await  unit.CompleteAsync();

        return Result.Success();
    }
    
    public async Task<Result> DesactivarAsync(Guid clienteId)
    {
        var cliente = await unit.Clientes.GetByIdAsync(clienteId, userContext.EmpresaId);
        if (cliente == null) return Result.Failure(ErrorCodes.Generic.NoEncontrado);
        
        cliente.Estatus = EstatusBasico.Inactivo;
        await unit.CompleteAsync();
        
        return Result.Success();
    }
    
    public async Task<Result> ActivarAsync(Guid clienteId)
    {
        var cliente = await unit.Clientes.GetByIdAsync(clienteId, userContext.EmpresaId);
        if (cliente == null) return Result.Failure(ErrorCodes.Generic.NoEncontrado);
        
        cliente.Estatus = EstatusBasico.Activo;
        await unit.CompleteAsync();
        
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid clienteId)
    {
        var cliente = (await unit.Clientes.GetByIdAsync(clienteId, userContext.EmpresaId));

        if (cliente is null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);
        unit.Clientes.Delete(cliente);
        
        await unit.CompleteAsync();
        
        return Result.Success();
    }
    
    public async Task<Result> ActivarDireccionAsync(Guid direccionId, Guid clienteId)
    {
        var cliente = await unit.Clientes.GetByIdAsync(clienteId, userContext.EmpresaId);
        if (cliente == null) return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        var direccion = await unit.Clientes.GetDireccionByIdAsync(clienteId, direccionId);
        if (direccion == null) return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        direccion.Estatus = EstatusBasico.Activo;
        
        return Result.Success();
    }
    
    public async Task<Result> DesactivarDireccionAsync(Guid direccionId, Guid clienteId)
    {
        var cliente = await unit.Clientes.GetByIdAsync(clienteId, userContext.EmpresaId);
        if (cliente == null) return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        var direccion = await unit.Clientes.GetDireccionByIdAsync(clienteId, direccionId);
        if (direccion == null) return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        direccion.Estatus = EstatusBasico.Inactivo;
        
        return Result.Success();
    }
}