using CatalogoAranda.ApplicationCore.ApplicationExceptions;
using CatalogoAranda.ApplicationCore.DataInterfaces.Repositories;
using CatalogoAranda.ApplicationCore.DataInterfaces.UnitOfWork;
using CatalogoAranda.ApplicationCore.Entities;
using CatalogoAranda.ApplicationCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.Tests.ImagenesServiceTests
{
    public class BaseImagenServiceTests
    {
        protected Mock<IUnitOfWork> mockedUnitOfWork = new();
        protected Mock<IUnitOfWorkAdapter> mockedUnitOfWorkAdapter = new();
        protected Mock<IUnitOfWorkRepository> mockedUnitOfWorkRepository = new();
        protected Mock<IImagenesRepository> mockedImagenesRepository = new();
        protected Mock<IProductosRepository> mockedProductosRepository = new();

        protected void ResetMockedVariables()
        {
            mockedUnitOfWork = new();
            mockedUnitOfWorkAdapter = new();
            mockedUnitOfWorkRepository = new();
            mockedImagenesRepository = new();
            mockedProductosRepository = new();
        }

        protected void SetSaveChangesExceptionAsync()
        {
            mockedUnitOfWorkAdapter.Setup(x => x.SaveChangesAsync())
                .ThrowsAsync(new CatalogoDbUpdateException("Guardado Inválido"));
        }

        protected void SetMockedObjects()
        {
            mockedUnitOfWorkRepository.SetupGet(x => x.ImagenesRepository).Returns(mockedImagenesRepository.Object);
            mockedUnitOfWorkRepository.SetupGet(x => x.ProductosRepository).Returns(mockedProductosRepository.Object);
            mockedUnitOfWorkAdapter.SetupGet(x => x.Repositories).Returns(mockedUnitOfWorkRepository.Object);
            mockedUnitOfWork.Setup(x => x.Create()).Returns(mockedUnitOfWorkAdapter.Object);

        }

        protected void SetMockedImagenRepositoryGet(bool Nulo)
        {
            if (Nulo)
            {
                Imagen? ImagenNula = null;
                mockedImagenesRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(ImagenNula);
            }
            else
                mockedImagenesRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(CreateTestImagen());
        }
        protected void SetMockedProductoRepositoryGet(bool Nulo)
        {
            if (Nulo)
            {
                Producto? ProductoNula = null;
                mockedProductosRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(ProductoNula);
            }
            else
                mockedProductosRepository.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Producto());
        }

        protected Imagen CreateTestImagen()
        {
            return new Imagen
            {
                Id = Guid.NewGuid(),
                Nombre = "ImagenPrueba",
                Base64 = "ContenidoImagenBase64",
                ProductoId = Guid.NewGuid(),
                Url = "UrlPrueba",
                Producto = new Producto()
            };
        }
        protected void SetGetAsyncForRepositories()
        {
            mockedUnitOfWorkRepository.Setup(x => x.ImagenesRepository.GetAsync(
                It.IsAny<Guid>())).ReturnsAsync(new Imagen());
        }

        protected void SetIdNotExists(bool result)
        {
            mockedImagenesRepository.Setup(
                x => x.IdNotExistsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(result);
        }
    }
}
