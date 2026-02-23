using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
{
    public async Task<Result<List<UsuarioResponseDto>>> GetAll()
    {
        var usuarios = (await usuarioRepository.GetAllAsync())
            .Select( UsuarioResponseDto.FromEntity ).ToList();
        return Result<List<UsuarioResponseDto>>.Success(usuarios);
    }

    public async Task<Result<UsuarioResponseDto>> GetById(string username)
    {
        var usuario = await usuarioRepository.GetByUsernameAsync(username);

        if (usuario == null)
            return Result<UsuarioResponseDto>.Failure(CodigoRespuesta.NotFound, "Usuario no encontrado");

        return Result<UsuarioResponseDto>.Success(UsuarioResponseDto.FromEntity(usuario));
    }

    public async Task<Result<UsuarioResponseDto>> Create(UsuarioCreateDto dto)
    {
        var usuario = await usuarioRepository.AddAsync(dto.ToEntity());

        if (usuario == null)
            return Result<UsuarioResponseDto>.Failure(CodigoRespuesta.Failure, "Error al insertar");

        return Result<UsuarioResponseDto>.Success(UsuarioResponseDto.FromEntity(usuario));
    }

    public async Task<Result> Update(UsuarioUpdateDto dto)
    {
        var usuario = (await usuarioRepository.GetByIdAsync(dto.UsuarioId));

        if (usuario == null)
            return Result.Failure(CodigoRespuesta.NotFound, "Usuario no encontrado");

        dto.UpdateEntity(usuario);
        usuarioRepository.Update(usuario);

        return Result.Success();

    }

    public async Task<Result> Delete(Guid id)
    {
        var usuario = (await usuarioRepository.GetByIdAsync(id));

        if (usuario == null)
            return Result.Failure(CodigoRespuesta.NotFound, "Usuario no encontrado");

        usuarioRepository.Delete(usuario);

        return Result.Success();
    }
}