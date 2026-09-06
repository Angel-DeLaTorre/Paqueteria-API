using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Comun.Dtos;

namespace Paqueteria.Application.Modulos.Empresas.Dtos;

public record EmpresaRespuestaDto
(
    Guid EmpresaId,
    string Nombre,
    string? NombreCorto,
    string Rfc,
    DireccionRespuestaDto? Direccion,
    DateTime FechaAlta
);