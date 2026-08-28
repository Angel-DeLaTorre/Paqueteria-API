using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Comun.Errors;

public record BaseError
(
    CodigoRespuesta Code,
    string Description
);