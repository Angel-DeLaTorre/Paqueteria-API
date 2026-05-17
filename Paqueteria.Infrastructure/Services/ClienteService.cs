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
                return Result<ClienteResponseDto>.Failure(CodigoRespuesta.NotFound, "Cliente no encontrado");

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
                return Result<ClienteResponseDto>.Failure(CodigoRespuesta.Failure, "No se guardo el cliente");
            }

            return Result<ClienteResponseDto>.Success(ClienteResponseDto.FromEntity(cliente));
        }
        catch (DbUpdateException ex)
        {
            return Result<ClienteResponseDto>.Failure(
                CodigoRespuesta.BadRequest, 
                "No se pudo guardar: verifique que el municipio y estado sean válidos."
            );
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
                return Result.Failure(CodigoRespuesta.NotFound, "Cliente no encontrado");

            dto.UpdateEntity(cliente);
            clienteRepo.Update(cliente);
            var result = await  unitOfWorkBase.CompleteAsync();

            if (result != 0)
                return Result.Failure(CodigoRespuesta.Failure, "Error al actualizar");

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
                return Result.Failure(CodigoRespuesta.NotFound, "Cliente no encontrado");

            clienteRepo.Delete(cliente);

            var result = await unitOfWorkBase.CompleteAsync();
            if (result <= 0)
                return Result.Failure(CodigoRespuesta.Failure, "No se realizaron cambios");

            return Result.Success();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}