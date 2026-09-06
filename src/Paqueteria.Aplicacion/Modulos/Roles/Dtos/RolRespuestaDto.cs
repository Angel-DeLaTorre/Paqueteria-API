using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Modulos.Permisos.Dtos;

namespace Paqueteria.Application.Modulos.Roles.Dtos;

public record RolRespuestaDto
(
    Guid RolId,
    string Nombre,
    string Descripcion,
    IEnumerable<PermisoRespuestaDto> Permisos    
);