using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;

namespace Paqueteria.Infrastructure.Services;

public class AsignacionService (IAsignacionRepository asignacionRepository) : IAsignacionSerivce
{
    public Task<Result<IReadOnlyList<AsignacionResponseDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<AsignacionResponseDto>> GetByIdAsync(Guid asignacionId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<AsignacionResponseDto>> CreateAsync(AsignacionCreateDto dto, UserContext currentUser)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateAsync(AsignacionUpdateDto dto, UserContext currentUser)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(Guid asignacionId, UserContext currentUser)
    {
        throw new NotImplementedException();
    }
}