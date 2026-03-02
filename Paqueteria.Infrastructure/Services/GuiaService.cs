using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;

namespace Paqueteria.Infrastructure.Services;

public class GuiaService(IGuiaRepository guiaRepository) : IGuiaService
{
    public Task<Result<IReadOnlyList<GuiaResponseDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<GuiaResponseDto>> GetByIdAsync(Guid guiaId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<GuiaResponseDto>> CreateAsync(GuiaCreateDto dto, UserContext currentUser)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateAsync(GuiaCreateDto dto, UserContext currentUser)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(Guid guiaId, UserContext currentUser)
    {
        throw new NotImplementedException();
    }
}