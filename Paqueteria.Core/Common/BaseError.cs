using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Common;

public record BaseError
(
    CodigoRespuesta Code,
    string Description
);