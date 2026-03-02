using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Entities;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class ClienteService(IClienteRepository clienteRepo) : IClienteService
{
    public async Task<Result<IReadOnlyList<ClienteResponseDto>>> GetAllAsync()
    {
        try
        {
            var clientes = (await clienteRepo.GetAllAsync()).Select(ClienteResponseDto.FromEntity).ToList();

            if (clientes.Any())
                return Result<IReadOnlyList<ClienteResponseDto>>.Failure(CodigoRespuesta.NotFound, "Sin datos");

            return Result<IReadOnlyList<ClienteResponseDto>>.Success(clientes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<ClienteResponseDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var cliente = await clienteRepo.GetByIdAsync(id);

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
            var cliente = dto.ToEntity();

            var clienteCreado = await clienteRepo.AddAsync(cliente);

            return Result<ClienteResponseDto>.Success(ClienteResponseDto.FromEntity(clienteCreado));
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
            var cliente = await clienteRepo.AddAsync(new Cliente());
            return Result.Success();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<Result> DeleteAsync(Guid clienteId, UserContext currentUser)
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}