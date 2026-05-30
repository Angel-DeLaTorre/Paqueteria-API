using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class UsuarioService(IUnitOfWork unit) : IUsuarioService
{
    public async Task<Result<IReadOnlyList<UsuarioResponseDto>>> GetAllAsync(UserContext currentUser)
    {
        try
        {
            var usuarios = (await unit.Usuarios.GetAllAsync(currentUser.EmpresaId))
                .Select( UsuarioResponseDto.FromEntity ).ToList();
            return Result<IReadOnlyList<UsuarioResponseDto>>.Success(usuarios);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<Result<UsuarioResponseDto>> GetByIdAsync(Guid usuarioId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<UsuarioResponseDto>> GetByUsername(string username)
    {
        try
        {
            var usuario = await unit.Usuarios.GetByUsernameAsync(username);

            if (usuario == null)
                return Result<UsuarioResponseDto>.Failure(Errors.Generic.NoEncontrado);

            return Result<UsuarioResponseDto>.Success(UsuarioResponseDto.FromEntity(usuario));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<UsuarioResponseDto>> CreateAsync(UsuarioCreateDto dto, UserContext currentUser)
    {
        try
        {
            var usuario = dto.ToEntity(
                AuthService.HashPassword(dto.Password),
                currentUser.EmpresaId
            );
            
            await unit.Usuarios.AddAsync(usuario);

            foreach (var rolId in dto.Roles)
            {
                await unit.Usuarios.AddRoleToUserAsync(usuario.Id, rolId, currentUser.EmpresaId);
            }
            
            await unit.CompleteAsync();

            return Result<UsuarioResponseDto>.Success(UsuarioResponseDto.FromEntity(usuario));
        }
        catch (Exception e)
        {
            await unit.RollbackTransactionAsync();
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result> UpdateAsync(UsuarioUpdateDto dto, UserContext currentUser)
    {
        try
        {
            var usuario = (await unit.Usuarios.GetByIdAsync(dto.UsuarioId, currentUser.EmpresaId));

            if (usuario == null)
                return Result.Failure(Errors.Generic.NoEncontrado);

            dto.UpdateEntity(usuario);
            await unit.CompleteAsync();
            
            return Result.Success();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result> DeleteAsync(Guid usuarioId, UserContext currentUser)
    {
        try
        {
            var usuario = (await unit.Usuarios.GetByIdAsync(usuarioId, currentUser.EmpresaId));

            if (usuario == null)
                return Result.Failure(Errors.Generic.NoEncontrado);

            unit.Usuarios.Delete(usuario);
            await unit.CompleteAsync();

            return Result.Success();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}