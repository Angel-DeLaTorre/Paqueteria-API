using Paqueteria.Aplicacion.Comun;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Empresas;

namespace Paqueteria.Aplicacion.Modulos.Empresas;

public interface IEmpresaServicio
{
    Task<Respuesta<IReadOnlyList<EmpresaRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<EmpresaRespuestaDto>> ObtenerPorIdAsync();
    Task<Respuesta<EmpresaRespuestaDto>> AgregarAsync(EmpresaCrearDto dto);
    Task<Respuesta> ActualizarAsync(EmpresaActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid empresaId);
}