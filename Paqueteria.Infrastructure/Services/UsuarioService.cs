using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class UsuarioService(IUnitOfWork unit, IUserContextService userContext) : IUsuarioService
{
    public async Task<Result<IReadOnlyList<UsuarioResponseDto>>> GetAllAsync()
    {
        var usuarios = (await unit.Usuarios.GetAllAsync(userContext.EmpresaId))
            .Select( UsuarioResponseDto.FromEntity ).ToList();
        return Result<IReadOnlyList<UsuarioResponseDto>>.Success(usuarios);
    }

    public Task<Result<UsuarioResponseDto>> GetByIdAsync(Guid usuarioId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<UsuarioResponseDto>> GetByUsername(string username)
    {
        var usuario = await unit.Usuarios.GetByUsernameAsync(username);

        if (usuario == null)
            return Result<UsuarioResponseDto>.Failure(ErrorCodes.Generic.NoEncontrado);

        return Result<UsuarioResponseDto>.Success(UsuarioResponseDto.FromEntity(usuario));
    }

    public async Task<Result<UsuarioResponseDto>> CreateAsync(UsuarioCreateDto dto)
    {
        var usuario = dto.ToEntity(
            AuthService.HashPassword(dto.Password),
            userContext.EmpresaId
        );
        
        await unit.Usuarios.AddAsync(usuario);

        foreach (var rolId in dto.Roles)
        {
            await unit.Usuarios.AddRoleToUserAsync(usuario.Id, rolId, userContext.EmpresaId);
        }
        
        await unit.CompleteAsync();

        return Result<UsuarioResponseDto>.Success(UsuarioResponseDto.FromEntity(usuario));
    }

    public async Task<Result> UpdateAsync(UsuarioUpdateDto dto)
    {
        var usuario = (await unit.Usuarios.GetByIdAsync(dto.UsuarioId, userContext.EmpresaId));

        if (usuario == null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        dto.UpdateEntity(usuario);
        await unit.CompleteAsync();
        
        return Result.Success();
    }
    
    public async Task<Result> DesactivarAsync(Guid usuarioId)
    {
        var usuario = await unit.Usuarios.GetByIdAsync(usuarioId, userContext.EmpresaId);
        if (usuario == null) return Result.Failure(ErrorCodes.Generic.NoEncontrado);
        
        usuario.Estatus = EstatusBasico.Inactivo;
        await  unit.CompleteAsync();
        
        return Result.Success();
    }
    
    public async Task<Result> ActivarAsync(Guid usuarioId)
    {
        var usuario = await unit.Usuarios.GetByIdAsync(usuarioId, userContext.EmpresaId);
        if (usuario == null) return Result.Failure(ErrorCodes.Generic.NoEncontrado);
        
        usuario.Estatus = EstatusBasico.Activo;
        await  unit.CompleteAsync();
        
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid usuarioId)
    {
        var usuario = (await unit.Usuarios.GetByIdAsync(usuarioId, userContext.EmpresaId));

        if (usuario == null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        unit.Usuarios.Delete(usuario);
        await unit.CompleteAsync();

        return Result.Success();
    }
}