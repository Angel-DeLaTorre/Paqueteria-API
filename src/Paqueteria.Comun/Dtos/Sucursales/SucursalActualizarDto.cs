using Paqueteria.Comun.Dtos.Comun;

namespace Paqueteria.Comun.Dtos.Sucursales;

public record SucursalActualizarDto
(
    Guid SucursalId,
    string Nombre,
    string Codigo,
    bool EsMatriz,
    DireccionDto Direccion,
    string Telefono
);