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
        var cliente = new Cliente
        {
            Nombre = dto.Nombre,
            Estatus = EstatusGenerico.Activo,
            Rfc = dto.Rfc,
            Direccion = dto.Direccion,
            DireccionComplemento = dto.DireccionComplemento,
            CodigoPostal  = dto.CodigoPostal,
            MunicipioId = Guid.Parse(dto.IdMunicipio),
            Telefono = dto.Telefono,
            Telefono2  = dto.Telefono2,
            Correo  = dto.Correo,
            Contacto  = dto.Contacto,
            NumConvenio  = dto.NumConvenio,
            PolizaSeguro  = dto.PolizaSeguro,
            SucursalId = Guid.Parse(dto.IdSucursal)
        };

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