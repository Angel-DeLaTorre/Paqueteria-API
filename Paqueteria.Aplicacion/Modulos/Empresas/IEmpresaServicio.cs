using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Modulos.Empresas;

public interface IEmpresaServicio
{
    Task<Resultado<IReadOnlyList<EmpresaResponseDto>>> ObtenerTodosAsync();
    Task<Resultado<EmpresaResponseDto>> ObtenerPorIdAsync();
    Task<Resultado<EmpresaResponseDto>> AgregarAsync(EmpresaCreateDto dto);
    Task<Resultado> ActualizarAsync(EmpresaUpdateDto dto);
    Task<Resultado> EliminarAsync(Guid empresaId);
}