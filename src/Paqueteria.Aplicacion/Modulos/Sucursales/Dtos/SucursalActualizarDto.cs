using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Comun.Dtos;

namespace Paqueteria.Application.Modulos.Sucursales.Dtos;

public record SucursalActualizarDto
(
    Guid SucursalId,
    string Nombre,
    string Codigo,
    bool EsMatriz,
    DireccionDto Direccion,
    string Telefono
);