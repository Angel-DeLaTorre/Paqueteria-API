using Paqueteria.Dominio.Enums;

namespace Paqueteria.Dominio.Comun.Errors;

public record BaseError
(
    CodigoRespuesta Code,
    string Description
);