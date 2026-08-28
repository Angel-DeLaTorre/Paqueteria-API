using Paqueteria.Application.Dtos;
using Paqueteria.Application.Modulos.Empresas.Dtos;
using Paqueteria.Core.Comun;

namespace Paqueteria.Application.Modulos.Empresas;

public interface IEmpresaServicio
{
    Task<Respuesta<IReadOnlyList<EmpresaResponseDto>>> ObtenerTodosAsync();
    Task<Respuesta<EmpresaResponseDto>> ObtenerPorIdAsync();
    Task<Respuesta<EmpresaResponseDto>> AgregarAsync(EmpresaCreateDto dto);
    Task<Respuesta> ActualizarAsync(EmpresaUpdateDto dto);
    Task<Respuesta> EliminarAsync(Guid empresaId);
}