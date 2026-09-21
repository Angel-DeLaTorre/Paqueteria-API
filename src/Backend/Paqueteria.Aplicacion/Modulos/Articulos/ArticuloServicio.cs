using Paqueteria.Aplicacion.Comun;
using Paqueteria.Aplicacion.Interfaces.Persistence;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Comun.Errores;
using Paqueteria.Comun.Dtos.Articulos;
using Paqueteria.Dominio.Entidades.Sat;

namespace Paqueteria.Aplicacion.Modulos.Articulos;

public class ArticuloServicio(IUnitOfWork unit) : IArticuloServicio
{
    public async Task<Respuesta<IReadOnlyList<ArticuloRespuestaDto>>> ObtenerTodosAsync()
    {
        var articulos = (await unit.Articulos.ObtenerTodosAsync())
            .Select(a => a.MapearArticuloRespuesta()).ToList();
        return Respuesta<IReadOnlyList<ArticuloRespuestaDto>>.Exitoso(articulos);
    }

    public async Task<Respuesta<ArticuloRespuestaDto>> ObtenerPorIdAsync(Guid id)
    {
        var articulo = await unit.Articulos.ObtenerPorIdAsync(id);

        return articulo is not null
            ? Respuesta<ArticuloRespuestaDto>.Exitoso(articulo.MapearArticuloRespuesta())
            : Respuesta<ArticuloRespuestaDto>.Error(CodigosError.Comun.NoEncontrado);
    }

    public async Task<Respuesta<ArticuloRespuestaDto>> AgregarAsync(ArticuloCrearDto dto)
    {
        var articulo = Articulo.Create
        (
            dto.Texto,
            dto.Similares,
            dto.MaterialPeligroso,
            dto.VigenciaDesde,
            dto.VigenciaHasta
        );

        await unit.Articulos.AgregarAsync(articulo);

        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0
            ? Respuesta<ArticuloRespuestaDto>.Exitoso(articulo.MapearArticuloRespuesta())
            : Respuesta<ArticuloRespuestaDto>.Error(CodigosError.Comun.NoCreado);
    }

    public async Task<Respuesta> ActualizarAsync(ArticuloActualizarDto dto)
    {
        var articulo = await unit.Articulos.ObtenerPorIdAsync(dto.ArticuloId);

        if (articulo == null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0
            ? Respuesta.Exitoso()
            : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }

    public async Task<Respuesta> EliminarAsync(Guid articuloId)
    {
        var articulo = await unit.Articulos.ObtenerPorIdAsync(articuloId);

        if (articulo == null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        unit.Articulos.Eliminar(articulo);

        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0
            ? Respuesta.Exitoso()
            : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }
}