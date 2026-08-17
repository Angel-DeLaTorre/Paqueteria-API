using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;

namespace Paqueteria.Application.Modulos.Empresas;

public sealed class EmpresaServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : IEmpresaServicio
{
    public async Task<Resultado<IReadOnlyList<EmpresaResponseDto>>> ObtenerTodosAsync()
    {

        var empresas =  ( await unit.Empresas.ObtenerTodosAsync(false) )
            .Select( EmpresaResponseDto.FromEntity ).ToList();

        return Resultado<IReadOnlyList<EmpresaResponseDto>>.Exitoso(empresas);
    }

    public async Task<Resultado<EmpresaResponseDto>> ObtenerPorIdAsync()
    {
        var empresa = await unit.Empresas.ObtenerPorIdAsync(contextoUsuario.EmpresaId);

        if (empresa is null)
            return Resultado<EmpresaResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        return Resultado<EmpresaResponseDto>.Exitoso(EmpresaResponseDto.FromEntity(empresa));
    }

    public async Task<Resultado<EmpresaResponseDto>> AgregarAsync(EmpresaCreateDto dto)
    {
        var empresa = await unit.Empresas.AgregarAsync(dto.ToEntity());

        var result = await unit.CompletarAsync();

        if (result <= 0)
            return Resultado<EmpresaResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        return Resultado<EmpresaResponseDto>.Exitoso(EmpresaResponseDto.FromEntity(empresa));
    }

    public async Task<Resultado> ActualizarAsync(EmpresaUpdateDto dto)
    {
        var empresa = (await unit.Empresas.ObtenerPorIdAsync(dto.EmpresaId));

        if (empresa is null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);

        dto.UpdateEntity(empresa);
        await unit.CompletarAsync();

        return Resultado.Exitoso();
    }

    public async Task<Resultado> EliminarAsync(Guid empresaId)
    {
        var empresa = (await unit.Empresas.ObtenerPorIdAsync(empresaId));

        if ( empresa is null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);

        unit.Empresas.Eliminar(empresa);

        var result = await unit.CompletarAsync();
        if (result <= 0)
            return Resultado.Error(CodigosError.Generic.NoEliminado);

        return Resultado.Exitoso();
    }
}