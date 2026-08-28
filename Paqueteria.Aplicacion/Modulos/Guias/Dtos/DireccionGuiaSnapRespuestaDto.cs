using Paqueteria.Application.Comun.Dtos;

namespace Paqueteria.Application.Modulos.Guias.Dtos;

public record DireccionGuiaSnapRespuestaDto
(
    Guid DireccionGuiaId,
    DireccionRespuestaDto Direccion,
    string MunicipioNombre
);