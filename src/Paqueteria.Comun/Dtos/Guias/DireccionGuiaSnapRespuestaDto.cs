using Paqueteria.Comun.Dtos.Comun;

namespace Paqueteria.Comun.Dtos.Guias;

public record DireccionGuiaSnapRespuestaDto
(
    Guid DireccionGuiaId,
    DireccionRespuestaDto Direccion,
    string MunicipioNombre
);