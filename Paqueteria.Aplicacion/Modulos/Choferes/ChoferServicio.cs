using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;
using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Application.Modulos.Choferes;

public class ChoferServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : IChoferServicio
{
    public async Task<Resultado<IReadOnlyList<ChoferResponseDto>>> ObtenerTodosAsync()
    {

        var choferes = ( await unit.Choferes.ObtenerTodosAsync(contextoUsuario.EmpresaId) )
            .Select( ChoferResponseDto.FromEntity ).ToList();
        return Resultado<IReadOnlyList<ChoferResponseDto>>.Exitoso(choferes);
        
    }

    public async Task<Resultado<ChoferResponseDto>> ObtenerPorIdAsync(Guid choferId)
    {

        var chofer = await unit.Choferes.ObtenerPorIdAsync(choferId, contextoUsuario.EmpresaId);

        if (chofer is null)
            return Resultado<ChoferResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        return Resultado<ChoferResponseDto>.Exitoso(ChoferResponseDto.FromEntity(chofer));
    }

    public async Task<Resultado<ChoferResponseDto>> AgregarAsync(ChoferCreateDto dto)
    {
        var direccion = dto.Direccion.ToEntity();
        
        var chofer = Chofer.Crear(
            dto.Nombre,
            dto.ApellidoPaterno,
            dto.ApellidoMaterno,
            direccion,
            dto.Telefono,
            dto.NumCamion,
            dto.NumContenedor,
            dto.NumContenedor2,
            contextoUsuario.EmpresaId
        );
        
        await unit.Choferes.AgregarAsync(chofer);

        await unit.CompletarAsync();

        return Resultado<ChoferResponseDto>.Exitoso(ChoferResponseDto.FromEntity(chofer));
    }

    public async Task<Resultado> ActualizarAsync(ChoferUpdateDto dto)
    {

        var chofer = await unit.Choferes.ObtenerPorIdAsync(dto.ChoferId,  contextoUsuario.EmpresaId);

        if (chofer is null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);

        var direccion = dto.Direccion.ToEntity();
    
        chofer.ActualizarDatosPersonales
        (
            dto.Nombre,
            dto.ApellidoPaterno,
            dto.ApellidoMaterno,
            dto.Telefono,
            direccion
        );
        
        chofer.AsignarCamion( dto.NumCamion, dto.NumContenedor, dto.NumContenedor2 );
        
        await unit.CompletarAsync();

        return Resultado.Exitoso();
    }

    public async Task<Resultado> EliminarAsync(Guid choferId)
    {
        var chofer = (await unit.Choferes.ObtenerPorIdAsync(choferId,  contextoUsuario.EmpresaId));

        if (chofer is null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);

        unit.Choferes.Eliminar(chofer);

        var result = await unit.CompletarAsync();
        if (result <= 0)
            return Resultado.Error(CodigosError.Generic.NoEliminado);

        return Resultado.Exitoso();
    }
    
    public async Task<Resultado> DesactivarAsync(Guid choferId)
    {
        var chofer = await unit.Choferes.ObtenerPorIdAsync(choferId, contextoUsuario.EmpresaId);
        if (chofer == null) return Resultado.Error(CodigosError.Generic.NoEncontrado);
        
        chofer.Desactivar();
        await unit.CompletarAsync();
        
        return Resultado.Exitoso();
    }
    
    public async Task<Resultado> ActivarAsync(Guid choferId)
    {
        var chofer = await unit.Choferes.ObtenerPorIdAsync(choferId, contextoUsuario.EmpresaId);
        if (chofer == null) return Resultado.Error(CodigosError.Generic.NoEncontrado);
        
        chofer.Activar();
        await unit.CompletarAsync();
        
        return Resultado.Exitoso();
    }
}