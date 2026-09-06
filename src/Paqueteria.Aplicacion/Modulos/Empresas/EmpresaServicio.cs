using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Comun.Mapeador;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Empresas.Dtos;
using Paqueteria.Dominio.Comun;
using Paqueteria.Dominio.Comun.Errors;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Empresas;

public sealed class EmpresaServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : IEmpresaServicio
{
    public async Task<Respuesta<IReadOnlyList<EmpresaRespuestaDto>>> ObtenerTodosAsync()
    {

        var empresas =  ( await unit.Empresas.ObtenerTodosAsync(false) )
            .Select( e => e.MapeaRespuestaDto() ).ToList();

        return Respuesta<IReadOnlyList<EmpresaRespuestaDto>>.Exitoso(empresas);
    }

    public async Task<Respuesta<EmpresaRespuestaDto>> ObtenerPorIdAsync()
    {
        var empresa = await unit.Empresas.ObtenerPorIdAsync(contextoUsuario.EmpresaId);

        return empresa is not null
            ? Respuesta<EmpresaRespuestaDto>.Exitoso(empresa.MapeaRespuestaDto())
            : Respuesta<EmpresaRespuestaDto>.Error(CodigosError.Comun.NoEncontrado);

    }

    public async Task<Respuesta<EmpresaRespuestaDto>> AgregarAsync(EmpresaCrearDto dto)
    {
        var direccion = dto.Direccion.MapeaEntidad();
        
        var empresa = Empresa.Crear
        (
            dto.Nombre,
            dto.NombreCorto,
            dto.Rfc,
            direccion
        );
        
        await unit.Empresas.AgregarAsync( empresa );

        var resultado = await unit.GuardarCambiosAsync();

        return resultado > 0 
            ? Respuesta<EmpresaRespuestaDto>.Exitoso( empresa.MapeaRespuestaDto() )
            : Respuesta<EmpresaRespuestaDto>.Error(CodigosError.Comun.NoEncontrado);
    }

    public async Task<Respuesta> ActualizarAsync(EmpresaActualizarDto dto)
    {
        var empresa = await unit.Empresas.ObtenerPorIdAsync(dto.EmpresaId);

        if (empresa is null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        var direccion = dto.Direccion.MapeaEntidad();
        
        empresa.ActualizarDatos
        (
            dto.Nombre,
            dto.NombreCorto,
            dto.Rfc,
            direccion
        );
        
        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0
            ? Respuesta.Exitoso()
            : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }

    public async Task<Respuesta> EliminarAsync(Guid empresaId)
    {
        var empresa = (await unit.Empresas.ObtenerPorIdAsync(empresaId));

        if ( empresa is null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        unit.Empresas.Eliminar(empresa);

        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0
            ? Respuesta.Exitoso()
            : Respuesta.Error(CodigosError.Comun.NoEliminado);
    }
}