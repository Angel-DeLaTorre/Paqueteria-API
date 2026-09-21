namespace Paqueteria.Dominio.Entidades.Remisiones;

public partial class GuiaTransbordo
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    
    public Guid GuiaId { get; private init; }
    public Guid AsignacionId { get; private init; }
    public Guid SucursalTransbordoId { get; private init; }
    public Guid UsuarioRegistroId { get; private init; }

    public DateTime FechaEscaneoIngreso { get; private init; } = DateTime.UtcNow;
    public DateTime? FechaEscaneoSalida { get; private set; }
    public string? Observaciones { get; private set; }

    public Guia Guia { get; private set; } = null!;
    public Asignacion Asignacion { get; private set; } = null!;
    public Sucursal SucursalTransbordo { get; private set; } = null!;

    private GuiaTransbordo() { }

    public static GuiaTransbordo RegistrarIngreso(
        Guid guiaId, 
        Guid asignacionId, 
        Guid sucursalId, 
        Guid usuarioId, 
        string? observaciones = null)
    {
        return new GuiaTransbordo
        {
            GuiaId = guiaId,
            AsignacionId = asignacionId,
            SucursalTransbordoId = sucursalId,
            UsuarioRegistroId = usuarioId,
            FechaEscaneoIngreso = DateTime.UtcNow,
            Observaciones = observaciones
        };
    }
}