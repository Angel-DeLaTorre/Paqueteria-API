using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IUsuarioService
{
    Task<Result<IReadOnlyList<UsuarioResponseDto>>> GetAllAsync();
    Task<Result<UsuarioResponseDto>> GetByIdAsync(Guid usuarioId);
    Task<Result<UsuarioResponseDto>> GetByUsername(string username);
    Task<Result<UsuarioResponseDto>> CreateAsync(UsuarioCreateDto dto, UserContext currentUser);
    Task<Result> UpdateAsync(UsuarioUpdateDto dto, UserContext currentUser);
    Task<Result> DeleteAsync(Guid usuarioId, UserContext currentUser);
}