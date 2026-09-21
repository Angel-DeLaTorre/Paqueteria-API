using Paqueteria.Aplicacion.Comun;
using Paqueteria.Aplicacion.Comun.Interfaces;
using Paqueteria.Aplicacion.Interfaces.Persistence;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Comun.Errores;
using Paqueteria.Comun.Dtos.Seguros;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Aplicacion.Modulos.Seguros;

public class SeguroServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : ISeguroServicio
{
    public async Task<Respuesta<IReadOnlyList<SeguroRespuestaDto>>> ObtenerTodosAsync()
    {
        var seguros = (await unit.Seguros.ObtenerTodosAsync(contextoUsuario.EmpresaId, false))
            .Select(s => s.MapeaRespuestaDto()).ToList();
        return Respuesta<IReadOnlyList<SeguroRespuestaDto>>.Exitoso(seguros);
    }

    public async Task<Respuesta<SeguroRespuestaDto>> ObtenerPorIdAsync(Guid seguroId)
    {
        var seguro = await unit.Seguros.ObtenerPorIdAsync(seguroId, contextoUsuario.EmpresaId, false);

        if (seguro is null)
            return Respuesta<SeguroRespuestaDto>.Error(CodigosError.Comun.NoEncontrado);

        return Respuesta<SeguroRespuestaDto>.Exitoso(seguro.MapeaRespuestaDto());
    }

    public async Task<Respuesta<SeguroRespuestaDto>> AgregarAsync(SeguroCrearDto dto)
    {
        var seguro = Seguro.Crear(dto.Nombre, contextoUsuario.EmpresaId);

        await unit.Seguros.AgregarAsync(seguro);
        var result = unit.GuardarCambiosAsync();

        if (result.IsCompletedSuccessfully)
            return Respuesta<SeguroRespuestaDto>.Error(CodigosError.Comun.NoCreado);

        return Respuesta<SeguroRespuestaDto>.Exitoso(seguro.MapeaRespuestaDto());
    }

    public async Task<Respuesta> ActualizarAsync(SeguroActualizarDto dto)
    {
        var seguro = await unit.Seguros.ObtenerPorIdAsync(dto.SeguroId, contextoUsuario.EmpresaId);

        if (seguro is null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        seguro.ActualizarDatos(dto.Nombre);
        var resultado = await unit.GuardarCambiosAsync();

        return resultado > 0 ?
            Respuesta.Exitoso()
            :Respuesta.Error(CodigosError.Comun.NoActualizado);
    }

    public async Task<Respuesta> EliminarAsync(Guid seguroId)
    {
        throw new NotImplementedException();
    }

    public async Task<Respuesta> DesactivarAsync(Guid seguroId)
    {
        var seguro = await unit.Seguros.ObtenerPorIdAsync(seguroId, contextoUsuario.EmpresaId);
        if (seguro == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        seguro.Desactivar();
        await unit.GuardarCambiosAsync();

        return Respuesta.Exitoso();
    }

    public async Task<Respuesta> ActivarAsync(Guid seguroId)
    {
        var seguro = await unit.Seguros.ObtenerPorIdAsync(seguroId, contextoUsuario.EmpresaId);
        if (seguro == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        seguro.Activar();
        await unit.GuardarCambiosAsync();

        return Respuesta.Exitoso();
    }
}