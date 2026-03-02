using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;

namespace Paqueteria.Infrastructure.Services;

public class EmpresaService(IEmpresaRepository empresaRepository) : IEmpresaService
{
    public Task<Result<IReadOnlyList<EmpresaResponseDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<EmpresaResponseDto>> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<EmpresaResponseDto>> CreateAsync(EmpresaCreateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateAsync(EmpresaCreateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(Guid empresaId)
    {
        throw new NotImplementedException();
    }
}