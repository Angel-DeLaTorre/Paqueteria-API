using Moq;
using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Usuarios;
using Paqueteria.Application.Modulos.Usuarios.Dtos;
using Paqueteria.Dominio.Entidades.Sistema;
using Paqueteria.Dominio.Interfaces.Repositorios;

namespace Paqueteria.Aplicacion.Tests.Modulos.Usuarios.Servicios;

public class UsuarioServicioPruebas
{
    [Fact]
    public async Task AgregarAsync_DebeCrearUsuarioExitosamente_CuandoLosDatosSonValidos()
    {
        // Preparar
        var usuarioRepositorioMock = new Mock<IUsuarioRepositorio>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var contextoUsuarioMock = new Mock<IUsuarioContextoServicio>();
        var hashServicioMock = new Mock<IHashServicio>();

        // Configuramos la propiedad Usuarios de la interfaz IUnitOfWork
        unitOfWorkMock
            .Setup(u => u.Usuarios)
            .Returns(usuarioRepositorioMock.Object);

        hashServicioMock
            .Setup(h => h.Hash(It.IsAny<string>()))
            .Returns("hash_password_seguro");

        usuarioRepositorioMock
            .Setup(r => r.AgregarAsync(It.IsAny<Usuario>()))
            .Returns(Task.CompletedTask);

        // GuardarCambiosAsync no recibe parámetros en tu interfaz IUnitOfWork
        unitOfWorkMock
            .Setup(u => u.GuardarCambiosAsync())
            .ReturnsAsync(1);

        var servicio = new UsuarioServicio(
            unitOfWorkMock.Object,
            contextoUsuarioMock.Object,
            hashServicioMock.Object
        );

        var dto = new UsuarioCrearDto(
            Nombre: "Juan Perez",
            Username: "jperez",
            Password: "Password123*",
            Roles: []
        );

        // Actuar
        var respuesta = await servicio.AgregarAsync(dto);

        // Afirmar
        Assert.True(respuesta.EsExitoso);
        usuarioRepositorioMock.Verify(r => r.AgregarAsync(It.IsAny<Usuario>()), Times.Once);
        unitOfWorkMock.Verify(u => u.GuardarCambiosAsync(), Times.Once);
    }


}