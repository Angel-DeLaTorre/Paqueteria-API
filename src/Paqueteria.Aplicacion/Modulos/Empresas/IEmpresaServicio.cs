using Paqueteria.Application.Modulos.Empresas.Dtos;
using Paqueteria.Dominio.Comun;

namespace Paqueteria.Application.Modulos.Empresas;

public interface IEmpresaServicio
{
    Task<Respuesta<IReadOnlyList<EmpresaRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<EmpresaRespuestaDto>> ObtenerPorIdAsync();
    Task<Respuesta<EmpresaRespuestaDto>> AgregarAsync(EmpresaCrearDto dto);
    Task<Respuesta> ActualizarAsync(EmpresaActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid empresaId);
}