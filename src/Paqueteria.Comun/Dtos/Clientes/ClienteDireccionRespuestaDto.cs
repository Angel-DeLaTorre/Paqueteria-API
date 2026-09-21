using Paqueteria.Comun.Dtos.Comun;
using Paqueteria.Comun.Enums;

namespace Paqueteria.Comun.Dtos.Clientes;

public record ClienteDireccionRespuestaDto
(
    Guid DireccionId,
    DireccionRespuestaDto Direccion,
    EstatusBasico Estatus
);