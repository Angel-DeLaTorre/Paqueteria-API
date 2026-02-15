namespace Paqueteria.Application.DTOs;

public record ArticuloCreateDto
(
    string Clave,
    string Descripcion
);

public record ArticuloUpdateDto(
    string IdArticulo,
    string Descripcion
);

public record ArticuloResponseDto
(
    Guid IdArticulo,
    string Clave,
    string Descripcion
);