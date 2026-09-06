using System.ComponentModel.DataAnnotations;

namespace Paqueteria.Application.Modulos.Seguros.Dtos;

public record SeguroActualizarDto( Guid SeguroId, string Nombre);