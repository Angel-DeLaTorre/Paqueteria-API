using Paqueteria.Application.Comun.Mapeador;
using Paqueteria.Application.Modulos.Sucursales.Dtos;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Sucursales;

public static class SucursalMapeador
{
    extension(Sucursal sucursal)
    {
        public SucursalRespuestaDto MapeaRespuestaDto()
        {
            return new SucursalRespuestaDto
            (
                SucursalId: sucursal.Id,
                Nombre: sucursal.Nombre,
                Codigo: sucursal.Codigo,
                EsMatriz: sucursal.EsMatriz,
                Direccion: sucursal.Direccion.MapeaRespuestaDto(),
                Telefono: sucursal.Telefono,
                Estatus: sucursal.Estatus
            );
        }
    }
}