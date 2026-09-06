using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Dominio.Interfaces.Repositorios;

public interface IAsignacionRepositorio
{
    public Task<Asignacion?> ObtenerPorIdAsync(Guid asignacionId, Guid empresaId);
    public Task<IEnumerable<Asignacion>> ObtenerTodosAsync(Guid empresaId);
    public Task<Asignacion> AgregarAsync(Asignacion entity);
    public void Eliminar(Asignacion entity);
    
    Task<IEnumerable<Asignacion>> ObtenerParaReporteSalidasAsync(
        Guid empresaId, 
        Guid? sucursalOrigenId, 
        DateTime fechaInicio, 
        DateTime fechaFin);
}