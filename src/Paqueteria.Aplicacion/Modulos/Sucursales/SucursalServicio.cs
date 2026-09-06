using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Comun.Mapeador;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Sucursales.Dtos;
using Paqueteria.Dominio.Comun;
using Paqueteria.Dominio.Comun.Errors;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Sucursales;

public class SucursalServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : ISucursalServicio
{
    public async Task<Respuesta<IReadOnlyList<SucursalRespuestaDto>>> ObtenerTodosAsync()
    {
        var sucursales =  ( await unit.Sucursales.ObtenerTodosAsync(contextoUsuario.EmpresaId) )
            .Select( s => s.MapeaRespuestaDto() ).ToList();

        return Respuesta<IReadOnlyList<SucursalRespuestaDto>>.Exitoso(sucursales);
    }

    public async Task<Respuesta<SucursalRespuestaDto>> ObtenerPorIdAsync(Guid sucursalId)
    {
        var sucursal = await unit.Sucursales.ObtenerPorIdAsync(sucursalId, contextoUsuario.EmpresaId);
        if (sucursal == null)
            return Respuesta<SucursalRespuestaDto>.Error(CodigosError.Comun.NoEncontrado);

        return Respuesta<SucursalRespuestaDto>.Exitoso( sucursal.MapeaRespuestaDto() );
    }

    public async Task<Respuesta<SucursalRespuestaDto>> AgregarAsync(SucursalCrearDto dto)
    {
        var direccion = dto.Direccion.MapeaEntidad();
        
        var sucursal = Sucursal.Crear
        (
            dto.Nombre,
            dto.Codigo,
            dto.EsMatriz,
            direccion,
            dto.Telefono,
            contextoUsuario.EmpresaId
        );
        
        await unit.Sucursales.AgregarAsync( sucursal );

        var result = await unit.GuardarCambiosAsync();

        if (result <= 0)
            return Respuesta<SucursalRespuestaDto>.Error(CodigosError.Comun.NoCreado);

        return Respuesta<SucursalRespuestaDto>.Exitoso( sucursal.MapeaRespuestaDto() );
    }

    public async Task<Respuesta> ActualizarAsync(SucursalActualizarDto dto)
    {
        var sucursal = await unit.Sucursales.ObtenerPorIdAsync(dto.SucursalId, contextoUsuario.EmpresaId);

        if (sucursal == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        sucursal.ActualizarDatos(dto.Nombre, dto.EsMatriz, dto.Telefono);
        sucursal.ActualizarDireccion(dto.Direccion.MapeaEntidad());
        
        var result = await  unit.GuardarCambiosAsync();

        if (result != 0)
            return Respuesta.Error(CodigosError.Comun.NoActualizado);

        return Respuesta.Exitoso();
    }
    
    public async Task<Respuesta> DesactivarAsync(Guid sucursalId)
    {
        var sucursal = await unit.Sucursales.ObtenerPorIdAsync(sucursalId, contextoUsuario.EmpresaId);
        if (sucursal == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);
        
        sucursal.Desactivar();
        await  unit.GuardarCambiosAsync();
        
        return Respuesta.Exitoso();
    }
    
    public async Task<Respuesta> ActivarAsync(Guid sucursalId)
    {
        var sucursal = await unit.Sucursales.ObtenerPorIdAsync(sucursalId, contextoUsuario.EmpresaId);
        if (sucursal == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);
        
        sucursal.Activar();
        await  unit.GuardarCambiosAsync();
        
        return Respuesta.Exitoso();
    }

    public async Task<Respuesta> EliminarAsync(Guid idSucursal)
    {
        var sucursal = await unit.Sucursales.ObtenerPorIdAsync(idSucursal, contextoUsuario.EmpresaId);

        if (sucursal == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        unit.Sucursales.Eliminar(sucursal);
        var result = await  unit.GuardarCambiosAsync();

        if (result != 0)
            return Respuesta.Error(CodigosError.Comun.NoActualizado);

        return Respuesta.Exitoso();
    }
}