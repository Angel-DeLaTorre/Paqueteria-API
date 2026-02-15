namespace Paqueteria.Application.DTOs;

public record SeguroCreateDto(
    string Nombre
);

public record SeguroUpdateDto(
    Guid SeguroId,
    string Nombre
);

public record SeguroResponseDto(
    Guid SeguroId,
    string Nombre
);