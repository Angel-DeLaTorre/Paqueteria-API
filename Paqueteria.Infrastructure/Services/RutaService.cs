using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;

namespace Paqueteria.Infrastructure.Services;

public class RutaService(IRutaRepository rutaRepository) : IRutaService
{
    public Task<Result<IReadOnlyList<RutaResponseDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<RutaResponseDto>> GetByIdAsync(Guid rutaId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<RutaResponseDto>> CreateAsync(RutaCreateDto dto, UserContext currentUser)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateAsync(RutaUpdateDto dto, UserContext currentUser)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(Guid rutaId, UserContext currentUser)
    {
        throw new NotImplementedException();
    }
}