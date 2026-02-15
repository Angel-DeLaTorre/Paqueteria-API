using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Entities;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class ClienteService(IUnitOfWork unitOfWork) : IClienteService
{
    public async Task<IEnumerable<ClienteResponseDto>> ObtenerTodosAsync()
    {
        var clientes = await unitOfWork.Repository<Cliente>().GetAllAsync();
        return clientes.Select(c => new ClienteResponseDto(
            c.IdCliente,
            c.Nombre
        ));
    }
    public async Task<ClienteResponseDto> CrearClienteAsync(ClienteCreateDto dto, Guid usuarioId,  Guid sucursalId)
    {
        var cliente = new Cliente
        {
            Nombre = dto.Nombre,
            Estatus = EstatusGenerico.Activo,
            Rfc = dto.Rfc,
            Direccion = dto.Direccion,
            DireccionComplemento = dto.DireccionComplemento,
            CodigoPostal  = dto.CodigoPostal,
            IdMunicipio = Guid.Parse(dto.IdMunicipio),
            Telefono = dto.Telefono,
            Telefono2  = dto.Telefono2,
            Correo  = dto.Correo,
            Contacto  = dto.Contacto,
            NumConvenio  = dto.NumConvenio,
            PolizaSeguro  = dto.PolizaSeguro,
            IdSucursal = Guid.Parse(dto.IdSucursal)
        };

        await unitOfWork.Repository<Cliente>().AddAsync(cliente);

        await unitOfWork.Complete(usuarioId, sucursalId);

        return new ClienteResponseDto(
            cliente.IdCliente,
            cliente.Nombre
        );
    }


}