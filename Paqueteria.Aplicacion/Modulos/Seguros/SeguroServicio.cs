using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Dtos;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Seguros.Dtos;
using Paqueteria.Core.Comun;
using Paqueteria.Core.Comun.Errors;

namespace Paqueteria.Application.Modulos.Seguros;

public class SeguroServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : ISeguroServicio
{
    public async Task<Respuesta<IReadOnlyList<SeguroResponseDto>>> ObtenerTodosAsync()
    {
        var seguros = ( await unit.Seguros.ObtenerTodosAsync(contextoUsuario.EmpresaId, false) )
            .Select( SeguroResponseDto.FromEntity ).ToList();
        return Respuesta<IReadOnlyList<SeguroResponseDto>>.Exitoso(seguros);
    }

    public async Task<Respuesta<SeguroResponseDto>> ObtenerPorIdAsync(Guid seguroId)
    {
        var seguro = await unit.Seguros.ObtenerPorIdAsync(seguroId, contextoUsuario.EmpresaId, false);

        if (seguro is null)
            return Respuesta<SeguroResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        return Respuesta<SeguroResponseDto>.Exitoso(SeguroResponseDto.FromEntity(seguro));
    }

    public async Task<Respuesta<SeguroResponseDto>> AgregarAsync(SeguroCreateDto dto)
    {
        var seguro = await unit.Seguros.AgregarAsync(dto.ToEntity(contextoUsuario.EmpresaId));

        var result = unit.GuardarCambiosAsync();

        if (result.IsCompletedSuccessfully)
            return Respuesta<SeguroResponseDto>.Error(CodigosError.Generic.NoCreado);

        return Respuesta<SeguroResponseDto>.Exitoso(SeguroResponseDto.FromEntity(seguro));
    }

    public async Task<Respuesta> ActualizarAsync(SeguroUpdateDto dto)
    {
        var seguro = await unit.Seguros.ObtenerPorIdAsync(dto.SeguroId, contextoUsuario.EmpresaId);

        if (seguro is null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        dto.UpdateEntity(seguro);
        var result = await  unit.GuardarCambiosAsync();

        if (result != 0)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        return Respuesta.Exitoso();
    }

    public async Task<Respuesta> EliminarAsync(Guid seguroId)
    {
        throw new NotImplementedException();
    }
    
    public async Task<Respuesta> DesactivarAsync(Guid seguroId)
    {
        var seguro = await unit.Seguros.ObtenerPorIdAsync(seguroId, contextoUsuario.EmpresaId);
        if (seguro == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);
        
        seguro.Desactivar();
        await  unit.GuardarCambiosAsync();
        
        return Respuesta.Exitoso();
    }
}