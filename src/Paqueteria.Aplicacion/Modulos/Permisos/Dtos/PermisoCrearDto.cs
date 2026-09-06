using System.ComponentModel.DataAnnotations;

namespace Paqueteria.Application.Modulos.Permisos.Dtos;

public record PermisoCrearDto
(
    string Nombre,
    string Descripcion
);