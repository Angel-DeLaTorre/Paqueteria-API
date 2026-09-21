using Paqueteria.Comun.Dtos.Comun;

namespace Paqueteria.Comun.Dtos.Empresas;

public record EmpresaCrearDto
(
    string Nombre,
    string NombreCorto,
    string Rfc,
    DireccionDto Direccion
);