using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Common.Errors;

public record BaseError
(
    CodigoRespuesta Code,
    string Description
);