using Paqueteria.Application.Modulos.Articulos.Dtos;
using Paqueteria.Dominio.Entidades.Sat;

namespace Paqueteria.Application.Modulos.Articulos;

public static class ArticuloMapeador
{
    extension(Articulo articulo)
    {
        public ArticuloRespuestaDto MapearArticuloRespuesta()
        {
            return new ArticuloRespuestaDto
            (
                articulo.Id,
                articulo.Texto,
                articulo.Similares,
                articulo.MaterialPeligroso
            );
        }
    }
}