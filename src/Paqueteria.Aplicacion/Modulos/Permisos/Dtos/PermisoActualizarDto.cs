using System.ComponentModel.DataAnnotations;

namespace Paqueteria.Application.Modulos.Permisos.Dtos;

public record PermisoActualizarDto
(
    string Nombre,
    string Descripcion
);