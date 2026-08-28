using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Dtos;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Choferes.Dtos;
using Paqueteria.Core.Comun;
using Paqueteria.Core.Comun.Errors;
using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Choferes;

public class ChoferServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : IChoferServicio
{
    public async Task<Respuesta<IReadOnlyList<ChoferResponseDto>>> ObtenerTodosAsync()
    {

        var choferes = ( await unit.Choferes.ObtenerTodosAsync(contextoUsuario.EmpresaId) )
            .Select( ChoferResponseDto.FromEntity ).ToList();
        return Respuesta<IReadOnlyList<ChoferResponseDto>>.Exitoso(choferes);
        
    }

    public async Task<Respuesta<ChoferResponseDto>> ObtenerPorIdAsync(Guid choferId)
    {

        var chofer = await unit.Choferes.ObtenerPorIdAsync(choferId, contextoUsuario.EmpresaId);

        if (chofer is null)
            return Respuesta<ChoferResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        return Respuesta<ChoferResponseDto>.Exitoso(ChoferResponseDto.FromEntity(chofer));
    }

    public async Task<Respuesta<ChoferResponseDto>> AgregarAsync(ChoferCreateDto dto)
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

        await unit.GuardarCambiosAsync();

        return Respuesta<ChoferResponseDto>.Exitoso(ChoferResponseDto.FromEntity(chofer));
    }

    public async Task<Respuesta> ActualizarAsync(ChoferUpdateDto dto)
    {

        var chofer = await unit.Choferes.ObtenerPorIdAsync(dto.ChoferId,  contextoUsuario.EmpresaId);

        if (chofer is null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);

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
        
        await unit.GuardarCambiosAsync();

        return Respuesta.Exitoso();
    }

    public async Task<Respuesta> EliminarAsync(Guid choferId)
    {
        var chofer = (await unit.Choferes.ObtenerPorIdAsync(choferId,  contextoUsuario.EmpresaId));

        if (chofer is null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        unit.Choferes.Eliminar(chofer);

        var result = await unit.GuardarCambiosAsync();
        if (result <= 0)
            return Respuesta.Error(CodigosError.Generic.NoEliminado);

        return Respuesta.Exitoso();
    }
    
    public async Task<Respuesta> DesactivarAsync(Guid choferId)
    {
        var chofer = await unit.Choferes.ObtenerPorIdAsync(choferId, contextoUsuario.EmpresaId);
        if (chofer == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);
        
        chofer.Desactivar();
        await unit.GuardarCambiosAsync();
        
        return Respuesta.Exitoso();
    }
    
    public async Task<Respuesta> ActivarAsync(Guid choferId)
    {
        var chofer = await unit.Choferes.ObtenerPorIdAsync(choferId, contextoUsuario.EmpresaId);
        if (chofer == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);
        
        chofer.Activar();
        await unit.GuardarCambiosAsync();
        
        return Respuesta.Exitoso();
    }
}