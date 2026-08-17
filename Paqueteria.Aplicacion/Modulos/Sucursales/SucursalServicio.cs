using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;

namespace Paqueteria.Application.Modulos.Sucursales;

public class SucursalServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : ISucursalServicio
{
    public async Task<Resultado<IReadOnlyList<SucursalResponseDto>>> ObtenerTodosAsync()
    {
        var sucursales =  ( await unit.Sucursales.ObtenerTodosAsync(contextoUsuario.EmpresaId) )
            .Select( SucursalResponseDto.FromEntity ).ToList();

        return Resultado<IReadOnlyList<SucursalResponseDto>>.Exitoso(sucursales);
    }

    public async Task<Resultado<SucursalResponseDto>> ObtenerPorIdAsync(Guid sucursalId)
    {
        var sucursal = await unit.Sucursales.ObtenerPorIdAsync(sucursalId, contextoUsuario.EmpresaId);
        if (sucursal == null)
            return Resultado<SucursalResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        return Resultado<SucursalResponseDto>.Exitoso(SucursalResponseDto.FromEntity(sucursal));
    }

    public async Task<Resultado<SucursalResponseDto>> AgregarAsync(SucursalCreateDto dto)
    {
        var sucursalIn = dto.ToEntity(contextoUsuario.EmpresaId);
        var sucursal = await unit.Sucursales.AgregarAsync( sucursalIn );

        var result = await unit.CompletarAsync();

        if (result <= 0)
            return Resultado<SucursalResponseDto>.Error(CodigosError.Generic.NoCreado);

        return Resultado<SucursalResponseDto>.Exitoso(SucursalResponseDto.FromEntity(sucursal));
    }

    public async Task<Resultado> ActualizarAsync(SucursaUpdateDto dto)
    {
        var sucursal = await unit.Sucursales.ObtenerPorIdAsync(dto.SucursalId, contextoUsuario.EmpresaId);

        if (sucursal == null) return Resultado.Error(CodigosError.Generic.NoEncontrado);

        dto.UpdateEntity(sucursal);
        var result = await  unit.CompletarAsync();

        if (result != 0)
            return Resultado.Error(CodigosError.Generic.NoActualizado);

        return Resultado.Exitoso();
    }
    
    public async Task<Resultado> DesactivarAsync(Guid sucursalId)
    {
        var sucursal = await unit.Sucursales.ObtenerPorIdAsync(sucursalId, contextoUsuario.EmpresaId);
        if (sucursal == null) return Resultado.Error(CodigosError.Generic.NoEncontrado);
        
        sucursal.Desactivar();
        await  unit.CompletarAsync();
        
        return Resultado.Exitoso();
    }
    
    public async Task<Resultado> ActivarAsync(Guid sucursalId)
    {
        var sucursal = await unit.Sucursales.ObtenerPorIdAsync(sucursalId, contextoUsuario.EmpresaId);
        if (sucursal == null) return Resultado.Error(CodigosError.Generic.NoEncontrado);
        
        sucursal.Activar();
        await  unit.CompletarAsync();
        
        return Resultado.Exitoso();
    }

    public async Task<Resultado> EliminarAsync(Guid idSucursal)
    {
        var sucursal = await unit.Sucursales.ObtenerPorIdAsync(idSucursal, contextoUsuario.EmpresaId);

        if (sucursal == null) return Resultado.Error(CodigosError.Generic.NoEncontrado);

        unit.Sucursales.Eliminar(sucursal);
        var result = await  unit.CompletarAsync();

        if (result != 0)
            return Resultado.Error(CodigosError.Generic.NoActualizado);

        return Resultado.Exitoso();
    }
}