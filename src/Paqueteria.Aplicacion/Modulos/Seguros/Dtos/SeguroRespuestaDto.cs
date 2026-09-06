using System.ComponentModel.DataAnnotations;
using Paqueteria.Dominio.Enums;

namespace Paqueteria.Application.Modulos.Seguros.Dtos;

public record SeguroRespuestaDto( 
    Guid SeguroId,
    string Nombre,
    EstatusBasico Estatus
);