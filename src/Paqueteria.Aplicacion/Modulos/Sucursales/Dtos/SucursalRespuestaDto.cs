using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Comun.Dtos;
using Paqueteria.Dominio.Enums;

namespace Paqueteria.Application.Modulos.Sucursales.Dtos;

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