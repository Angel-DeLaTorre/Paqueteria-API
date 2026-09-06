using Paqueteria.Application.Comun.Dtos;
using Paqueteria.Dominio.Enums;

namespace Paqueteria.Application.Modulos.Clientes.Dtos;

public record ClienteDireccionRespuestaDto
(
    Guid DireccionId,
    DireccionRespuestaDto Direccion,
    EstatusBasico Estatus
);