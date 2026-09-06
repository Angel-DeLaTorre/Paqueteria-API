using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Rutas.Dtos;
using Paqueteria.Dominio.Comun;
using Paqueteria.Dominio.Comun.Errors;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Rutas;

public class RutaServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : IRutaServicio
{
    public async Task<Respuesta<IReadOnlyList<RutaRespuestaDto>>> ObtenerTodosAsync()
    {
        var rutas = ( await unit.Rutas.ObtenerTodosAsync(contextoUsuario.EmpresaId) )
            .Select( r => r.MapeaRespuestaDto() ).ToList();
        return Respuesta<IReadOnlyList<RutaRespuestaDto>>.Exitoso(rutas);
    }

    public async Task<Respuesta<RutaRespuestaDto>> ObtenerPorIdAsync(Guid rutaId)
    {
        var ruta = await unit.Rutas.ObtenerPorIdAsync(rutaId, contextoUsuario.EmpresaId);

        return ruta is not null ? 
            Respuesta<RutaRespuestaDto>.Exitoso( ruta.MapeaRespuestaDto() ) 
            : Respuesta<RutaRespuestaDto>.Error(CodigosError.Comun.NoEncontrado);
    }

    public async Task<Respuesta<RutaRespuestaDto>> AgregarAsync(RutaCrearDto dto)
    {

        var ruta = Ruta.Crear
        (
            dto.Descripcion,
            dto.SucursalOrigenId,
            dto.SucursalDestinoId,
            contextoUsuario.EmpresaId
        );
        
        await unit.Rutas.AgregarAsync(ruta);
        
        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? 
            Respuesta<RutaRespuestaDto>.Exitoso( ruta.MapeaRespuestaDto() ) 
            : Respuesta<RutaRespuestaDto>.Error(CodigosError.Comun.NoCreado);
    }

    public async Task<Respuesta> ActualizarAsync(RutaActualizarDto dto)
    {
        var ruta = await unit.Rutas.ObtenerPorIdAsync(dto.RutaId, contextoUsuario.EmpresaId);

        if (ruta is null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        ruta.ActualizarDatos(dto.Descripcion, dto.SucursalOrigenId, dto.SucursalDestinoId);
        var resultado = await unit.GuardarCambiosAsync();
        
        return resultado > 0 ? Respuesta.Exitoso() : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }
    
    public async Task<Respuesta> ActivarAsync(Guid rutaId)
    {
        var ruta = await unit.Rutas.ObtenerPorIdAsync(rutaId, contextoUsuario.EmpresaId);
        if (ruta == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);
        
        ruta.Activar();
        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? Respuesta.Exitoso() : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }
    
    public async Task<Respuesta> DesactivarAsync(Guid rutaId)
    {
        var ruta = await unit.Rutas.ObtenerPorIdAsync(rutaId, contextoUsuario.EmpresaId);
        if (ruta == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);
        
        ruta.Desactivar();
        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? Respuesta.Exitoso() : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }

    public async Task<Respuesta> EliminarAsync(Guid rutaId)
    {
        var ruta = await unit.Rutas.ObtenerPorIdAsync(rutaId, contextoUsuario.EmpresaId);

        if (ruta is null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        unit.Rutas.Eliminar(ruta);
        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? Respuesta.Exitoso() : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }
}