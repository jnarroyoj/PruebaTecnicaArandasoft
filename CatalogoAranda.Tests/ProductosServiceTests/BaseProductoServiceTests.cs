using CatalogoAranda.ApplicationCore.ApplicationExceptions;
using CatalogoAranda.ApplicationCore.DataInterfaces.Repositories;
using CatalogoAranda.ApplicationCore.DataInterfaces.UnitOfWork;
using CatalogoAranda.ApplicationCore.Entities;
using CatalogoAranda.ApplicationCore.Services;
using CatalogoAranda.ApplicationCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoAranda.Tests.ProductosServiceTests
{
    public class BaseImagenServiceTests
    {
        protected Mock<IUnitOfWork> mockedUnitOfWork = new();
        protected Mock<IUnitOfWorkAdapter> mockedUnitOfWorkAdapter = new();
        protected Mock<IUnitOfWorkRepository> mockedUnitOfWorkRepository = new();
        protected Mock<IProductosRepository> mockedProductosRepository = new();
        protected Mock<ICategoriasService> mockedCategoriaService = new();
        protected Mock<IImagenesService> mockedImagenesService = new();

        protected void ResetMockedVariables()
        {
            mockedUnitOfWork = new();
            mockedUnitOfWorkAdapter = new();
            mockedUnitOfWorkRepository = new();
            mockedProductosRepository = new();
            mockedCategoriaService = new();
            mockedImagenesService = new();
        }

        protected void SetSaveChangesExceptionAsync()
        {
            mockedUnitOfWorkAdapter.Setup(x => x.SaveChangesAsync())
                .ThrowsAsync(new CatalogoDbUpdateException("Guardado Inválido"));
        }

        protected void SetMockedObjects()
        {
            mockedUnitOfWorkRepository.SetupGet(x => x.ProductosRepository).Returns(mockedProductosRepository.Object);
            mockedUnitOfWorkAdapter.SetupGet(x => x.Repositories).Returns(mockedUnitOfWorkRepository.Object);
            mockedUnitOfWork.Setup(x => x.Create()).Returns(mockedUnitOfWorkAdapter.Object);

        }

        protected ProductosService InitializeProductosService()
        {
            return new ProductosService(mockedUnitOfWork.Object,
                mockedCategoriaService.Object, mockedImagenesService.Object);
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
        protected Producto CreateTestProduct()
        {
            return new Producto
            {
                Id = Guid.NewGuid(),
                Nombre = "ProductoPrueba",
                Descripcion = "Descripción categoría de prueba",
                Categoria = new List<Categoria>(),
                Imagenes = new List<Imagen>()
            };
        }
        protected void SetGetAsyncForRepositories()
        {
            mockedUnitOfWorkRepository.Setup(x => x.CategoriasRepository.GetAsync(
                It.IsAny<Guid>())).ReturnsAsync(new Categoria());
            mockedUnitOfWorkRepository.Setup(x => x.ImagenesRepository.GetAsync(
                It.IsAny<Guid>())).ReturnsAsync(new Imagen());
        }
    }
}
