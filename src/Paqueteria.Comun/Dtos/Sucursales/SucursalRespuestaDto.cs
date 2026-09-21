using Paqueteria.Comun.Dtos.Comun;
using Paqueteria.Comun.Enums;

namespace Paqueteria.Comun.Dtos.Sucursales;

public record SucursalRespuestaDto
(
    Guid SucursalId,
    string Nombre,
    string Codigo,
    bool EsMatriz,
    DireccionRespuestaDto Direccion,
    string? Telefono,
    EstatusBasico Estatus
);