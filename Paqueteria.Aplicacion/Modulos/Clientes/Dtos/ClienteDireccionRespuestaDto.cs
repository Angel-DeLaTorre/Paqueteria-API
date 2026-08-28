using Paqueteria.Application.Comun.Dtos;
using Paqueteria.Core.Enums;

namespace Paqueteria.Application.Modulos.Clientes.Dtos;

public record ClienteDireccionRespuestaDto
(
    Guid DireccionId,
    DireccionRespuestaDto Direccion,
    EstatusBasico Estatus
);