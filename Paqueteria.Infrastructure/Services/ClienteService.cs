using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;
using Paqueteria.Infrastructure.Persistence;

namespace Paqueteria.Infrastructure.Services;

public class ClienteService(IClienteRepository clienteRepo, UnitOfWork unitOfWork) : IClienteService
{
    public async Task<Result<IReadOnlyList<ClienteResponseDto>>> GetAllAsync()
    {
        try
        {
            var clientes = ( await clienteRepo.GetAllAsync() )
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
            var cliente = await clienteRepo.AddAsync(dto.ToEntity());

            var result = unitOfWork.CompleteAsync();

            if (result.IsCompletedSuccessfully)
                return Result<ClienteResponseDto>.Failure(CodigoRespuesta.Failure, "Error al crear articulo");

            return Result<ClienteResponseDto>.Success(ClienteResponseDto.FromEntity(cliente));
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
            var cliente = await clienteRepo.GetByIdAsync(dto.IdCliente);

            if (cliente is null)
                return Result.Failure(CodigoRespuesta.NotFound, "Cliente no encontrado");

            dto.UpdateEntity(cliente);
            clienteRepo.Update(cliente);
            var result = await  unitOfWork.CompleteAsync();

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

            var result = await unitOfWork.CompleteAsync();
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