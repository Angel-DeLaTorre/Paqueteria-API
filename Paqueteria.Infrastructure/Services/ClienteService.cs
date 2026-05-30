using Microsoft.EntityFrameworkCore;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class ClienteService(IClienteRepository clienteRepo, IUnitOfWorkBase unitOfWorkBase) : IClienteService
{
    public async Task<Result<IReadOnlyList<ClienteResponseDto>>> GetAllAsync(UserContext currentUser)
    {
        try
        {
            var clientes = ( await clienteRepo.GetAllAsync(currentUser.EmpresaId) )
                .Select( ClienteResponseDto.FromEntity ).ToList();
            return Result<IReadOnlyList<ClienteResponseDto>>.Success(clientes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<ClienteResponseDto>> GetByIdAsync(Guid clienteId)
    {
        try
        {
            var cliente = await clienteRepo.GetByIdAsync(clienteId);

            if (cliente == null)
                return Result<ClienteResponseDto>.Failure(Errors.Generic.NoEncontrado);

            return Result<ClienteResponseDto>.Success(ClienteResponseDto.FromEntity(cliente));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<ClienteResponseDto>> CreateAsync(ClienteCreateDto dto, UserContext currentUser)
    {
        try
        {
            var cliente = dto.ToClienteEntity(currentUser.EmpresaId);
            await clienteRepo.AddAsync(cliente);

            if (dto.DireccionC is not null)
            {
                // El factory ya debería setear el ClienteId internamente
                var nuevaDireccion = dto.ToDireccionEntity(cliente.Id);
                await clienteRepo.AddDireccion(nuevaDireccion);
            }

            var rowsAffected = await unitOfWorkBase.CompleteAsync();

            if (rowsAffected <= 0)
            {
                return Result<ClienteResponseDto>.Failure(Errors.Generic.NoCreado);
            }

            return Result<ClienteResponseDto>.Success(ClienteResponseDto.FromEntity(cliente));
        }
        catch (DbUpdateException ex)
        {
            return Result<ClienteResponseDto>.Failure( Errors.Generic.NoCreado );
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result> UpdateAsync(ClienteUpdateDto dto, UserContext currentUser)
    {
        try
        {
            var cliente = await clienteRepo.GetByIdAsync(dto.ClienteId);

            if (cliente is null)
                return Result.Failure(Errors.Generic.NoEncontrado);

            dto.UpdateEntity(cliente);
            clienteRepo.Update(cliente);
            var result = await  unitOfWorkBase.CompleteAsync();

            if (result != 0)
                return Result.Failure(Errors.Generic.NoActualizado);

            return Result.Success();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result> DeleteAsync(Guid clienteId, UserContext currentUser)
    {
        try
        {
            var cliente = (await clienteRepo.GetByIdAsync(clienteId));

            if (cliente is null)
                return Result.Failure(Errors.Generic.NoEncontrado);

            clienteRepo.Delete(cliente);

            var result = await unitOfWorkBase.CompleteAsync();
            if (result <= 0)
                return Result.Failure(Errors.Generic.NoEliminado);

            return Result.Success();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}