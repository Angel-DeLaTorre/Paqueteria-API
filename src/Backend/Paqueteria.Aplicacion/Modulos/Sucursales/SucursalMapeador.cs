using Paqueteria.Aplicacion.Comun.Mapeador;
using Paqueteria.Comun.Dtos.Sucursales;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Aplicacion.Modulos.Sucursales;

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