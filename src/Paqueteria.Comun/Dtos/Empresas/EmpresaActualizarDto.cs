using Paqueteria.Comun.Dtos.Comun;

namespace Paqueteria.Comun.Dtos.Empresas;

public record EmpresaActualizarDto
(
    Guid EmpresaId,
    string Nombre,
    string? NombreCorto,
    string Rfc,
    DireccionDto Direccion
);