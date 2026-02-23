using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IUsuarioService
{
    Task<Result<List<UsuarioResponseDto>>> GetAll();
    Task<Result<UsuarioResponseDto>> GetById(string username);
    Task<Result<UsuarioResponseDto>> Create(UsuarioCreateDto dto);
    Task<Result> Update(UsuarioUpdateDto dto);
    Task<Result> Delete(Guid id);
}