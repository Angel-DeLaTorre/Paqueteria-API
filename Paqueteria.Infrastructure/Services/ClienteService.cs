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
        var clientes = await clienteRepo.GetAllAsync();

        if (clientes.Any())
            return Result<IReadOnlyList<ClienteResponseDto>>.Failure(CodigoRespuesta.NotFound, "Sin datos");

        return Result<IReadOnlyList<ClienteResponseDto>>.Success(EntityToResponseDto(clientes));
    }

    public async Task<Result<ClienteResponseDto>> GetByIdAsync(Guid id)
    {
        var cliente = await clienteRepo.GetByIdAsync(id);

        if (cliente == null)
            return Result<ClienteResponseDto>.Failure(CodigoRespuesta.NotFound, "Cliente no encontrado");

        return Result<ClienteResponseDto>.Success(EntityToResponseDto(cliente));
    }

    public async Task<Result<ClienteResponseDto>> CreateAsync(ClienteCreateDto dto, Guid usuarioId,  Guid sucursalId)
    {
        var cliente = dto.ToEntity();

        var clienteCreado = await clienteRepo.AddAsync(cliente);

        return Result<ClienteResponseDto>.Success(ClienteResponseDto.FromEntity(clienteCreado));
    }

    public async Task<Result<bool>> UpdateAsync(ClienteUpdateDto dto)
    {
        var cliente = await clienteRepo.AddAsync(new Cliente());
        return Result<bool>.Success(true);
    }

    private ClienteResponseDto EntityToResponseDto(Cliente cliente)
    {
        return new ClienteResponseDto(
            cliente.Id,
            cliente.Nombre);
    }

    private List<ClienteResponseDto> EntityToResponseDto(IReadOnlyList<Cliente> clientes)
    {
        return clientes.Select(EntityToResponseDto).ToList();
    }

}