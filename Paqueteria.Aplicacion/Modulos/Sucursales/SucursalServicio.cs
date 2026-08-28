using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Dtos;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Sucursales.Dtos;
using Paqueteria.Core.Comun;
using Paqueteria.Core.Comun.Errors;

namespace Paqueteria.Application.Modulos.Sucursales;

public class SucursalServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : ISucursalServicio
{
    public async Task<Respuesta<IReadOnlyList<SucursalResponseDto>>> ObtenerTodosAsync()
    {
        var sucursales =  ( await unit.Sucursales.ObtenerTodosAsync(contextoUsuario.EmpresaId) )
            .Select( SucursalResponseDto.FromEntity ).ToList();

        return Respuesta<IReadOnlyList<SucursalResponseDto>>.Exitoso(sucursales);
    }

    public async Task<Respuesta<SucursalResponseDto>> ObtenerPorIdAsync(Guid sucursalId)
    {
        var sucursal = await unit.Sucursales.ObtenerPorIdAsync(sucursalId, contextoUsuario.EmpresaId);
        if (sucursal == null)
            return Respuesta<SucursalResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        return Respuesta<SucursalResponseDto>.Exitoso(SucursalResponseDto.FromEntity(sucursal));
    }

    public async Task<Respuesta<SucursalResponseDto>> AgregarAsync(SucursalCreateDto dto)
    {
        var sucursalIn = dto.ToEntity(contextoUsuario.EmpresaId);
        var sucursal = await unit.Sucursales.AgregarAsync( sucursalIn );

        var result = await unit.GuardarCambiosAsync();

        if (result <= 0)
            return Respuesta<SucursalResponseDto>.Error(CodigosError.Generic.NoCreado);

        return Respuesta<SucursalResponseDto>.Exitoso(SucursalResponseDto.FromEntity(sucursal));
    }

    public async Task<Respuesta> ActualizarAsync(SucursaUpdateDto dto)
    {
        var sucursal = await unit.Sucursales.ObtenerPorIdAsync(dto.SucursalId, contextoUsuario.EmpresaId);

        if (sucursal == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        dto.UpdateEntity(sucursal);
        var result = await  unit.GuardarCambiosAsync();

        if (result != 0)
            return Respuesta.Error(CodigosError.Generic.NoActualizado);

        return Respuesta.Exitoso();
    }
    
    public async Task<Respuesta> DesactivarAsync(Guid sucursalId)
    {
        var sucursal = await unit.Sucursales.ObtenerPorIdAsync(sucursalId, contextoUsuario.EmpresaId);
        if (sucursal == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);
        
        sucursal.Desactivar();
        await  unit.GuardarCambiosAsync();
        
        return Respuesta.Exitoso();
    }
    
    public async Task<Respuesta> ActivarAsync(Guid sucursalId)
    {
        var sucursal = await unit.Sucursales.ObtenerPorIdAsync(sucursalId, contextoUsuario.EmpresaId);
        if (sucursal == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);
        
        sucursal.Activar();
        await  unit.GuardarCambiosAsync();
        
        return Respuesta.Exitoso();
    }

    public async Task<Respuesta> EliminarAsync(Guid idSucursal)
    {
        var sucursal = await unit.Sucursales.ObtenerPorIdAsync(idSucursal, contextoUsuario.EmpresaId);

        if (sucursal == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        unit.Sucursales.Eliminar(sucursal);
        var result = await  unit.GuardarCambiosAsync();

        if (result != 0)
            return Respuesta.Error(CodigosError.Generic.NoActualizado);

        return Respuesta.Exitoso();
    }
}