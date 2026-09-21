using Paqueteria.Comun.Dtos.Permisos;

namespace Paqueteria.Comun.Dtos.Roles;

public record RolRespuestaDto
(
    Guid RolId,
    string Nombre,
    string Descripcion,
    IEnumerable<PermisoRespuestaDto> Permisos    
);