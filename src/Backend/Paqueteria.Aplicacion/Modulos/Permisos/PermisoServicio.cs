using Paqueteria.Aplicacion.Comun;
using Paqueteria.Aplicacion.Comun.Interfaces;
using Paqueteria.Aplicacion.Interfaces.Persistence;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Comun.Errores;
using Paqueteria.Comun.Dtos.Permisos;
using Paqueteria.Dominio.Comun;
using Paqueteria.Dominio.Entidades.Sistema;

namespace Paqueteria.Aplicacion.Modulos.Permisos;

public sealed class PermisoServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : IPermisoServicio
{
    public async Task<Respuesta<PermisoRespuestaDto>> ObtenerPorIdAsync(Guid id)
    {
        var permiso = await unit.Permisos.ObtenerPorIdAsync(id, contextoUsuario.EmpresaId);

        return permiso is not null
            ? Respuesta<PermisoRespuestaDto>.Exitoso(permiso.MapeaRespuestaDto())
            : Respuesta<PermisoRespuestaDto>.Error(FabricaErrores.NoEncontrado<Permiso>());
    }

    public async Task<Respuesta<IEnumerable<PermisoRespuestaDto>>> ObtenerTodosAsync()
    {
        var listaPermisos = await unit.Permisos.ObtenerTodosAsync(contextoUsuario.EmpresaId);

        var dtos = listaPermisos.Select(p => p.MapeaRespuestaDto()).ToList();

        return Respuesta<IEnumerable<PermisoRespuestaDto>>.Exitoso(dtos);
    }

    public async Task<Respuesta<PermisoRespuestaDto>> AgregarAsync(PermisoCrearDto dto)
    {
        var permisoExistente = await unit.Permisos.ObtenerPorNombreAsync(dto.Nombre, contextoUsuario.EmpresaId);
        if (permisoExistente != null)
            return Respuesta<PermisoRespuestaDto>.Error(FabricaErrores.Duplicado<Permiso>());

        var permiso = Permiso.Crear
        (
            dto.Nombre,
            dto.Descripcion,
            contextoUsuario.EmpresaId
        );

        await unit.Permisos.AgregarAsync(permiso);

        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0
            ? Respuesta<PermisoRespuestaDto>.Exitoso(permiso.MapeaRespuestaDto())
            : Respuesta<PermisoRespuestaDto>.Error(FabricaErrores.NoCreado<Permiso>());
    }

    public async Task<Respuesta> ActualizarAsync(Guid permisoId, PermisoActualizarDto dto)
    {
        var permiso = await unit.Permisos.ObtenerPorIdAsync(permisoId, contextoUsuario.EmpresaId);
        if (permiso == null)
            return Respuesta.Error(FabricaErrores.NoEncontrado<Permiso>());

        permiso.ActualizarDatos(dto.Nombre, dto.Descripcion);

        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? Respuesta.Exitoso() : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }

    public async Task<Respuesta> ActivarAsync(Guid permisoId)
    {
        var permiso = await unit.Permisos.ObtenerPorIdAsync(permisoId, contextoUsuario.EmpresaId);
        if (permiso == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        permiso.Activar();

        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? Respuesta.Exitoso() : Respuesta.Error(FabricaErrores.NoActualizado<Permiso>());
    }

    public async Task<Respuesta> DesactivarAsync(Guid permisoId)
    {
        var permiso = await unit.Permisos.ObtenerPorIdAsync(permisoId, contextoUsuario.EmpresaId);
        if (permiso == null) return Respuesta.Error(FabricaErrores.NoEncontrado<Permiso>());

        permiso.Desactivar();
        await unit.GuardarCambiosAsync();

        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? Respuesta.Exitoso() : Respuesta.Error(FabricaErrores.NoActualizado<Permiso>());
    }

    public async Task<Respuesta> EliminarAsync(Guid id)
    {
        var permiso = await unit.Permisos.ObtenerPorIdAsync(id, contextoUsuario.EmpresaId);
        if (permiso == null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        unit.Permisos.Eliminar(permiso);

        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? Respuesta.Exitoso() : Respuesta.Error(FabricaErrores.NoEliminado<Permiso>());
    }
}