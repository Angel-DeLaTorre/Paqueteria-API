using Paqueteria.Comun.Dtos.Comun;

namespace Paqueteria.Comun.Dtos.Empresas;

public record EmpresaRespuestaDto
(
    Guid EmpresaId,
    string Nombre,
    string? NombreCorto,
    string Rfc,
    DireccionRespuestaDto? Direccion,
    DateTime FechaAlta
);