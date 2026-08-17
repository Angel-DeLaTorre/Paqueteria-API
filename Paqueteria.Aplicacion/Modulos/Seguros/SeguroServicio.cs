using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;

namespace Paqueteria.Application.Modulos.Seguros;

public class SeguroServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : ISeguroServicio
{
    public async Task<Resultado<IReadOnlyList<SeguroResponseDto>>> ObtenerTodosAsync()
    {
        var seguros = ( await unit.Seguros.ObtenerTodosAsync(contextoUsuario.EmpresaId, false) )
            .Select( SeguroResponseDto.FromEntity ).ToList();
        return Resultado<IReadOnlyList<SeguroResponseDto>>.Exitoso(seguros);
    }

    public async Task<Resultado<SeguroResponseDto>> ObtenerPorIdAsync(Guid seguroId)
    {
        var seguro = await unit.Seguros.ObtenerPorIdAsync(seguroId, contextoUsuario.EmpresaId, false);

        if (seguro is null)
            return Resultado<SeguroResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        return Resultado<SeguroResponseDto>.Exitoso(SeguroResponseDto.FromEntity(seguro));
    }

    public async Task<Resultado<SeguroResponseDto>> AgregarAsync(SeguroCreateDto dto)
    {
        var seguro = await unit.Seguros.AgregarAsync(dto.ToEntity(contextoUsuario.EmpresaId));

        var result = unit.CompletarAsync();

        if (result.IsCompletedSuccessfully)
            return Resultado<SeguroResponseDto>.Error(CodigosError.Generic.NoCreado);

        return Resultado<SeguroResponseDto>.Exitoso(SeguroResponseDto.FromEntity(seguro));
    }

    public async Task<Resultado> ActualizarAsync(SeguroUpdateDto dto)
    {
        var seguro = await unit.Seguros.ObtenerPorIdAsync(dto.SeguroId, contextoUsuario.EmpresaId);

        if (seguro is null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);

        dto.UpdateEntity(seguro);
        var result = await  unit.CompletarAsync();

        if (result != 0)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);

        return Resultado.Exitoso();
    }

    public async Task<Resultado> EliminarAsync(Guid seguroId)
    {
        throw new NotImplementedException();
    }
    
    public async Task<Resultado> DesactivarAsync(Guid seguroId)
    {
        var seguro = await unit.Seguros.ObtenerPorIdAsync(seguroId, contextoUsuario.EmpresaId);
        if (seguro == null) return Resultado.Error(CodigosError.Generic.NoEncontrado);
        
        seguro.Desactivar();
        await  unit.CompletarAsync();
        
        return Resultado.Exitoso();
    }
}