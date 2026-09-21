using Paqueteria.Comun.Enums;

namespace Paqueteria.Dominio.Dto;

public record GuiaFiltroDto
{
    public string? Clave { get; init; }
    public EstatusGuia? Estatus { get; init; }
    public FormaPago? FormaPago { get; init; }
    
    public Guid? SucursalOrigenId { get; init; }
    public Guid? SucursalDestinoId { get; init; }
    public Guid? ClienteOrigenId { get; init; }
    
    public bool? EstaAsignado { get; init; }
    
    public DateTime? FechaInicio { get; init; }
    
    public bool? EstaAsegurado { get; init; }

    // Paginación por defecto
    public int Pagina { get; init; } = 1;
    public int TamanoPagina { get; init; } = 20;
}