using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class UsuarioService(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork) : IUsuarioService
{
    public async Task<Result<IReadOnlyList<UsuarioResponseDto>>> GetAllAsync()
    {
        try
        {
            var usuarios = (await usuarioRepository.GetAllAsync())
                .Select( UsuarioResponseDto.FromEntity ).ToList();
            return Result<IReadOnlyList<UsuarioResponseDto>>.Success(usuarios);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<UsuarioResponseDto>> GetByIdAsync(Guid usuarioId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<UsuarioResponseDto>> GetByUsername(string username)
    {
        try
        {
            var usuario = await usuarioRepository.GetByUsernameAsync(username);

            if (usuario == null)
                return Result<UsuarioResponseDto>.Failure(CodigoRespuesta.Failure, "Usuario no encontrado");

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
            var usuario = await usuarioRepository.AddAsync(dto.ToEntity(currentUser.EmpresaId));

            var result = await unitOfWork.CompleteAsync();

            if (result <= 0)
                return Result<UsuarioResponseDto>.Failure(CodigoRespuesta.NotFound, "Usuario no creado");

            return Result<UsuarioResponseDto>.Success(UsuarioResponseDto.FromEntity(usuario));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result> UpdateAsync(UsuarioUpdateDto dto, UserContext currentUser)
    {
        try
        {
            var usuario = (await usuarioRepository.GetByIdAsync(dto.UsuarioId));

            if (usuario == null)
                return Result.Failure(CodigoRespuesta.NotFound, "Usuario no encontrado");

            dto.UpdateEntity(usuario);
            usuarioRepository.Update(usuario);
            var result = await unitOfWork.CompleteAsync();
            if (result <= 0)
                return Result.Failure(CodigoRespuesta.Failure, "No se realizaron cambios");

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
            var usuario = (await usuarioRepository.GetByIdAsync(usuarioId));

            if (usuario == null)
                return Result.Failure(CodigoRespuesta.NotFound, "Usuario no encontrado");

            usuarioRepository.Delete(usuario);

            var result = await unitOfWork.CompleteAsync();
            if (result <= 0)
                return Result.Failure(CodigoRespuesta.Failure, "No se realizaron cambios");

            return Result.Success();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}