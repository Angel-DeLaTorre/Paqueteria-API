namespace Paqueteria.Application.Modulos.Guias.Dtos;

public record GuiaCreada
(
    Guid Id,
    string Clave,
    DateTime FechaCaptura
);