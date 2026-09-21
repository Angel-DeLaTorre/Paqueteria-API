using Paqueteria.Comun.Enums;

namespace Paqueteria.Comun.Dtos.Seguros;

public record SeguroRespuestaDto( 
    Guid SeguroId,
    string Nombre,
    EstatusBasico Estatus
);