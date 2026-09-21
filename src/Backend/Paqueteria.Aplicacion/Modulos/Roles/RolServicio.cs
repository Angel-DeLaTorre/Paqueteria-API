using Paqueteria.Aplicacion.Comun;
using Paqueteria.Aplicacion.Comun.Interfaces;
using Paqueteria.Aplicacion.Interfaces.Persistence;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Comun.Errores;
using Paqueteria.Comun.Dtos.Roles;
using Paqueteria.Dominio.Comun;
using Paqueteria.Dominio.Entidades.Sistema;

namespace Paqueteria.Aplicacion.Modulos.Roles;

public sealed class RolServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : IRolServicio
{
    public async Task<Respuesta<RolRespuestaDto>> ObtenerPorIdAsync(Guid id)
    {
        var rol = await unit.Roles.ObtenerPorIdAsync(id, contextoUsuario.EmpresaId, true);

        return rol is not null
            ? Respuesta<RolRespuestaDto>.Exitoso(rol.MapeaRespuestaDto())
            : Respuesta<RolRespuestaDto>.Error(CodigosError.Comun.NoEncontrado);
    }

    public async Task<Respuesta<IEnumerable<RolRespuestaDto>>> ObtenerTodosAsync()
    {
        var listaRoles = await unit.Roles.ObtenerTodosAsync(contextoUsuario.EmpresaId);
        var dtos = listaRoles.Select(r => r.MapeaRespuestaDto()).ToList();
        return Respuesta<IEnumerable<RolRespuestaDto>>.Exitoso(dtos);
    }

    public async Task<Respuesta<RolRespuestaDto>> AgregarAsync(RolCrearDto dto)
    {
        var rolExistente = await unit.Roles.ObtenerPorNombreAsync(dto.Nombre, contextoUsuario.EmpresaId);
        if (rolExistente != null)
            return Respuesta<RolRespuestaDto>.Error(FabricaErrores.Duplicado<Permiso>());

        if (dto.PermisosIds is { Count: > 0 })
        {
            var permisosValidos =
                await unit.Permisos.ObtenerPorIdsYEmpresaAsync(dto.PermisosIds, contextoUsuario.EmpresaId);
            if (permisosValidos.Count != dto.PermisosIds.Count)
                return Respuesta<RolRespuestaDto>.Error(FabricaErrores.NoEncontrado<Permiso>());
        }

        var rol = Rol.Crear
        (
            dto.Nombre,
            dto.Descripcion,
            contextoUsuario.EmpresaId
        );

        if (dto.PermisosIds != null && dto.PermisosIds.Count != 0)
            foreach (var permisoId in dto.PermisosIds)
                rol.AgregarPermiso(permisoId);

        await unit.Roles.AgregarAsync(rol);

        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0
            ? Respuesta<RolRespuestaDto>.Exitoso(rol.MapeaRespuestaDto())
            : Respuesta<RolRespuestaDto>.Error(CodigosError.Comun.NoCreado);
    }

    public async Task<Respuesta> ActualizarAsync(RolActualizarDto dto)
    {
        var rol = await unit.Roles.ObtenerPorIdAsync(dto.RoleId, contextoUsuario.EmpresaId, true);
        if (rol is null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        rol.ActualizarDatos(dto.Nombre, dto.Descripcion);

        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? Respuesta.Exitoso() : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }

    public async Task<Respuesta> AgregarPermisosAsync(RolAgregarPermisoDto dto)
    {
        var rol = await unit.Roles.ObtenerPorIdAsync(dto.RolId, contextoUsuario.EmpresaId, true);
        if (rol is null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        foreach (var permisoId in dto.PermisosIds)
            await unit.Roles.AgregarPermisoAlRolAsync(rol.Id, permisoId, contextoUsuario.EmpresaId);

        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? Respuesta.Exitoso() : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }

    public async Task<Respuesta> ActivarAsync(Guid rolId)
    {
        var rol = await unit.Roles.ObtenerPorIdAsync(rolId, contextoUsuario.EmpresaId);
        if (rol == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        rol.Activar();

        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? Respuesta.Exitoso() : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }

    public async Task<Respuesta> DesactivarAsync(Guid rolId)
    {
        var rol = await unit.Roles.ObtenerPorIdAsync(rolId, contextoUsuario.EmpresaId);
        if (rol == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        rol.Desactivar();

        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? Respuesta.Exitoso() : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }

    public async Task<Respuesta> EliminarAsync(Guid id)
    {
        var rol = await unit.Roles.ObtenerPorIdAsync(id, contextoUsuario.EmpresaId);
        if (rol == null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        unit.Roles.Eliminar(rol);

        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0 ? Respuesta.Exitoso() : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }
}