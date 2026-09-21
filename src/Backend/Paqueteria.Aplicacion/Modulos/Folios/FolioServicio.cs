using Paqueteria.Aplicacion.Comun.Interfaces;
using Paqueteria.Aplicacion.Interfaces.Persistence;
using Paqueteria.Comun.Enums;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Aplicacion.Modulos.Folios;

public class FolioServicio (
    IUnitOfWork unit, 
    IUsuarioContextoServicio contextoUsuario
) : IFolioServicio
{
    public async Task<string> GenerarSiguienteFolioAsync(Guid sucursalId, TipoFolio tipo)
    {
        var sucursal = await unit.Sucursales.ObtenerPorIdAsync(sucursalId, contextoUsuario.EmpresaId);
        if (sucursal == null) 
            throw new Exception("La sucursal no existe.");

        var controlFolio = await unit.FoliosSucursal.ObtenerConBloqueoAsync(sucursalId, tipo);
        int siguienteNumero;

        if (controlFolio == null)
        {
            siguienteNumero = 1;
            controlFolio = FolioSucursal.Crear(sucursalId, tipo, siguienteNumero);
            await unit.FoliosSucursal.AgregarAsync(controlFolio);
        }
        else
        {
            controlFolio.UltimoConsecutivo += 1;
            siguienteNumero = controlFolio.UltimoConsecutivo;
        }

        return tipo switch
        {
            TipoFolio.Asignacion => $"ASIG-{sucursal.Codigo}-{siguienteNumero:D6}",
            TipoFolio.Guia => $"{sucursal.Codigo}-{siguienteNumero:D6}",
            _ => $"{siguienteNumero:D6}"
        };
    }
}