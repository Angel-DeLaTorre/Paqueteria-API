using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Comun.Dtos;

namespace Paqueteria.Application.Modulos.Empresas.Dtos;

public record EmpresaCrearDto
(
    string Nombre,
    string NombreCorto,
    string Rfc,
    DireccionDto Direccion
);