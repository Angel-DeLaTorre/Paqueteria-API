using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Rutas.Dtos;
using Paqueteria.Core.Comun;
using Paqueteria.Core.Comun.Errors;

namespace Paqueteria.Application.Modulos.Rutas;

public class RutaServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : IRutaServicio
{
    public async Task<Respuesta<IReadOnlyList<RutaRespuestaDto>>> ObtenerTodosAsync()
    {
        var rutas = ( await unit.Rutas.ObtenerTodosAsync(contextoUsuario.EmpresaId) )
            .Select( RutaRespuestaDto.FromEntity ).ToList();
        return Respuesta<IReadOnlyList<RutaRespuestaDto>>.Exitoso(rutas);
    }

    public async Task<Respuesta<RutaRespuestaDto>> ObtenerPorIdAsync(Guid rutaId)
    {
        var ruta = await unit.Rutas.ObtenerPorIdAsync(rutaId, contextoUsuario.EmpresaId);

        if (ruta is null)
            return Respuesta<RutaRespuestaDto>.Error(CodigosError.Generic.NoEncontrado);

        return Respuesta<RutaRespuestaDto>.Exitoso(RutaRespuestaDto.FromEntity(ruta));
    }

    public async Task<Respuesta<RutaRespuestaDto>> AgregarAsync(RutaCrearDto dto)
    {
        var ruta = await unit.Rutas.AgregarAsync(dto.ToEntity(contextoUsuario.EmpresaId));

        await unit.GuardarCambiosAsync();
        
        ruta = await unit.Rutas.ObtenerPorIdAsync(ruta.Id, contextoUsuario.EmpresaId);

        return Respuesta<RutaRespuestaDto>.Exitoso(RutaRespuestaDto.FromEntity(ruta));
    }

    public async Task<Respuesta> ActualizarAsync(RutaActualizarDto dto)
    {
        var ruta = await unit.Rutas.ObtenerPorIdAsync(dto.RutaId, contextoUsuario.EmpresaId);

        if (ruta is null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        dto.UpdateEntity(ruta);
        await unit.GuardarCambiosAsync();

        return Respuesta.Exitoso();
    }
    
    public async Task<Respuesta> DesactivarAsync(Guid rutaId)
    {
        var ruta = await unit.Rutas.ObtenerPorIdAsync(rutaId, contextoUsuario.EmpresaId);
        if (ruta == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);
        
        ruta.Desactivar();
        await  unit.GuardarCambiosAsync();
        
        return Respuesta.Exitoso();
    }
    
    public async Task<Respuesta> ActivarAsync(Guid rutaId)
    {
        var ruta = await unit.Rutas.ObtenerPorIdAsync(rutaId, contextoUsuario.EmpresaId);
        if (ruta == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);
        
        ruta.Activar();
        await  unit.GuardarCambiosAsync();
        
        return Respuesta.Exitoso();
    }

    public async Task<Respuesta> EliminarAsync(Guid rutaId)
    {
        var ruta = await unit.Rutas.ObtenerPorIdAsync(rutaId, contextoUsuario.EmpresaId);

        if (ruta is null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        unit.Rutas.Eliminar(ruta);
        await unit.GuardarCambiosAsync();

        return Respuesta.Exitoso();
    }
}