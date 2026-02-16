using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Entities;
using Paqueteria.Core.Enums;
using Paqueteria.Infrastructure.Repositories;

namespace Paqueteria.Infrastructure.Services;

public class ClienteService(ClienteRepository clienteRepo) : IClienteService
{
    public async Task<IReadOnlyList<Cliente>> ObtenerTodosAsync()
    {
        return await clienteRepo.GetAllAsync();
    }
    public async Task<ClienteResponseDto> CreateAsync(ClienteCreateDto dto, Guid usuarioId,  Guid sucursalId)
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

        await clienteRepo.AddAsync(cliente);

        return new ClienteResponseDto(
            cliente.Id,
            cliente.Nombre
        );
    }


}