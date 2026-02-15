using Microsoft.EntityFrameworkCore;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Entities;

namespace Paqueteria.Infrastructure.Services;

public abstract class SucursalService(ISucursalRepository repository) : ISucursalService
{
    public async Task<SucursalResponseDto?> ObtenerSucursalByIdAsync(Guid sucursalId)
    {
        try
        {
            var sucursal = (await repository.ObtenerSucursalPorIdAsync(sucursalId));
            if (sucursal != null)
                return new SucursalResponseDto(
                    sucursal.IdSucursal,
                    sucursal.Codigo,
                    sucursal.Calle,
                    sucursal.Colonia,
                    sucursal.NumeroExterior,
                    sucursal.NumeroInterior,
                    sucursal.Localidad,
                    sucursal.Municipio.Nombre,
                    sucursal.Telefono,
                    sucursal.Estatus
                );
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public async Task<ICollection<SucursalResponseDto>> ObtenerSucursalesAsync()
    {
        try
        {
            return (await repository.ObtenerSucursalesAsync())
                .Select( s => new SucursalResponseDto(
                    s.IdSucursal,
                    s.Codigo,
                    s.Calle,
                    s.Colonia,
                    s.NumeroExterior,
                    s.NumeroInterior,
                    s.Localidad,
                    s.Municipio.Nombre,
                    s.Telefono,
                    s.Estatus
                )).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public async Task<SucursalResponseDto> InsertarSucursalAsync(SucursalCreateDto dto)
    {
        try
        {
            var x = await repository.InsertarSucursalAsync(
                new Sucursal(
                    dto.Nombre,
                    dto.Codigo,
                    dto.EsMatriz,
                    dto.Calle,
                    dto.Colonia,
                    dto.NumeroExterior,
                    dto.NumeroInterior,
                    dto.Localidad,
                    dto.MunicipioId,
                    dto.Telefono
                )
            );

            return new SucursalResponseDto(
                x.IdSucursal,
                x.Codigo,
                x.Calle,
                x.Colonia,
                x.NumeroExterior,
                x.NumeroInterior,
                x.Localidad,
                x.Municipio.Nombre,
                x.Telefono,
                x.Estatus
            );
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> ActualizarSucursalAsync(Guid idSucursal, SucursaUpdateDto dto)
    {
        try
        {
            var sucursalExistente = await repository.ObtenerSucursalPorIdAsync(idSucursal);

            if (sucursalExistente == null) return false;

            await repository.EditarSucursalAsync(new Sucursal(
                    dto.Nombre,
                    dto.Codigo,
                    dto.EsMatriz,
                    dto.Calle,
                    dto.Colonia,
                    dto.NumeroExterior,
                    dto.NumeroInterior,
                    dto.Localidad,
                    dto.MunicipioId,
                    dto.Telefono
                )
            );
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DesactivarSucursalAsync(Guid idSucursal)
    {
        try
        {
            var sucursalExistente = await repository.ObtenerSucursalPorIdAsync(idSucursal);

            if (sucursalExistente == null) return false;

            await repository.DesactivarSucursalAsync(idSucursal);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}