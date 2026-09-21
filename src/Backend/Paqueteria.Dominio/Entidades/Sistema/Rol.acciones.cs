using Paqueteria.Comun.Enums;

namespace Paqueteria.Dominio.Entidades.Sistema;

public partial class Rol
{
    public void ActualizarDatos(string nombre, string descripcion)
    {
        Nombre = nombre;
        Descripcion = descripcion;
    }
    
    public void Desactivar()
    {
        if (Estatus.Equals(EstatusBasico.Inactivo)) 
            throw new InvalidOperationException("Ruta ya está de baja.");

        Estatus = EstatusBasico.Inactivo;
    }
    
    public void Activar()
    {
        if (Estatus.Equals(EstatusBasico.Activo)) 
            throw new InvalidOperationException("Ruta ya está activo.");

        Estatus = EstatusBasico.Activo;
    }

    public void AgregarPermiso(Guid permisoId)
    {
        if (Permisos.Any( rp => rp.PermisoId == permisoId )) return;
        var permiso = RolPermiso.Crear( Id, permisoId );
        Permisos.Add(permiso);
    }
    
    public void LimpiarPermisos()
    {
        Permisos.Clear();
    }
}