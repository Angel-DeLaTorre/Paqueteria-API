using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Comun.Dtos;

namespace Paqueteria.Application.Modulos.Choferes.Dtos;

public record ChoferCrearDto
(
    string Nombre,
    string ApellidoPaterno,
    string ApellidoMaterno,
    DireccionDto Direccion,
    string Telefono,
    string? NumCamion,
    string? NumContenedor,
    string? NumContenedor2
);