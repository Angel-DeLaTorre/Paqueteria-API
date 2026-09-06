using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Comun.Mapeador;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Choferes.Dtos;
using Paqueteria.Dominio.Comun;
using Paqueteria.Dominio.Comun.Errors;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Choferes;

public class ChoferServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : IChoferServicio
{
    public async Task<Respuesta<IReadOnlyList<ChoferRespuestaDto>>> ObtenerTodosAsync()
    {

        var choferes = ( await unit.Choferes.ObtenerTodosAsync(contextoUsuario.EmpresaId) )
            .Select( c => c.MapeaRespuestaDto() )
            .ToList();
        return Respuesta<IReadOnlyList<ChoferRespuestaDto>>.Exitoso(choferes);
        
    }

    public async Task<Respuesta<ChoferRespuestaDto>> ObtenerPorIdAsync(Guid choferId)
    {

        var chofer = await unit.Choferes.ObtenerPorIdAsync(choferId, contextoUsuario.EmpresaId);

        return chofer is not null ?
            Respuesta<ChoferRespuestaDto>.Exitoso( chofer.MapeaRespuestaDto() )
            : Respuesta<ChoferRespuestaDto>.Error(CodigosError.Comun.NoEncontrado);
    }

    public async Task<Respuesta<ChoferRespuestaDto>> AgregarAsync(ChoferCrearDto dto)
    {
        var direccion = dto.Direccion.MapeaEntidad();
        
        var chofer = Chofer.Crear(
            dto.Nombre,
            dto.ApellidoPaterno,
            dto.ApellidoMaterno,
            direccion,
            dto.Telefono,
            dto.NumCamion,
            dto.NumContenedor,
            dto.NumContenedor2,
            contextoUsuario.EmpresaId
        );
        
        await unit.Choferes.AgregarAsync(chofer);

        var resultado = await unit.GuardarCambiosAsync();
        
        return resultado > 0 ? 
            Respuesta<ChoferRespuestaDto>.Exitoso( chofer.MapeaRespuestaDto() ) 
            : Respuesta<ChoferRespuestaDto>.Error(CodigosError.Comun.NoCreado);
    }

    public async Task<Respuesta> ActualizarAsync(ChoferActualizarDto dto)
    {

        var chofer = await unit.Choferes.ObtenerPorIdAsync(dto.ChoferId,  contextoUsuario.EmpresaId);

        if (chofer is null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        var direccion = dto.Direccion.MapeaEntidad();
    
        chofer.ActualizarDatosPersonales
        (
            dto.Nombre,
            dto.ApellidoPaterno,
            dto.ApellidoMaterno,
            dto.Telefono,
            direccion
        );
        
        chofer.AsignarCamion( dto.NumCamion, dto.NumContenedor, dto.NumContenedor2 );
        
        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? 
            Respuesta.Exitoso() 
            : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }

    public async Task<Respuesta> EliminarAsync(Guid choferId)
    {
        var chofer = (await unit.Choferes.ObtenerPorIdAsync(choferId,  contextoUsuario.EmpresaId));

        if (chofer is null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        unit.Choferes.Eliminar(chofer);

        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? 
            Respuesta.Exitoso() 
            : Respuesta.Error(CodigosError.Comun.NoEliminado);
    }
    
    public async Task<Respuesta> ActivarAsync(Guid choferId)
    {
        var chofer = await unit.Choferes.ObtenerPorIdAsync(choferId, contextoUsuario.EmpresaId);
        if (chofer == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);
        
        chofer.Activar();
        
        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? 
            Respuesta.Exitoso() 
            : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }
    
    public async Task<Respuesta> DesactivarAsync(Guid choferId)
    {
        var chofer = await unit.Choferes.ObtenerPorIdAsync(choferId, contextoUsuario.EmpresaId);
        if (chofer == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);
        
        chofer.Desactivar();
        await unit.GuardarCambiosAsync();
        
        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? 
            Respuesta.Exitoso() 
            : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }
}