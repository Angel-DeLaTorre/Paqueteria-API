using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;

namespace Paqueteria.Application.Modulos.Rutas;

public class RutaServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : IRutaServicio
{
    public async Task<Resultado<IReadOnlyList<RutaResponseDto>>> ObtenerTodosAsync()
    {
        var rutas = ( await unit.Rutas.ObtenerTodosAsync(contextoUsuario.EmpresaId) )
            .Select( RutaResponseDto.FromEntity ).ToList();
        return Resultado<IReadOnlyList<RutaResponseDto>>.Exitoso(rutas);
    }

    public async Task<Resultado<RutaResponseDto>> ObtenerPorIdAsync(Guid rutaId)
    {
        var ruta = await unit.Rutas.ObtenerPorIdAsync(rutaId, contextoUsuario.EmpresaId);

        if (ruta is null)
            return Resultado<RutaResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        return Resultado<RutaResponseDto>.Exitoso(RutaResponseDto.FromEntity(ruta));
    }

    public async Task<Resultado<RutaResponseDto>> AgregarAsync(RutaCreateDto dto)
    {
        var ruta = await unit.Rutas.AgregarAsync(dto.ToEntity(contextoUsuario.EmpresaId));

        await unit.CompletarAsync();
        
        ruta = await unit.Rutas.ObtenerPorIdAsync(ruta.Id, contextoUsuario.EmpresaId);

        return Resultado<RutaResponseDto>.Exitoso(RutaResponseDto.FromEntity(ruta));
    }

    public async Task<Resultado> ActualizarAsync(RutaUpdateDto dto)
    {
        var ruta = await unit.Rutas.ObtenerPorIdAsync(dto.RutaId, contextoUsuario.EmpresaId);

        if (ruta is null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);

        dto.UpdateEntity(ruta);
        await unit.CompletarAsync();

        return Resultado.Exitoso();
    }
    
    public async Task<Resultado> DesactivarAsync(Guid rutaId)
    {
        var ruta = await unit.Rutas.ObtenerPorIdAsync(rutaId, contextoUsuario.EmpresaId);
        if (ruta == null) return Resultado.Error(CodigosError.Generic.NoEncontrado);
        
        ruta.Desactivar();
        await  unit.CompletarAsync();
        
        return Resultado.Exitoso();
    }
    
    public async Task<Resultado> ActivarAsync(Guid rutaId)
    {
        var ruta = await unit.Rutas.ObtenerPorIdAsync(rutaId, contextoUsuario.EmpresaId);
        if (ruta == null) return Resultado.Error(CodigosError.Generic.NoEncontrado);
        
        ruta.Activar();
        await  unit.CompletarAsync();
        
        return Resultado.Exitoso();
    }

    public async Task<Resultado> EliminarAsync(Guid rutaId)
    {
        var ruta = await unit.Rutas.ObtenerPorIdAsync(rutaId, contextoUsuario.EmpresaId);

        if (ruta is null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);

        unit.Rutas.Eliminar(ruta);
        await unit.CompletarAsync();

        return Resultado.Exitoso();
    }
}