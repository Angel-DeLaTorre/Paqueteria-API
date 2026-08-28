using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Dtos;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Empresas.Dtos;
using Paqueteria.Core.Comun;
using Paqueteria.Core.Comun.Errors;

namespace Paqueteria.Application.Modulos.Empresas;

public sealed class EmpresaServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : IEmpresaServicio
{
    public async Task<Respuesta<IReadOnlyList<EmpresaResponseDto>>> ObtenerTodosAsync()
    {

        var empresas =  ( await unit.Empresas.ObtenerTodosAsync(false) )
            .Select( EmpresaResponseDto.FromEntity ).ToList();

        return Respuesta<IReadOnlyList<EmpresaResponseDto>>.Exitoso(empresas);
    }

    public async Task<Respuesta<EmpresaResponseDto>> ObtenerPorIdAsync()
    {
        var empresa = await unit.Empresas.ObtenerPorIdAsync(contextoUsuario.EmpresaId);

        if (empresa is null)
            return Respuesta<EmpresaResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        return Respuesta<EmpresaResponseDto>.Exitoso(EmpresaResponseDto.FromEntity(empresa));
    }

    public async Task<Respuesta<EmpresaResponseDto>> AgregarAsync(EmpresaCreateDto dto)
    {
        var empresa = await unit.Empresas.AgregarAsync(dto.ToEntity());

        var result = await unit.GuardarCambiosAsync();

        if (result <= 0)
            return Respuesta<EmpresaResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        return Respuesta<EmpresaResponseDto>.Exitoso(EmpresaResponseDto.FromEntity(empresa));
    }

    public async Task<Respuesta> ActualizarAsync(EmpresaUpdateDto dto)
    {
        var empresa = (await unit.Empresas.ObtenerPorIdAsync(dto.EmpresaId));

        if (empresa is null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        dto.UpdateEntity(empresa);
        await unit.GuardarCambiosAsync();

        return Respuesta.Exitoso();
    }

    public async Task<Respuesta> EliminarAsync(Guid empresaId)
    {
        var empresa = (await unit.Empresas.ObtenerPorIdAsync(empresaId));

        if ( empresa is null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        unit.Empresas.Eliminar(empresa);

        var result = await unit.GuardarCambiosAsync();
        if (result <= 0)
            return Respuesta.Error(CodigosError.Generic.NoEliminado);

        return Respuesta.Exitoso();
    }
}